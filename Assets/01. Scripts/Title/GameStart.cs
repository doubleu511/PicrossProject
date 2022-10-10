using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum WHATISME
{
    WORLD,
    BUTTON,
    TEXT
}

public class GameStart : MonoBehaviour
{
    [SerializeField] private Image brightBg = null;
    [SerializeField] private Text titleNameTxt = null;
    [SerializeField] private Text subNameTxt = null;
    [SerializeField] private GameObject boxParent = null;

    [SerializeField] private WHATISME owner = WHATISME.WORLD;

    private void Start()
    {
        if (owner == WHATISME.WORLD)
        {
            brightBg.DOFade(0, 0);
            brightBg.DOFade(1, 3).SetDelay(2);

            for (int i = 0; i < boxParent.transform.childCount; i++)
            {
                boxParent.transform.GetChild(i).GetComponent<Image>().DOFade(0, 0);
                boxParent.transform.GetChild(i).GetComponent<Image>().DOFade(1, 3).SetDelay(3 + 0.1f * i);
                boxParent.transform.GetChild(i).DOLocalRotate(new Vector3(0, 180, 0), 1.5f).SetDelay(3 + 0.1f * i);
            }

            titleNameTxt.DOFade(0, 0);
            titleNameTxt.DOFade(1, 1).SetDelay(5);
            titleNameTxt.gameObject.GetComponent<RectTransform>().DOAnchorPosY(-1000, 2).SetDelay(4.5f);

            subNameTxt.DOFade(0, 0);
            subNameTxt.DOFade(1, 1).SetDelay(5);
        }
        else if(owner == WHATISME.BUTTON)
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().DOFade(0, 0);
            transform.GetChild(0).GetComponent<Text>().DOFade(0, 0);
            GetComponent<Image>().DOFade(1f, 2).SetDelay(5);
            transform.GetChild(0).GetComponent<Text>().DOFade(1, 2).SetDelay(6);

            Invoke("ButtonActive", 6);
        }
        else if (owner == WHATISME.TEXT)
        {
            GetComponent<Text>().DOFade(0, 0);
            GetComponent<Text>().DOFade(1, 1).SetDelay(5);
        }
    }

    void ButtonActive()
    {
        GetComponent<Button>().interactable = true;
    }
}
