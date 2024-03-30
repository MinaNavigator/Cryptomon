using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CropSo : ScriptableObject
{
    public Sprite seed;
    public Sprite leaf;
    public Sprite plant;
    public int MinLevel { get; set; }
}
