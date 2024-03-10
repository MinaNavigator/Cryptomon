using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public TMP_InputField txtUsernane;
    public TMP_Text title;
    public Button btnConnect;
    public Button btnRegister;

    // Start is called before the first frame update
    void Start()
    {
        btnConnect.gameObject.SetActive(true);
        btnRegister.gameObject.SetActive(false);
        txtUsernane.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {

    }

    public async void Connect()
    {
        try
        {
            title.color = Color.blue;
            btnConnect.gameObject.SetActive(false);
            var address = await MinaInteraction.ConnectAccount();
            Debug.Log("account" + address);
            var result = await ConnectionService.RequestConnection(address);
            Debug.Log(result);
            var sign = await MinaInteraction.Sign(result.Message);
            Debug.Log(sign);
            var login = new LoginVM() { Field = sign.Signature.Field, Scalar = sign.Signature.Scalar, Message = result.Message, Signer = address };
            var token = await ConnectionService.CreateTokenAsync(login);
            Debug.Log(token.Token);
            GameContext.Instance.Token = token.Token;
            var accountDto = await ConnectionService.GetAccount();
            accountDto.Address = address;
            GameContext.Instance.Account = accountDto;
            if (!string.IsNullOrEmpty(accountDto.Username))
            {
                title.text = $"Welcome {accountDto.Username}";
            }
            else
            {
                txtUsernane.gameObject.SetActive(true);
                btnRegister.gameObject.SetActive(true);
            }
        }
        catch (System.Exception ex)
        {
            title.text = ex.Message;
            title.color = Color.red;

            btnConnect.gameObject.SetActive(true);
        }
    }

    public async void Register()
    {
        try
        {
            title.color = Color.blue;
            txtUsernane.gameObject.SetActive(false);
            btnRegister.gameObject.SetActive(false);
            var name = txtUsernane.text;
            AccountDto account = GameContext.Instance.Account;
            account.Username = name;
            account.RecoveryEmail = string.Empty;
            var accountRegistered = await ConnectionService.Register(account);
            GameContext.Instance.Account = accountRegistered;
            title.text = $"Welcome {accountRegistered.Username}";
        }
        catch (System.Exception ex)
        {
            title.text = ex.Message;
            title.color = Color.red;
            btnRegister.gameObject.SetActive(true);
            txtUsernane.gameObject.SetActive(true);
        }
    }

}
