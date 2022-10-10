using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FillMode
{
    NONE,
    FILL,
    XMARK
}

public class GameManager : Singleton<GameManager>
{
    public FillMode fillMode = FillMode.FILL;
    public FillMode clickBoxType = FillMode.FILL;
    public bool isDown = false;

    private void Update()
    {
        if(!Input.GetMouseButton(0))
        {
            isDown = false;
        }
    }
}
