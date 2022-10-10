using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PuzzleSelect : MonoBehaviour
{
    public int puzzleId = 0;

    public void OnClick()
    {
        Appear();
        Puzzles myPuzzle = SaveData.Instance.gameData.puzzleData[puzzleId];
        PuzzleBtn.Instance.puzzleId = puzzleId;
        Text stageTxt = PuzzleCreate.Instance.puzzleSelectTabs[2].GetComponent<Text>();
        Text sizeTxt = PuzzleCreate.Instance.puzzleSelectTabs[3].GetComponent<Text>();
        Text nameTxt = PuzzleCreate.Instance.puzzleSelectTabs[4].GetComponent<Text>();

        if (SaveData.Instance.gameData.puzzleData[puzzleId].IsCleared)
            PuzzleBtn.Instance.PuzzleImage.sprite = SpriteHub.Instance.puzzleSprites[puzzleId];
        else
            PuzzleBtn.Instance.PuzzleImage.sprite = SpriteHub.Instance.puzzleSpriteUnknown;

        stageTxt.text = string.Format("Stage {0}", myPuzzle.PuzzleId + 1);

        if (myPuzzle.Stage == 1)
        {
            sizeTxt.text = "5 x 5";
        }
        else if (myPuzzle.Stage == 2)
        {
            sizeTxt.text = "10 x 10";
        }
        else if (myPuzzle.Stage == 3)
        {
            sizeTxt.text = "15 x 15";
        }

        if (myPuzzle.IsCleared)
            nameTxt.text = string.Format("\"{0}\"", myPuzzle.PuzzleName);
        else
            nameTxt.text = "\"???\"";

    }

    private void Appear()
    {
        PuzzleCreate.Instance.puzzleSelectTabs[0].SetActive(true);
        PuzzleCreate.Instance.puzzleSelectTabs[0].GetComponent<Image>().DOFade(0.2f, 1);
        PuzzleCreate.Instance.puzzleSelectTabs[1].GetComponent<Image>().DOFade(0.8f, 1);
        for (int i = 2; i < PuzzleCreate.Instance.puzzleSelectTabs.Length; i++)
        {
            if (PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Image>() != null)
            {
                PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Image>().DOFade(1, 2);
            }

            if (PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Text>() != null)
            {
                PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Text>().DOFade(1, 2);
            }
        }
    }
}
