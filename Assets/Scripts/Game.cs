using Assets.Scripts.Service;
using Assets.Scripts.Service.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private LandDto land;

    public Tilemap tilemapHole;
    public Tilemap tilemapCrops;
    public CropSo[] crops;
    public List<GameObject> UITiles;
    public int selectedCrops = 0;
    public Transform tileGridUI;


    // Start is called before the first frame update
    void Start()
    {
        // GetLand();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = tilemapHole.WorldToCell(worldPoint);

            TileBase tile = tilemapHole.GetTile(position);
            Debug.Log(tile);
            if (tile != null)
            {
                CropTileBase tileCrop = ScriptableObject.CreateInstance<CropTileBase>();
                tileCrop.cropSo = crops[0];
                tilemapHole.SetTile(position, tileCrop);
            }
        }
    }

    public async void GetLand()
    {
        land = await LandService.GetLand();
    }


}
