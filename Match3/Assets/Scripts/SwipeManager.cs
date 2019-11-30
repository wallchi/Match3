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
    bool isLerping;
    bool isLerpingBack;

    private void Start()
    {
        board = GetComponent<Board>();
        isLerping = false;
        isLerpingBack = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLerping & !isLerpingBack)
        {
            selected1 = TileCheck();
        }

        if (Input.GetMouseButtonUp(0) && !isLerping & !isLerpingBack)
        {
            if (selected1)
            {
                selected2 = TileCheck();
                if (selected2)
                {
                    swap1 = selected1.transform.position;
                    swap2 = selected2.transform.position;
                    SelectedTilesPositionCheck();
                }
            }

            if (selected1 && selected2 && !ReferenceEquals(selected1, selected2))
            {
                isLerping = true;
            }
            else
            {
                ResetTiles();
            }
        }

        if(selected1 && (Vector2.Distance(selected1.transform.position, swap2) <= 0.01f) 
            && selected2 && (Vector2.Distance(selected2.transform.position, swap1) <= 0.01f) && isLerping)
        {
            selected1.transform.position = swap2;
            selected2.transform.position = swap1;
            board.TileSwapOnBoard(selected1, selected2);
            isLerping = false;
            if(!selected1.isMatched && !selected2.isMatched)
            {
                isLerpingBack = true;
            }
            if(!isLerpingBack)
                ResetTiles();
        }

        if (selected1 && (Vector2.Distance(selected1.transform.position, swap1) <= 0.01f)
            && selected2 && (Vector2.Distance(selected2.transform.position, swap2) <= 0.01f) && isLerpingBack)
        {
            selected1.transform.position = swap1;
            selected2.transform.position = swap2;
            board.TileSwapOnBoard(selected1, selected2);
            isLerpingBack = false;
            ResetTiles();
        }
    }

    private void FixedUpdate()
    {
        if(isLerping)
        {
            selected1.transform.position = Vector2.Lerp(selected1.transform.position, swap2, 0.33f);
            selected2.transform.position = Vector2.Lerp(selected2.transform.position, swap1, 0.33f);
        }
        if(isLerpingBack)
        {
            selected1.transform.position = Vector2.Lerp(selected1.transform.position, swap1, 0.33f);
            selected2.transform.position = Vector2.Lerp(selected2.transform.position, swap2, 0.33f);
        }
    }

    void SelectedTilesPositionCheck()
    {
        if (Mathf.RoundToInt(swap1.x) == Mathf.RoundToInt(swap2.x))
        {
            int int1 = Mathf.RoundToInt(swap1.y - swap2.y);
            if (int1 == 1)
            {
                return;
            }
            else if(int1 == -1)
            {
                return;
            }
        }
        else if(Mathf.RoundToInt(swap1.y) == Mathf.RoundToInt(swap2.y))
        {
            int int2 = Mathf.RoundToInt(swap1.x - swap2.x);
            if (int2 == 1)
            {
                return;
            }
            else if (int2 == -1)
            {
                return;
            }
        }
        ResetTiles();
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