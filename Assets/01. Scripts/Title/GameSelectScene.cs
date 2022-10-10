using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSelectScene : MonoBehaviour
{
    [SerializeField] private RectTransform mainScene = null;
    [SerializeField] private GameObject darkBg = null;

    [ContextMenu("SkipIntro")]
    public void SkipIntro()
    {
        mainScene.DOLocalMoveX(0, 0);
        transform.DOLocalMoveX(0, 0);
    }

    public Ease ease = Ease.Unset;
    [ContextMenu("game")]
    public void GameSelect()
    {
        mainScene.DOLocalMoveX(-1920, 2).SetEase(ease);
        transform.DOLocalMoveX(0, 2).SetEase(ease);
    }

    private void Update()
    {
        int skipIntro = PlayerPrefs.GetInt("skipIntro");

        if(skipIntro == 1)
        {
            int puzzleId = PlayerPrefs.GetInt("puzzleId");
            SkipIntro();
            PuzzleCreate.Instance.PuzzleRefresh((puzzleId / 10) + 1);
            PlayerPrefs.SetInt("skipIntro", 0);
            darkBg.SetActive(true);
            darkBg.GetComponent<Image>().DOFade(1, 0);
            darkBg.GetComponent<Image>().DOFade(0, 1).OnComplete(DisableBg);
        }
    }

    private void DisableBg()
    {
        darkBg.SetActive(false);
    }
}
