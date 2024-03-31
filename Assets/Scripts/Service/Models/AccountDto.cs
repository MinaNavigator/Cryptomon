using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class AccountDto
{
    public string Address { get; set; } // Unique account address (the Ethereum account)
    public string Username { get; set; } // The user name
    public string RecoveryEmail { get; set; } // The user Email
    public decimal CoinBalance { get; set; }
    public decimal MinaBalance { get; set; }
}
