using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    none,
    square,
    triangle,
    circle,
    diamond,
    star
}
public abstract class Tile : MonoBehaviour
{
    protected TileType tileType;
    protected bool isBottomCollided;
    protected Vector3 downwardVec;

    private void Awake()
    {
        isBottomCollided = false;
        downwardVec = new Vector3();
        downwardVec.y = -5f;
    }
    private void FixedUpdate()
    {
        if (!isBottomCollided)
            transform.Translate(downwardVec * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Tile tile = collision.gameObject.GetComponent<Tile>();
    }
}