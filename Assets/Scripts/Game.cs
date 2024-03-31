using Assets.Scripts.Service;
using Assets.Scripts.Service.Models;
using System;
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
    public Tile hole;
    public List<CropSo> crops;
    public List<CropTileBase> cropTiles = new List<CropTileBase>();
    public CropSo selectedCrop;


    // Start is called before the first frame update
    void Start()
    {
        // GetLand();
        selectedCrop = crops[0];
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
            if (tile?.name == "tileset_16px_777")
            {
                CropTileBase tileCrop = ScriptableObject.CreateInstance<CropTileBase>();
                tileCrop.cropSo = crops[0];
                tileCrop.plantingDto = new PlantingDto() { PlantingDate = DateTime.Now };
                tilemapHole.SetTile(position, tileCrop);
                cropTiles.Add(tileCrop);

            }
        }
    }

    public void FixedUpdate()
    {
        tilemapHole.RefreshAllTiles();       
    }

    public async void GetLand()
    {
        land = await LandService.GetLand();


    }


}
