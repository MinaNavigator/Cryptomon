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
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private LandDto land;
    private List<CropTileBase> cropTiles = new List<CropTileBase>();
    private CropSo selectedCrop;

    public Tilemap tilemapHole;
    public Tile tileHole;
    public List<CropSo> crops;
    public List<BoxCollider2D> holesPosition;
    public TextMeshProUGUI textAmount;
    public Image cropImage;

    const string HOLE_NAME = "tileset_16px_777";

    // Start is called before the first frame update
    void Start()
    {
        GetLand();
        textAmount.text = GameContext.Instance?.Account?.CoinBalance.ToString("C2");

        selectedCrop = crops[0];
        cropImage.sprite = selectedCrop.presentation;

        CropMenu.OnSelectCrop += CropMenu_OnSelectCrop;
    }

    private void CropMenu_OnSelectCrop(CropSo obj)
    {
        selectedCrop = obj;
        cropImage.sprite = selectedCrop.presentation;
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
                var hole = holesPosition.Where(i => i.OverlapPoint(worldPoint)).First();
                AddNewPlant(hole);
            }

            var found = cropTiles.FirstOrDefault(x => x == tile);
            if (found?.CanHarvest == true)
            {
                var hole = holesPosition.Where(i => i.OverlapPoint(worldPoint)).First();
                Harvest(hole, found);
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
        if (land.Plantings?.Count > 0)
        {
            for (int i = 0; i < land.Plantings.Count; i++)
            {
                var plant = land.Plantings[i];

                if (plant?.FruitId > 0)
                {
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

    }

    async void AddNewPlant(BoxCollider2D hole)
    {
        try
        {
            var plant = new PlantingDto();
            plant.FruitId = selectedCrop.id;
            var index = holesPosition.IndexOf(hole);
            plant.Square = index;
            plant.PlantingDate = DateTime.UtcNow;
            var newPlant = await LandService.AddPlant(plant);
            textAmount.text = newPlant.CoinBalance.ToString("C2");
            AddPlant(hole, newPlant);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

    }

    async void Harvest(BoxCollider2D hole, CropTileBase tileCrop)
    {
        try
        {
            Vector3Int position = tilemapHole.WorldToCell(hole.transform.localPosition);
            var oldPlant = await LandService.HarvestPlant(tileCrop.plantingDto);
            textAmount.text = oldPlant.CoinBalance.ToString("C2");
            tilemapHole.SetTile(position, tileHole);
            cropTiles.Remove(tileCrop);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void AddPlant(BoxCollider2D hole, PlantingDto plant)
    {
        if (plant?.FruitId > 0)
        {
            CropTileBase tileCrop = ScriptableObject.CreateInstance<CropTileBase>();
            var crop = crops.Where(c => c.id == plant.FruitId).FirstOrDefault();
            if (crop != null)
            {
                tileCrop.cropSo = crop;
                tileCrop.plantingDto = plant;
                Vector3Int position = tilemapHole.WorldToCell(hole.transform.localPosition);
                tilemapHole.SetTile(position, tileCrop);
                cropTiles.Add(tileCrop);
            }
        }
    }

    public void ChangeCrop(int fruitId)
    {
        var fruit = crops.Where(x => x.id == fruitId).FirstOrDefault();
        if (fruit != null)
        {
            fruit = crops.First();
        }
        selectedCrop = fruit;
    }

    public void ShowCrop()
    {
        SceneManager.LoadScene("Crops", LoadSceneMode.Additive);
    }

}
