using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CircleTile : Tile
{
    void Awake()
    {
        tileType = TileType.circle;
    }
}
