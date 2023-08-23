using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
       if(e.selectedCounter == clearCounter)
        {
            ShowObject();
        }
        else
        {
            HideObject();
        }
    }

    private void ShowObject()
    {
        visualGameObject.SetActive(true);
    }
    private void HideObject()
    {
        visualGameObject.SetActive(false);
    }
}
