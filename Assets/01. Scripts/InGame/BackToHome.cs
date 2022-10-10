using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BackToHome : MonoBehaviour
{
    [SerializeField] private GameObject Blackbg = null;
    [SerializeField] private GameObject leaveUI = null;

    public void OnClick()
    {
        PlayerPrefs.SetInt("skipIntro", 1);
        Blackbg.SetActive(true);
        Blackbg.GetComponent<Image>().DOFade(1, 1f).OnComplete(ChangeScene);
    }

    public void OnDoorClick()
    {
        leaveUI.SetActive(true);

        List<Image> images = new List<Image>();

        leaveUI.GetComponentsInChildren<Image>(images);
        Text remainText = leaveUI.GetComponentInChildren<Text>();

        images[0].DOFade(0.7f, 1);
        images.RemoveAt(0);

        foreach (Image item in images)
        {
            item.DOFade(1, 1);
        }
        remainText.DOFade(1, 1);
    }

    public void OnCancelClick()
    {
        List<Image> images = new List<Image>();

        leaveUI.GetComponentsInChildren<Image>(images);
        Text remainText = leaveUI.GetComponentInChildren<Text>();

        foreach (Image item in images)
        {
            item.DOFade(0, 1);
        }
        remainText.DOFade(0, 1).OnComplete(LeaveUIDisappear);
    }

    private void LeaveUIDisappear() => leaveUI.SetActive(false);

    private void ChangeScene()
    {
        SceneManager.LoadScene("Title");
    }
}
