using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    [SerializeField]
    private Tile selected1;
    [SerializeField]
    private Tile selected2;

    private Board board;
    Vector2 swap1; 
    Vector2 swap2;

    private void Start()
    {
        board = GetComponent<Board>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected1 = TileCheck();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selected1)
            {
                selected2 = TileCheck();
                if (selected2)
                {
                    swap1 = selected1.transform.position;
                    swap2 = selected2.transform.position;
                }
            }

            if (selected1 && selected2 && !ReferenceEquals(selected1, selected2))
            {
                selected1.transform.position = swap2;
                selected2.transform.position = swap1;
                ResetTiles();
            }
            else
            {
                ResetTiles();
            }
        }
    }

    void ResetTiles()
    {
        selected1 = null;
        selected2 = null;
    }

    Tile TileCheck()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit)
        {
            Tile tile = hit.transform.gameObject.GetComponent<Tile>();
            return tile;
        }
        else
        {
            return null;
        }
    }
}