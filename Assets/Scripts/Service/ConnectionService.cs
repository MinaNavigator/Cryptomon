using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;


class LoginRequest
{
    /// <summary>
    /// The user's email address which acts as a user name.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The user's password.
    /// </summary>
    public string Password { get; set; }

}

public class ConnectionService
{
    private static string serviceUrl = "https://localhost:7155/";


    static ConnectionService()
    {
        serviceUrl = "https://cryptomon.azurewebsites.net/";

#if UNITY_EDITOR
        serviceUrl = "https://localhost:7155/";
#endif

    }

    public static async Task<TokenResponse> CreateTokenAsync(LoginVM loginInfo)
    {
        Debug.Log("GetTokenAsync");
        var url = $"{serviceUrl}api/auth/createtoken";
        var response = await UnityRequestClient.Post<TokenResponse>(url, loginInfo);
        Debug.Log("token created");
        return response;
    }

    public static async Task<AccountDto> GetAccount()
    {
        Debug.Log("get account");
        var url = $"{serviceUrl}api/auth/GetAccount";
        var response = await UnityRequestClient.Get<AccountDto>(url);
        Debug.Log("name" + response.Username);
        return response;
    }

    public static async Task<AccountDto> Register(AccountDto account)
    {
        Debug.Log("register");
        var url = $"{serviceUrl}api/auth/register";
        var response = await UnityRequestClient.Post<AccountDto>(url, account);
         Debug.Log("name" + response.Username);      
        return response;
    }

    public static async Task<MessageVM> RequestConnection(string account)
    {
        Debug.Log("GetTokenAsync");

        var url = $"{serviceUrl}api/auth/RequestConnection?account={account}";

        return await UnityRequestClient.Get<MessageVM>(url);
    }

    public static async Task<TokenResponse> RefreshToken()
    {
        Debug.Log("RefreshToken");
        var url = $"{serviceUrl}api/account/refresh";
        var player = await UnityRequestClient.Post<TokenResponse>(url, new TokenResponse() { RefreshToken = GameContext.Instance.RefreshToken });
        return player;
    }
}
