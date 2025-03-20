using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Map
{
    public string mapName;
    public Sprite mapSprite;
    public MapComponent[] mapComponents;
}
[System.Serializable]
public class MapComponent
{
    public GameObject mapComponent;
    public float positionX;
    public float positionY;
    public float positionZ;
}