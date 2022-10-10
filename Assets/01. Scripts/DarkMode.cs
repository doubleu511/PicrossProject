using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DarkMode : MonoBehaviour
{
    [SerializeField] private Slider darkSlider = null;

    bool isFirst = false;

    private void Start()
    {
        int darkmode = PlayerPrefs.GetInt("darkmode");

        if(darkmode == 1)
        {
            isFirst = true;
            darkSlider.value = 1;
        }
    }

    public void VisionMode()
    {
        if(darkSlider.value == 0)
        {
            Debug.Log("다크모드 해제");
            PlayerPrefs.SetInt("darkmode", 0);
        }
        else
        {
            Debug.Log("다크모드 설정");
            PlayerPrefs.SetInt("darkmode", 1);
            if(isFirst)
            {
                ColorReverse(true);
                isFirst = false;
                return;
            }
        }
        ColorReverse(false);
    }

    private void ColorReverse(bool isAwake)
    {
        List<GameObject> objs = new List<GameObject>();

        foreach(GameObject item in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if(item.CompareTag("visionModeAffacted"))
            {
                objs.Add(item);
            }
        }

        List<Image> images = new List<Image>();
        List<Text> texts = new List<Text>();

        foreach(GameObject item in objs)
        {
            if(item.GetComponent<Image>() != null)
            {
                images.Add(item.GetComponent<Image>());
            }

            if(item.GetComponent<Text>() != null)
            {
                texts.Add(item.GetComponent<Text>());
            }
        }

        foreach(Image item in images)
        {
            if (isAwake)
                item.color = ColorReverse(item.color);
            else
            {
                item.DOColor(ColorReverse(item.color), 1).OnComplete(DarkSliderActive);
                darkSlider.interactable = false;
            }
        }

        foreach (Text item in texts)
        {
            if (isAwake)
            {
                item.color = ColorReverse(item.color);
            }
            else
            {
                item.DOColor(ColorReverse(item.color), 1).OnComplete(DarkSliderActive);
                darkSlider.interactable = false;
            }
        }
    }

    void DarkSliderActive() => darkSlider.interactable = true;

    [ContextMenu("흑백 반전")]
    public Color ColorReverse(Color target)
    {
        float r = target.r;
        float g = target.g;
        float b = target.b;
        float a = target.a;

        return new Color(-r + 1, -g + 1, -b + 1, a);
    }
}
