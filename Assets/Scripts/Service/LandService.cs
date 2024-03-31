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

        public static async Task<PlantingDto> AddPlant(PlantingDto plant)
        {
            Debug.Log("add plant");
            var url = $"{serviceUrl}api/land/AddPlant";
            var response = await UnityRequestClient.Post<PlantingDto>(url, plant);
            Debug.Log("plant added");
            return response;
        }

        public static async Task<PlantingDto> HarvestPlant(PlantingDto plant)
        {
            Debug.Log("harvest plant");
            var url = $"{serviceUrl}api/land/HarvestPlant";
            var response = await UnityRequestClient.Post<PlantingDto>(url, plant);
            Debug.Log("plant harvested");
            return response;
        }
    }
}
