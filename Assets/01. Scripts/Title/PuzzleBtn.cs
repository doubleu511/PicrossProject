using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PuzzleBtn : Singleton<PuzzleBtn>
{
    public Image PuzzleImage = null;
    public int puzzleId = 0;

    public void OnStart()
    {
        PlayerPrefs.SetInt("puzzleId", puzzleId);
        PuzzleCreate.Instance.puzzleLoadingTab.SetActive(true);
        PuzzleCreate.Instance.puzzleLoadingTab.transform.GetComponent<Image>().DOFade(1, 2).OnComplete(GoPuzzle);
    }

    private void GoPuzzle()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnCancel()
    {
        PuzzleCreate.Instance.puzzleSelectTabs[0].GetComponent<Image>().DOFade(0f, 1).OnComplete(SetActiveFalse);
        PuzzleCreate.Instance.puzzleSelectTabs[1].GetComponent<Image>().DOFade(0f, 1);
        for (int i = 2; i < PuzzleCreate.Instance.puzzleSelectTabs.Length; i++)
        {
            if (PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Image>() != null)
            {
                PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Image>().DOFade(0, 1);
            }

            if (PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Text>() != null)
            {
                PuzzleCreate.Instance.puzzleSelectTabs[i].GetComponent<Text>().DOFade(0, 1);
            }
        }
    }

    private void SetActiveFalse()
    {
        PuzzleCreate.Instance.puzzleSelectTabs[0].SetActive(false);
    }
}
