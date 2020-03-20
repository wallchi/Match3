using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Board board;

    public Text score;

    public Text victory;

    private void Start()
    {
        victory.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateScore(int newScore)
    {
        score.text = newScore.ToString();

        if (board.GetScore() >= 20000)
        {
            Time.timeScale = 0;
            victory.gameObject.SetActive(true);
        }
    }

    public void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
