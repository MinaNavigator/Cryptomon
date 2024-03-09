using System;
using Newtonsoft.Json;

public class TokenResponse
{
    [JsonProperty("accessToken")]
    public string Token { get; set; }

    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }
}

public class ErrorResponse
{
    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("error_description")]
    public string Description { get; set; }
}

public class LoginVM
{
    public string Signer { get; set; } // Mina account that claim the signature
    public string Field { get; set; } // The signature R composant
    public string Scalar { get; set; } // The signature S composant
    public string Message { get; set; } // The plain message
}

public class MessageVM
{
    public string Account { get; set; }
    public string Message { get; set; }
}

public class SignedData
{
    public string PublicKey { get; set; }
    public string Data { get; set; }
    public SignatureResult Signature { get; set; }
}

public class SignatureResult
{
    public string Field { get; set; }
    public string Scalar { get; set; }
}