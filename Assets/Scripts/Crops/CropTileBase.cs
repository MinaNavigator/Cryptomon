using Assets.Scripts.Service.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTileBase : TileBase
{
    public CropSo cropSo;
    public PlantingDto plantingDto;
    public ITilemap tilemap;
    public Vector3Int position;

    public bool CanHarvest
    {
        get
        {
            var time = DateTime.Now - plantingDto.PlantingDate;
            return time.TotalSeconds >= cropSo.growTime;
        }
    }

    // Docs: https://docs.unity3d.com/ScriptReference/Tilemaps.TileBase.GetTileData.html

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = cropSo.plantStates[0];
        if (plantingDto != null)
        {
            var time = DateTime.Now - plantingDto.PlantingDate;
            if (time.TotalSeconds > 10)
            {
                // grow over the time care divide by zero
                int x = (int)time.TotalSeconds * cropSo.plantStates.Count / cropSo.growTime;
                int last = cropSo.plantStates.Count - 1;
                if (x > last)
                {
                    x = last;
                }
                if (x == last && !CanHarvest)
                {
                    x = last - 1;
                }
                if (x < 0)
                {
                    x = 0;
                }
                tileData.sprite = cropSo.plantStates[x];
            }
        }
    }
}
