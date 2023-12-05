using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnpaused;
        UpdateVisual();

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Efects: "+ Mathf.Round(SoundManager.Instance.GetVolume()*10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
