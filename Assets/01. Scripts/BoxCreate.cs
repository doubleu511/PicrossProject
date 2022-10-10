using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCreate : Singleton<BoxCreate>
{
    [SerializeField] private GameObject box = null;
    [SerializeField] private GameObject firstBox = null;
    [SerializeField] private GameObject hints = null;
    [SerializeField] private GameObject hintCol = null;
    [SerializeField] private GameObject hintRow = null;
    public int boxSize = 5;

    void Awake()
    {
        int boxSizeSquare = boxSize * boxSize;
        GetComponent<GridLayoutGroup>().constraintCount = boxSize;


        for(int i = 0;i<boxSizeSquare;i++)
        {
            GameObject boxOne = Instantiate(box, this.transform);
            if (i == 0) {
                firstBox = boxOne;
            }
            boxOne.name = i.ToString();
        }
    }

    private void Start()
    {
        
    }

    bool isTrigger = false;
    private void Update()
    {
        if (firstBox.GetComponent<RectTransform>().anchoredPosition != new Vector2(0, 0) && !isTrigger)
        {
            isTrigger = true;
            GameObject col = Instantiate(hintCol, hints.transform);
            col.transform.position = firstBox.transform.position;
            GameObject row = Instantiate(hintRow, hints.transform);
            row.transform.position = firstBox.transform.position;

            PuzzleManager.Instance.hintcol = col;
            PuzzleManager.Instance.hintrow = row;
        }
    }
}
