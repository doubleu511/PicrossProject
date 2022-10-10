using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class PuzzleManager : Singleton<PuzzleManager>
{
    [SerializeField]
    public Array currentBox;
    [SerializeField]
    public Array answerBox;

    [Serializable]
    public struct Array
    {
        public ColArray[] m_colBox;
        public int puzzleSize;
    }

    [Serializable]
    public struct ColArray
    {
        [SerializeField]
        public int[] m_rowBox;
    }

    void Start()
    {
        filePath = string.Format("Assets/Resources/{0}", puzzleNum);
        Debug.Log("filePath : " + filePath);

        currentBox.m_colBox = new ColArray[BoxCreate.Instance.boxSize];
        answerBox.m_colBox = new ColArray[BoxCreate.Instance.boxSize];

        currentBox.puzzleSize = BoxCreate.Instance.boxSize;
        answerBox.puzzleSize = BoxCreate.Instance.boxSize;

        for (int i = 0; i < currentBox.m_colBox.Length; i++)
        {
            currentBox.m_colBox[i].m_rowBox = new int[BoxCreate.Instance.boxSize];
            answerBox.m_colBox[i].m_rowBox = new int[BoxCreate.Instance.boxSize];
        }

        PicLoad();
    }

    private void Update()
    {
        if(hintcol != null & hintrow != null)
        ShowPuzzleHint();
    }

    [HideInInspector]
    public GameObject hintcol = null;
    [HideInInspector]
    public GameObject hintrow = null;

    [ContextMenu("힌트 표시")]
    void ShowPuzzleHint()
    {
        string hintStr = "";
        int count = 0;
        for (int i = 0; i < BoxCreate.Instance.boxSize; i++)
        {
            for (int j = 0; j < BoxCreate.Instance.boxSize; j++)
            {
                if (answerBox.m_colBox[j].m_rowBox[i] == 1)
                    count++;
                else
                {
                    if (count != 0)
                    {
                        hintStr += count.ToString();
                        hintStr += " ";
                    }
                    count = 0;
                }
            }

            if (count != 0)
            {
                hintStr += count.ToString();
                hintStr += " ";
            }
            count = 0;

            if (hintStr == "")
                hintStr = "0 ";
            hintcol.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = hintStr;
            hintStr = "";
        }

        for (int i = 0; i < BoxCreate.Instance.boxSize; i++)
        {
            for (int j = 0; j < BoxCreate.Instance.boxSize; j++)
            {
                if (answerBox.m_colBox[i].m_rowBox[j] == 1)
                    count++;
                else
                {
                    if (count != 0)
                    {
                        hintStr += count.ToString();
                        hintStr += "\n";
                    }
                    count = 0;
                }
            }

            if (count != 0)
            {
                hintStr += count.ToString();
                hintStr += "\n";
            }
            count = 0;
            if (hintStr == "")
                hintStr = "0\n";
            hintrow.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = hintStr;
            hintStr = "";
        }
    }

    [ContextMenu("퍼즐 표시")]
    void ShowPuzzleDebug()
    {
        string puzzleStr = "";
        for (int i = 0; i < BoxCreate.Instance.boxSize; i++)
        {
            for (int j = 0; j < BoxCreate.Instance.boxSize; j++)
            {
                if (answerBox.m_colBox[j].m_rowBox[i] == 0)
                    puzzleStr += "□";
                else if (answerBox.m_colBox[j].m_rowBox[i] == 1)
                    puzzleStr += "■";
            }
            puzzleStr += "\n";
        }

        Debug.Log(puzzleStr);
    }


    private string jsonString = "";
    private string filePath = "";
    [Header("그림 저장시 필요한 것들")]
    [SerializeField] private int puzzleNum = 0;

    public void PuzzleIdSet(int num) => puzzleNum = num;

    [ContextMenu("퍼즐 저장")]
    public void PicSave()
    {
        jsonString = JsonUtility.ToJson(currentBox);
        FileStream fs = new FileStream(filePath, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonString);
        fs.Write(data, 0, data.Length);
        fs.Close();
        Debug.Log("JSON : " + jsonString);
    }

    public void PicLoad()
    {
        //string filePath = string.Format("Assets/Resources/{0}.json",puzzleNum);
        string json = (Resources.Load(puzzleNum.ToString()) as TextAsset).ToString();
/*        FileStream fs = new FileStream(filePath, FileMode.Open);
        Debug.Log(fs.);
        FileLoad(fs);*/

        answerBox = JsonUtility.FromJson<Array>(json);

        if (answerBox.puzzleSize != currentBox.puzzleSize)
        {
            Debug.LogError("사이즈가 맞지 않음");
            answerBox.m_colBox = new ColArray[BoxCreate.Instance.boxSize];
            for (int i = 0; i < currentBox.m_colBox.Length; i++)
            {
                answerBox.m_colBox[i].m_rowBox = new int[BoxCreate.Instance.boxSize];
            }
        }
    }

    private void FileLoad(FileStream fs)
    {
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();
        jsonString = Encoding.UTF8.GetString(data);
        Debug.Log(jsonString);
        answerBox = JsonUtility.FromJson<Array>(jsonString);
    }
}