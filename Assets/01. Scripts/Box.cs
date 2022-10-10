using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image objImage = null;

    void Awake()
    {
        objImage = GetComponent<Image>();
        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (objImage.sprite == SpriteHub.Instance.sprite_box)
            {
                GameManager.Instance.clickBoxType = FillMode.NONE;
            }
            else if (objImage.sprite == SpriteHub.Instance.sprite_boxX)
            {
                GameManager.Instance.clickBoxType = FillMode.XMARK;
            }
            else if (objImage.sprite == SpriteHub.Instance.sprite_boxFill)
            {
                GameManager.Instance.clickBoxType = FillMode.FILL;
            }
            ClickEvent();
            GameManager.Instance.isDown = true;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0)) {
            if (GameManager.Instance.isDown)
            {
                ClickEvent();
            }
        }
        else {
            ExitEvent();
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ExitEvent();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        ExitEvent();
        GameManager.Instance.clickBoxType = FillMode.NONE;
    }

    void ClickEvent()
    {
        objImage.color = new Color(0.8f, 0.8f, 0.8f);

        if (GameManager.Instance.fillMode == FillMode.XMARK)
        {
            if (objImage.sprite == SpriteHub.Instance.sprite_box && GameManager.Instance.clickBoxType == FillMode.NONE)
            {
                objImage.sprite = SpriteHub.Instance.sprite_boxX;
                SavePuzzle(FillMode.XMARK);
            }
            else if (objImage.sprite == SpriteHub.Instance.sprite_boxX && GameManager.Instance.clickBoxType == FillMode.XMARK)
            {
                objImage.sprite = SpriteHub.Instance.sprite_box;
                SavePuzzle(FillMode.NONE);
            }
            else if (objImage.sprite == SpriteHub.Instance.sprite_boxFill && GameManager.Instance.clickBoxType == FillMode.FILL)
            {
                objImage.sprite = SpriteHub.Instance.sprite_boxX;
                SavePuzzle(FillMode.XMARK);
            }
        }
        else if(GameManager.Instance.fillMode == FillMode.FILL)
        {
            if (objImage.sprite == SpriteHub.Instance.sprite_box && GameManager.Instance.clickBoxType == FillMode.NONE)
            {
                objImage.sprite = SpriteHub.Instance.sprite_boxFill;
                SavePuzzle(FillMode.FILL);
            }
            else if (objImage.sprite == SpriteHub.Instance.sprite_boxX && GameManager.Instance.clickBoxType == FillMode.XMARK)
            {
                objImage.sprite = SpriteHub.Instance.sprite_boxFill;
                SavePuzzle(FillMode.FILL);
            }
           else if (objImage.sprite == SpriteHub.Instance.sprite_boxFill && GameManager.Instance.clickBoxType == FillMode.FILL)
            {
                objImage.sprite = SpriteHub.Instance.sprite_box;
                SavePuzzle(FillMode.NONE);
            }
        }

        ClearCheck();
    }

    void ExitEvent()
    {
        objImage.color = Color.white;
    }

    void SavePuzzle(FillMode what)
    {
        int col = int.Parse(gameObject.name) % BoxCreate.Instance.boxSize;
        int row = int.Parse(gameObject.name) / BoxCreate.Instance.boxSize;

        switch (what)
        {
            case FillMode.FILL:
                PuzzleManager.Instance.currentBox.m_colBox[col].m_rowBox[row] = 1;
                break;
            case FillMode.XMARK:
            case FillMode.NONE:
                PuzzleManager.Instance.currentBox.m_colBox[col].m_rowBox[row] = 0;
                break;
        }
    }

    void ClearCheck()
    {
        for (int i = 0; i < BoxCreate.Instance.boxSize; i++)
        {
            for (int j = 0; j < BoxCreate.Instance.boxSize; j++)
            {
                if (PuzzleManager.Instance.answerBox.m_colBox[i].m_rowBox[j] != PuzzleManager.Instance.currentBox.m_colBox[i].m_rowBox[j])
                {
                    return;
                }
            }
        }
        Debug.Log("³¡!");
        GameClear.Instance.Clear();
    }
}
