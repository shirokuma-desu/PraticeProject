using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;

    private Vector2 inputVector = Vector2.zero;
    private PlayerInputAction playerInputActions;

    // Start is called before the first frame update
    private void Awake()
    {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Check individual key presses using Input.GetKey
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }

        inputVector.Normalize(); // Normalize the input vector to ensure consistent speed.

        return inputVector;
    }
}
