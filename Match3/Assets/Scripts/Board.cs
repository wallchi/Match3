using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int mWidth;
    public int mHeight;

    public UIControl uiControl;
    private Tile[,] tiles;

    public GameObject[] tilePrefabs;

    bool isBoardFilled;

    int score;
    public int GetScore() { return score; }

    public bool isBoardMatching;
    void Start()
    {
        tiles = new Tile[mWidth, mHeight];
        SetUp();
        MatchDetection();
    }

    private void Update()
    {
        if (!isBoardFilled)
            RefillBoard();
    }

    private void SetUp()
    {
        for (int i = 0; i < mWidth; i++)
        {
            for (int j = 0; j < mHeight; j++)
            {
                Vector2 tempPos = new Vector2(i, j);
                tiles[i, j] = InstantiateTile(tempPos);
            }
        }
        isBoardFilled = true;
    }

    Tile InstantiateTile(Vector2 position)
    {
        int num = Random.Range(0, 5);
        return Instantiate(tilePrefabs[num], position, Quaternion.identity).GetComponent<Tile>();
    }

    Tile InstantiateTile(int i ,int j)
    {
        int num = Random.Range(0, 5);
        Vector2 position = new Vector2(i, j);
        return Instantiate(tilePrefabs[num], position, Quaternion.identity).GetComponent<Tile>();
    }

    public void TileSwapOnBoard(Tile selected1, Tile selected2)
    {
        int new1Xvalue = Mathf.RoundToInt(selected1.transform.position.x);
        int new1Yvalue = Mathf.RoundToInt(selected1.transform.position.y);
        int new2Xvalue = Mathf.RoundToInt(selected2.transform.position.x);
        int new2Yvalue = Mathf.RoundToInt(selected2.transform.position.y);
        tiles[new1Xvalue, new1Yvalue] = selected1;
        tiles[new2Xvalue, new2Yvalue] = selected2;
        MatchDetection();
    }

    private void MatchDetection()
    {
        isBoardMatching = true;
        TileType temp;
        for (int i = 0; i < mWidth; i++)
        {
            for (int j = 0; j < mHeight; j++)
            {
                if(tiles[i,j] != null)
                {
                    temp = tiles[i, j].GetTileType();
                    if (i != 0 && i != (mWidth - 1))
                    {
                        if (tiles[i - 1, j] != null && tiles[i + 1, j] != null)
                        {
                            if (temp == tiles[i - 1, j].GetTileType() && temp == tiles[i + 1, j].GetTileType())
                            {
                                tiles[i, j].isMatched = true;
                                tiles[i - 1, j].isMatched = true;
                                tiles[i + 1, j].isMatched = true;
                            }
                        }
                    }
                    if (j != 0 && j != (mHeight - 1))
                    {
                        if(tiles[i, j - 1] != null && tiles[i, j + 1] != null)
                        {
                            if (temp == tiles[i, j - 1].GetTileType() && temp == tiles[i, j + 1].GetTileType())
                            {
                                tiles[i, j].isMatched = true;
                                tiles[i, j - 1].isMatched = true;
                                tiles[i, j + 1].isMatched = true;
                            }
                        }
                    }
                }
            }
        }
        CollapseMatched();
    }

    private void CollapseMatched()
    {
        for (int i = 0; i < mWidth; i++)
        {
            for (int j = 0; j < mHeight; j++)
            {
                if(tiles[i,j] != null && tiles[i,j].isMatched)
                {
                    StartCoroutine(Collapse(i, j));
                    score += 100;
                    uiControl.UpdateScore(score);
                }
            }
        }
        isBoardMatching = false;
    }
    IEnumerator Collapse(int i, int j)
    {
        tiles[i, j].GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        Destroy(tiles[i, j].gameObject);
        tiles[i, j] = null;
        isBoardFilled = false;
    }

    private void RefillBoard()
    {
        isBoardMatching = true;
        while (!IsBoardFilled())
        {
            for (int i = 0; i < mWidth; i++)
            {
                for (int j = 0; j < mHeight; j++)
                {
                    if (tiles[i, j] == null)
                    {
                        if (j != (mHeight-1) && tiles[i, j+1] != null)
                        {
                            tiles[i, j] = tiles[i, j+1];
                            tiles[i, j+1] = null;
                            //tiles[i, j].transform.position = new Vector2(i, j);
                            StartCoroutine(TransformTile(i, j));
                        }
                    }

                    if (tiles[i, mHeight - 1] == null)
                    {
                        tiles[i, mHeight - 1] = InstantiateTile(i, mHeight);
                        StartCoroutine(TransformTile(i, mHeight - 1));
                    }
                }
            }
        }

        isBoardFilled = true;
        MatchDetection();
    }

    IEnumerator TransformTile(int i, int j)
    {
        Vector2 currentPos = tiles[i, j].transform.position;
        Vector2 targetPos = new Vector2(i, j);
        float fraction = 0.000f;
        while((tiles[i, j] != null) && ((tiles[i, j].transform.position.y - j) > 0.01f) )
        {
            fraction += 0.1f;
            tiles[i, j].transform.position = Vector2.Lerp(tiles[i, j].transform.position, targetPos, 0.5f);
            if ((tiles[i, j] != null) && (Vector2.Distance(tiles[i, j].transform.position, targetPos) <= 0.01f))
            {
                tiles[i, j].transform.position = new Vector2(i, j);
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }


    bool IsBoardFilled()
    {
        for (int i = 0; i < mWidth; i++)
        {
            for (int j = 0; j < mHeight; j++)
            {
                if (tiles[i, j] == null)
                    return false;
            }
        }
        return true;
    }
}
