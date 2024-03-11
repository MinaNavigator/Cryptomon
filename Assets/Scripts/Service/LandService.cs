using Assets.Scripts.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Service
{
    public class LandService
    {

        private static string serviceUrl = "https://localhost:7155/";


        static LandService()
        {
            serviceUrl = "https://cryptomon.azurewebsites.net/";

#if UNITY_EDITOR
            serviceUrl = "https://localhost:7155/";
#endif

        }

        public static async Task<LandDto> GetLand()
        {
            Debug.Log("GetLand");
            var url = $"{serviceUrl}api/land";
            var response = await UnityRequestClient.Get<LandDto>(url);
            Debug.Log("land loaded");
            return response;
        }
    }
}
