using AOT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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

    public static async Task<string> ConnectAccount()
    {
        taskConnected = new TaskCompletionSource<string>();
        GetAccount(Connected);
        string result = await taskConnected.Task;
        return result;
    }

    public static async Task<SignedData> Sign(string message)
    {
        taskSigned = new TaskCompletionSource<string>();
        SignMessage(message, Signed);
        string result = await taskSigned.Task;
        return JsonConvert.DeserializeObject<SignedData>(result);
    }
}
