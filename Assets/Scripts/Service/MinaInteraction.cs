using AOT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MinaSignerNet;

public static class MinaInteraction
{
#if UNITY_WEBGL
    [DllImport("__Internal")]
    public static extern string GetAccount(Action<string> callback);
    [DllImport("__Internal")]
    public static extern string SignMessage(string message, Action<string> callback);
#else
        // handle special platform like ios who throw an error on DllImport
        public static string GetAccount(Action<string> callback)
        {
           return string.Empty;
        }

        public static string SignMessage((Action<string> callback)
        {
            return string.Empty;
        }
#endif

    private static TaskCompletionSource<string> taskConnected;
    private static TaskCompletionSource<string> taskSigned;

    [MonoPInvokeCallback(typeof(Action<string>))]
    private static void Connected(string result)
    {
        taskConnected?.TrySetResult(result);
    }

    [MonoPInvokeCallback(typeof(Action<string>))]
    private static void Signed(string result)
    {
        taskSigned?.TrySetResult(result);
    }

    private static string privateKey = "EKDtctFSZuDJ8SXuWcbXHot57gZDtu7dNSAZNZvXek8KF8q6jV8K";
    private static string account = "B62qj5tBbE2xyu9k4r7G5npAGpbU1JDBkZm85WCVDMdCrHhS2v2Dy2y";

    public static async Task<string> ConnectAccount()
    {
#if UNITY_EDITOR
        return account;
#else
        taskConnected = new TaskCompletionSource<string>();
        GetAccount(Connected);
        string result = await taskConnected.Task;
        return result;
#endif

    }

    public static async Task<SignedData> Sign(string message)
    {
#if UNITY_EDITOR
        var signature = Signature.Sign(message, privateKey, Network.Testnet);
        return new SignedData() { Data = message, PublicKey = account, Signature = new SignatureResult { Field = signature.R.ToString(), Scalar = signature.S.ToString() } };
#else
        taskSigned = new TaskCompletionSource<string>();
        SignMessage(message, Signed);
        string result = await taskSigned.Task;
        return JsonConvert.DeserializeObject<SignedData>(result);
#endif

    }
}
