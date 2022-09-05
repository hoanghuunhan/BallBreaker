using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public int timeDelay;
    public Ball ball;

    int m_curTimeDelay;
    int m_level;
    int m_score;
    BrickManager m_levelObj;

    public int Level { get => m_level; }
    public BrickManager LevelObj { get => m_levelObj;}

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public override void Start()
    {
        m_curTimeDelay = timeDelay;
        StartCoroutine(CountingDown());
        Prefs.hasNewBest = false;
        GameGUIManager.Ins.UpdateScore(m_score);

        AudioController.Ins.PlayBackgroundMusic();
    }
    
    IEnumerator CountingDown()
    {
        BrickManager[] levelPrefabs = LevelManager.Ins.levelPrefabs;
        if (levelPrefabs != null && levelPrefabs.Length > 0 && levelPrefabs.Length > LevelManager.Ins.CurLevel)
        {
            BrickManager levelPrefab  = levelPrefabs[LevelManager.Ins.CurLevel];
            if (levelPrefab != null)
            {
                m_level = LevelManager.Ins.CurLevel;
                m_levelObj = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
            }
        }
        while (m_curTimeDelay > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeDelay--;

            if (m_curTimeDelay > 0)
            {
                AudioController.Ins.PlaySound(AudioController.Ins.timeBeep);
            } else
            {
                AudioController.Ins.PlaySound(AudioController.Ins.ballStartTrigger);
            }

            GameGUIManager.Ins.UpdateTimeCountDown(m_curTimeDelay);
        }
        if (ball != null)
        {
            ball.Trigger();
        }

        Prefs.SetGameEntered(true);
    }

    public void AddScore(int scoreToAdd)
    {
        m_score += scoreToAdd;
        Prefs.bestScore = m_score;
        GameGUIManager.Ins.UpdateScore(m_score);
    }
}
