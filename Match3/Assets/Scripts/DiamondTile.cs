using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DiamondTile : Tile
{
    void Awake()
    {
        tileType = TileType.diamond;
    }
}
