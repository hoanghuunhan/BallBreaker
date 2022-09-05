using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGUIManager : MonoBehaviour
{
    public LevelSelection lvSelectionDialog;

    public void PlayGame()
    {
        if (lvSelectionDialog)
        {
            lvSelectionDialog.Show(true);
        }
    }
}
