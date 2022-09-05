using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelGoto;
    public bool isUnlocked;
    public GameObject levelState;
    public Image icon;
    public Text levelText;
    public Sprite checkmark;
    public Sprite lockIcon;

    Button m_btnComp;

    private void Start()
    {
        if (levelText)
        {
            levelText.text = (levelGoto+1).ToString("00");
        }
        m_btnComp = GetComponent<Button>();

        if (m_btnComp)
        {
            m_btnComp.onClick.AddListener(() => GotoLevel());
        }

        if (!Prefs.IsGameEntered())
        {
            Prefs.SetLevelUnlocked(levelGoto, isUnlocked);
        }
        if (Prefs.IsLevelUnlocked(levelGoto))
        {
            if (levelState != null)
            {
                levelState.SetActive(false);
            }
            if (Prefs.IsLevelPassed(levelGoto))
            {
                if (levelState != null)
                {
                    levelState.SetActive(true);
                }
                if (icon && checkmark)
                {
                    icon.sprite = checkmark;
                }
            }
        }

        else
        {
            if (levelState)
            {
                levelState.SetActive(true);
            }
            if (icon && lockIcon)
            {
                icon.sprite = lockIcon;
            }
        }
    }

    public void GotoLevel()
    {
        if (Prefs.IsLevelUnlocked(levelGoto))
        {
            LevelManager.Ins.CurLevel = levelGoto;
            SceneManager.LoadScene(SceneConsts.GAME_PLAY);
        }
    }
}
