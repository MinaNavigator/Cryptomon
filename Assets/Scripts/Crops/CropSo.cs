using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CropSo : ScriptableObject
{
    public int id;
    public Sprite presentation;
    public List<Sprite> plantStates;
    public int minLevel;
    public int growTime;
    public float seedPrice;
    public float plantPrice;
}
