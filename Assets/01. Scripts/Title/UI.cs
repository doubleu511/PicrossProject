using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum ButtonType
{
    HOWTOPLAY,
    SETTING,
    CREDIT,
    DATAREMOVE
}

public class UI : MonoBehaviour
{
    [Header("게임 방법 UI")]
    [SerializeField] private GameObject howToPlayUI = null;
    [SerializeField] private Scrollbar howToPlaySlider = null;
    [Header("설정 UI")]
    [SerializeField] private GameObject settingUI = null;
    [Header("크레딧 UI")]
    [SerializeField] private GameObject creditUI = null;
    [SerializeField] private Scrollbar creditUIScrollBar = null;
    [Header("데이터 지우는거 확인 UI")]
    [SerializeField] private GameObject dataRemoveUI = null;

    public void OnBtnClick(int UIType)
    {
        switch(UIType)
        {
            case (int)ButtonType.HOWTOPLAY:
                UIAppear(howToPlayUI);
                howToPlaySlider.value = 1;
                break;
            case (int)ButtonType.SETTING:
                UIAppear(settingUI);
                break;
            case (int)ButtonType.CREDIT:
                UIAppear(creditUI);
                creditUIScrollBar.value = 1;
                break;
            case (int)ButtonType.DATAREMOVE:
                UIAppear(dataRemoveUI);
                break;
        }
    }

    public void OnCancelBtnClick(int UIType)
    {
        switch (UIType)
        {
            case (int)ButtonType.HOWTOPLAY:
                UIDisappear(howToPlayUI);
                break;
            case (int)ButtonType.SETTING:
                UIDisappear(settingUI);
                break;
            case (int)ButtonType.CREDIT:
                UIDisappear(creditUI);
                break;
            case (int)ButtonType.DATAREMOVE:
                UIDisappear(dataRemoveUI);
                break;
        }
    }

    private void UIAppear(GameObject target)
    {
        targetObj = target;
        List<Image> images = new List<Image>();
        List<Text> texts = new List<Text>();
        target.GetComponentsInChildren<Image>(images);
        target.GetComponentsInChildren<Text>(texts);

        target.SetActive(true);

        images[0].DOFade(0.14f, 0.75f);
        images.RemoveAt(0);

        foreach(Image item in images)
        {
            item.DOFade(1, 0.75f);
        }

        foreach(Text item in texts)
        {
            item.DOFade(1, 0.75f);
        }
    }

    private void UIDisappear(GameObject target)
    {
        targetObj = target;
        List<Image> images = new List<Image>();
        List<Text> texts = new List<Text>();
        target.GetComponentsInChildren<Image>(images);
        target.GetComponentsInChildren<Text>(texts);

        images[0].DOFade(0, 0.75f).OnComplete(UIActiveFalse);
        images.RemoveAt(0);

        foreach (Image item in images)
        {
            item.DOFade(0, 0.75f);
        }

        foreach (Text item in texts)
        {
            item.DOFade(0, 0.75f);
        }
    }

    private GameObject targetObj = null;
    private void UIActiveFalse()
    {
        targetObj.SetActive(false);
    }

    public void DataRemove()
    {
        SaveData.Instance.gameData.ClearData();
        SaveData.Instance.SaveGameData();
        PuzzleCreate.Instance.PuzzleRefresh(1);
        UIDisappear(dataRemoveUI);
    }
}
