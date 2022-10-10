using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public GameObject hint_p = null;

    private void OnEnable()
    {
        for(int i = 0;i<BoxCreate.Instance.boxSize;i++)
        {
            GameObject hintBg = Instantiate(hint_p, this.transform);
            hintBg.name = i.ToString();
        }
    }
}
