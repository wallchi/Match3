using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    circle,
    diamond,
    square,
    star,
    triangle
}
public class Tile : MonoBehaviour
{
    public TileType tileType;
    public bool isMatched;
    public TileType GetTileType() { return tileType; }
}