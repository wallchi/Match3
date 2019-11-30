using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StarTile : Tile
{
    void Awake()
    {
        tileType = TileType.star;
    }
}
