using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSucess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void Instance_OnRecipeFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = failureColor;
        iconImage.sprite = failureSprite;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSucess(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }
}
