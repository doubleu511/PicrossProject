using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameClear : Singleton<GameClear>
{
    [SerializeField] private GameObject Boxes = null;
    [SerializeField] private Text StageClearText = null;
    [SerializeField] private Image CheckBtn = null;
    public Text puzzleNameTxt = null;
    [SerializeField] private GameObject CompletePuzzle = null;
    [SerializeField] private Image leaveBtn = null;
    [SerializeField] private GameObject modeSwitch = null;

    public List<Image> colors = new List<Image>();
    public List<Text> txtColors = new List<Text>();
    public HintManager[] hintManagers = null;

    public Image CompletePuzzleImg = null;

    public void Clear()
    {
        leaveBtn.GetComponent<Button>().interactable = false;
        leaveBtn.DOFade(0, 1);
        modeSwitch.GetComponent<SwitchMode>().enabled = false;

        hintManagers = FindObjectsOfType<HintManager>();
        Image[] hint1 = hintManagers[0].GetComponentsInChildren<Image>();
        Image[] hint2 = hintManagers[1].GetComponentsInChildren<Image>();
        Image[] switchImgs = modeSwitch.GetComponentsInChildren<Image>();

        int puzzleId = PlayerPrefs.GetInt("puzzleId");

        SaveData.Instance.gameData.puzzleData[puzzleId].IsCleared = true;
        SaveData.Instance.SaveGameData();

        for(int i = 0;i<hint1.Length;i++)
        {
            hint1[i].DOFade(0, 1);
        }

        for (int i = 0; i < hint2.Length; i++)
        {
            hint2[i].DOFade(0, 1);
        }

        foreach(Image item in switchImgs)
        {
            item.DOFade(0, 1);
        }

        Text[] _hint1 = hintManagers[0].GetComponentsInChildren<Text>();
        Text[] _hint2 = hintManagers[1].GetComponentsInChildren<Text>();

        for (int i = 0; i < _hint1.Length; i++)
        {
            _hint1[i].DOFade(0, 1);
        }

        for (int i = 0; i < _hint2.Length; i++)
        {
            _hint2[i].DOFade(0, 1);
        }

        Boxes.transform.DOLocalRotate(new Vector3(0, 180, 0),1.5f);
        CompletePuzzle.SetActive(true);
        CompletePuzzleImg.color = new Color(1, 1, 1, 0);
        CompletePuzzle.transform.DOLocalRotate(new Vector3(0, 360, 0), 1.5f).OnComplete(Align);
        CheckBtn.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if(Boxes.transform.localEulerAngles.y >= 90)
        {
            Boxes.SetActive(false);
            CompletePuzzleImg.color = new Color(1, 1, 1, 1);
        }
    }

    private void Align()
    {
        CompletePuzzle.transform.DOLocalMoveX(0, 0.5f).OnComplete(TxtAppear);
    }

    private void TxtAppear()
    {
        StageClearText.DOFade(1, 1f);
        puzzleNameTxt.DOFade(1, 1f);
        CheckBtn.DOFade(1, 0.5f).OnComplete(BtnActive);
    }

    private void BtnActive() => CheckBtn.GetComponent<Button>().interactable = true;
}
