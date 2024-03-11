using Assets.Scripts.Service;
using Assets.Scripts.Service.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    LandDto land;

    // Start is called before the first frame update
    void Start()
    {
        GetLand();
    }



    // Update is called once per frame
    void Update()
    {

    }

    public async void GetLand()
    {
        land = await LandService.GetLand();
    }


}
