using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }

    public async void Connect()
    {
        var account = await MinaInteraction.ConnectAccount();
        Debug.Log("account" + account);
        var result = await ConnectionService.RequestConnection(account);
        Debug.Log(result);
        var sign = await MinaInteraction.Sign(result.Message);
        Debug.Log(sign);

        var login = new LoginVM() { Field = sign.Signature.Field, Scalar = sign.Signature.Scalar, Message = result.Message, Signer = account };
        var token = await ConnectionService.CreateTokenAsync(login);
        Debug.Log(token);
    }
    private async void MinaInteraction_OnAccountConnected(object sender, string account)
    {

    }

}
