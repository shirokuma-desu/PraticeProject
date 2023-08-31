using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress = hasProgressObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError("game obj" + hasProgressObject + "does not have IHasProgress component");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
       gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
