using Assets.Scripts.Service;
using Assets.Scripts.Service.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    private LandDto land;

    public Tilemap tilemapHole;
    public Tile tileHole;
    public List<CropSo> crops;
    public List<CropTileBase> cropTiles = new List<CropTileBase>();
    public CropSo selectedCrop;
    public List<BoxCollider2D> holesPosition;

    const string HOLE_NAME = "tileset_16px_777";

    // Start is called before the first frame update
    void Start()
    {
        //GetLand();

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
            if (tile?.name == HOLE_NAME)
            {
                var plant = new PlantingDto() { PlantingDate = DateTime.Now };
                var pos = holesPosition.Where(i => i.OverlapPoint(worldPoint)).First();
                AddPlant(pos, plant);

            }

            var found = cropTiles.FirstOrDefault(x => x == tile);
            if (found?.CanHarvest == true)
            {
                tilemapHole.SetTile(position, tileHole);
                cropTiles.Remove(found);
            }
        }
    }


    Dictionary<Vector3Int, TileBase> GetAllTiles()
    {
        BoundsInt bounds = tilemapHole.cellBounds;
        Dictionary<Vector3Int, TileBase> result = new Dictionary<Vector3Int, TileBase>();

        foreach (var pos in tilemapHole.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemapHole.HasTile(localPlace))
            {
                var tile = tilemapHole.GetTile(localPlace);
                result.Add(localPlace, tile);
            }
        }

        return result;
    }

    public void FixedUpdate()
    {
        tilemapHole.RefreshAllTiles();
    }

    public async void GetLand()
    {
        land = await LandService.GetLand();
        if (land.Plantings?.Count > 0)
        {
            for (int i = 0; i < land.Plantings.Count; i++)
            {
                var plant = land.Plantings[i];

                if (holesPosition.Count < (plant.Square - 1))
                {
                    var last = holesPosition.Count - 1;
                    var hole = holesPosition[last];
                    AddPlant(hole, plant);
                }
                else
                {
                    var hole = holesPosition[plant.Square];
                    AddPlant(hole, plant);
                }
            }
        }

    }

    private void AddPlant(BoxCollider2D hole, PlantingDto plant)
    {
        CropTileBase tileCrop = ScriptableObject.CreateInstance<CropTileBase>();
        tileCrop.cropSo = crops[0];
        tileCrop.plantingDto = plant;
        Vector3Int position = tilemapHole.WorldToCell(hole.transform.localPosition);
        tilemapHole.SetTile(position, tileCrop);
        cropTiles.Add(tileCrop);
    }


}
