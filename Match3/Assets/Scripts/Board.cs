using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int columns;
    public int rows;

    public Tile[,] tiles;

    public GameObject circlePrefab;
    public GameObject diamondPrefab;
    public GameObject squarePrefab;
    public GameObject starPrefab;
    public GameObject trianglePrefab;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[columns, rows];
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector2 tempPos = new Vector2(i, j);
                int tempNum = Random.Range(1, 6);
                InstantiateTile(tempNum, tempPos);
            }
        }
    }

    void InstantiateTile(int num, Vector2 position)
    {
        switch (num)
        {
            case 1:
                Instantiate(circlePrefab, position, Quaternion.identity);
                break;
            case 2:
                Instantiate(diamondPrefab, position, Quaternion.identity);
                break;
            case 3:
                Instantiate(squarePrefab, position, Quaternion.identity);
                break;
            case 4:
                Instantiate(starPrefab, position, Quaternion.identity);
                break;
            case 5:
                Instantiate(trianglePrefab, position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
