using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugEnum : MonoBehaviour
{
    [SerializeField] Text debugText = null;
    private void Update()
    {
        

        ShowPuzzleDebug();
    }

    [ContextMenu("∆€¡Ò «•Ω√")]
    void ShowPuzzleDebug()
    {
        string puzzleStr = "";
        for (int i = 0; i < BoxCreate.Instance.boxSize; i++)
        {
            for (int j = 0; j < BoxCreate.Instance.boxSize; j++)
            {
                if (PuzzleManager.Instance.currentBox.m_colBox[j].m_rowBox[i] == 0)
                    puzzleStr += "0";
                else if (PuzzleManager.Instance.currentBox.m_colBox[j].m_rowBox[i] == 1)
                    puzzleStr += "1";
                else if (PuzzleManager.Instance.currentBox.m_colBox[j].m_rowBox[i] == 2)
                    puzzleStr += "2";
            }
            puzzleStr += "\n";
        }

        debugText.text = string.Format("Mode : {0},\nTarget: {1}\n\n{2}", GameManager.Instance.fillMode, GameManager.Instance.clickBoxType,puzzleStr);
    }
}
