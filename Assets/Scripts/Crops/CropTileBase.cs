using Assets.Scripts.Service.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTileBase : TileBase
{
    public CropSo cropSo;
    public PlantingDto plantingDto;

    // Docs: https://docs.unity3d.com/ScriptReference/Tilemaps.TileBase.GetTileData.html

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = cropSo.seed;
    }
}
