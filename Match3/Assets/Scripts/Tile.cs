using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    none,
    circle,
    diamond,
    square,
    star,
    triangle
}
public abstract class Tile : MonoBehaviour
{
    protected TileType tileType;
    public bool isMatched;
    public TileType GetTileType() { return tileType; }
}