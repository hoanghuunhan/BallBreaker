using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public BrickManager[] levelPrefabs;
    int m_curLevel;

    public int CurLevel { get => m_curLevel; set => m_curLevel = value; }
}
