using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameStart : MonoBehaviour
{
    public Image darkBg = null;
    [SerializeField] GridLayoutGroup col = null;
    [SerializeField] GridLayoutGroup row = null;
    [SerializeField] GridLayoutGroup box = null;
    [SerializeField] Camera mainCamera = null;

    void Awake()
    {
        darkBg.DOFade(0, 1).OnComplete(SetActiveFalse);

        int puzzleId = PlayerPrefs.GetInt("puzzleId");

        GameClear.Instance.puzzleNameTxt.text = SaveData.Instance.gameData.puzzleData[puzzleId].PuzzleName;
        PuzzleManager.Instance.PuzzleIdSet(puzzleId);
        GameClear.Instance.CompletePuzzleImg.sprite = SpriteHub.Instance.puzzleSprites[puzzleId];
        if (puzzleId < 10)
        {
            BoxCreate.Instance.boxSize = 5;
            col.cellSize = new Vector2(300, 150);
            row.cellSize = new Vector2(150, 500);
            box.cellSize = new Vector2(150, 150);
            GameClear.Instance.CompletePuzzleImg.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 750);
        }
        else if (puzzleId < 20)
        {
            BoxCreate.Instance.boxSize = 10;
            col.cellSize = new Vector2(300, 80);
            row.cellSize = new Vector2(80, 500);
            box.cellSize = new Vector2(80, 80);
            GameClear.Instance.CompletePuzzleImg.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 800);
        }
        else if (puzzleId < 30)
        {
            BoxCreate.Instance.boxSize = 15;
            col.cellSize = new Vector2(300, 60);
            row.cellSize = new Vector2(60, 500);
            box.cellSize = new Vector2(60, 60);
            GameClear.Instance.CompletePuzzleImg.GetComponent<RectTransform>().sizeDelta = new Vector2(900, 900);
        }

        int darkmode = PlayerPrefs.GetInt("darkmode");

        if(darkmode == 1)
        {
            ColorReverse();
            mainCamera.backgroundColor = Color.gray;
        }
    }

    private void ColorReverse()
    {
        List<GameObject> objs = new List<GameObject>();

        foreach (GameObject item in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (item.CompareTag("visionModeAffacted"))
            {
                objs.Add(item);
            }
        }

        List<Image> images = new List<Image>();
        List<Text> texts = new List<Text>();

        foreach (GameObject item in objs)
        {
            if (item.GetComponent<Image>() != null)
            {
                images.Add(item.GetComponent<Image>());
            }

            if (item.GetComponent<Text>() != null)
            {
                texts.Add(item.GetComponent<Text>());
            }
        }

        foreach (Image item in images)
        {
            item.color = ColorReverse(item.color);
        }

        foreach (Text item in texts)
        {
            item.color = ColorReverse(item.color);
        }
    }

    public Color ColorReverse(Color target)
    {
        float r = target.r;
        float g = target.g;
        float b = target.b;
        float a = target.a;

        return new Color(-r + 1, -g + 1, -b + 1, a);
    }

    bool isTrigger = false;
    private void Update()
    {
        if (PuzzleManager.Instance.hintcol != null & PuzzleManager.Instance.hintrow != null && !isTrigger)
        {
            isTrigger = true;
            int puzzleId = PlayerPrefs.GetInt("puzzleId");

            if (puzzleId < 10)
            {
                for (int i = 0; i < BoxCreate.Instance.boxSize; i++)
                {
                    PuzzleManager.Instance.hintrow.transform.GetChild(i).GetChild(0).GetComponent<Text>().fontSize = 66;
                    PuzzleManager.Instance.hintcol.transform.GetChild(i).GetChild(0).GetComponent<Text>().fontSize = 66;
                }
            }
            else if (puzzleId < 30)
            {
                for (int i = 0; i < BoxCreate.Instance.boxSize; i++)
                {
                    PuzzleManager.Instance.hintrow.transform.GetChild(i).GetChild(0).GetComponent<Text>().fontSize = 40;
                    PuzzleManager.Instance.hintcol.transform.GetChild(i).GetChild(0).GetComponent<Text>().fontSize = 40;
                }
            }
        }
    }

    private void SetActiveFalse()
    {
        darkBg.gameObject.SetActive(false);
    }
}
