using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SwitchMode : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject modeSwitch = null;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        ModeSwitch();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModeSwitch();
        }
    }

    void ModeSwitch()
    {
        if (GameManager.Instance.fillMode == FillMode.FILL)
        {
            GameManager.Instance.fillMode = FillMode.XMARK;
            modeSwitch.transform.DOLocalMoveX(163, 0.5f);
        }
        else if (GameManager.Instance.fillMode == FillMode.XMARK)
        {
            GameManager.Instance.fillMode = FillMode.FILL;
            modeSwitch.transform.DOLocalMoveX(-167, 0.5f);
        }
    }
}
