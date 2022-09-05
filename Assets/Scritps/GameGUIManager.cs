using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public Text scoreText;
    public Text timeCountingdownText;
    public PauseDialog pauseDialog;
    public WinDialog winDialog;
    public GameOverDialog gameOverDialog;
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void UpdateScore(int score)
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + score.ToString("n0");
        }
    }
    public void UpdateTimeCountDown(int time)
    {
        if (timeCountingdownText)
        {
            timeCountingdownText.text = time.ToString("00");

        }
        if (time <= 0)
        {
            if (timeCountingdownText)
            {
                timeCountingdownText.gameObject.SetActive(false);
            }
        }
    }
    public void BackToHome()
    {
        SceneManager.LoadScene(SceneConsts.MAIN);
    }

    public void PauseGame()
    {
        if (pauseDialog)
        {
            pauseDialog.Show(true);
        }
    }
}
