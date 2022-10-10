using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCreate : Singleton<PuzzleCreate>
{
    [SerializeField] GameObject puzzleTab = null;
    [SerializeField] SpriteHub spriteHub = null;
    public GameObject[] puzzleSelectTabs = null;
    public GameObject puzzleLoadingTab = null;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(puzzleTab, this.transform);
        }

        PuzzleRefresh(1);
    }

    public void PuzzleRefresh(int stage)
    {
        List<Puzzles> puzzleList = SaveData.Instance.gameData.puzzleData;
        for (int i = (stage - 1) * 10; i < stage * 10; i++)
        {
            if (puzzleList[i].IsCleared)
            {
                puzzleList[i].PuzzleSpr = spriteHub.puzzleSprites[i];
            }
            else
            {
                puzzleList[i].PuzzleSpr = spriteHub.puzzleSpriteUnknown;
            }

            Text stageTxt = transform.GetChild(i - ((stage - 1) * 10)).GetChild(0).GetComponent<Text>();
            transform.GetChild(i - ((stage - 1) * 10)).GetComponent<PuzzleSelect>().puzzleId = puzzleList[i].PuzzleId;

            stageTxt.text = string.Format("스테이지 {0}", puzzleList[i].PuzzleId + 1);

            transform.GetChild(i - ((stage - 1) * 10)).GetComponent<Image>().sprite = puzzleList[i].PuzzleSpr;
        }
    }

    public void StageBtn(int stage)
    {
        PuzzleRefresh(stage);
    }
}
