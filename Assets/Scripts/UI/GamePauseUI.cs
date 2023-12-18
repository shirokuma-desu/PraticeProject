using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.togglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionUI.Instance.Show(Show);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OngamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OngameUnPaused;

        Hide();
    }

    private void GameManager_OngameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OngamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
       
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
