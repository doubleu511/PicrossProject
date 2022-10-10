using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField]
    public List<Puzzles> puzzleData = new List<Puzzles>();

    public void ClearData()
    {
        foreach(Puzzles item in puzzleData)
        {
            item.IsCleared = false;
        }
    }
}

[Serializable]
public class Puzzles
{
    public int Stage;
    public int PuzzleId;
    public string PuzzleName;
    [HideInInspector]
    public Sprite PuzzleSpr = null;
    public bool IsCleared = false;

    public Puzzles(int m_stage, int m_id, Sprite m_puzzleSpr, bool m_isCleared)
    {
        Stage = m_stage;
        PuzzleId = m_id;
        PuzzleSpr = m_puzzleSpr;
        IsCleared = m_isCleared;
    }
}