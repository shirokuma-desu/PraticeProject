using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyIntearctAltText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyInteractControllerText;
    [SerializeField] private TextMeshProUGUI keyInteractAltControllerText;
    [SerializeField] private TextMeshProUGUI keyPauseControllerText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyIntearctAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alt);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyInteractControllerText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        keyInteractAltControllerText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact_Alt);
        keyPauseControllerText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

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
