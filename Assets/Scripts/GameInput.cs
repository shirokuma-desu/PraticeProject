using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractActionAlternate;
    public event EventHandler OnPauseAction;

    private Vector2 inputVector = Vector2.zero;
    private PlayerInputAction playerInputActions;

    // Start is called before the first frame update
    private void Awake()
    {   
        instance = this;
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_perfromed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_perfromed;

        playerInputActions.Dispose();
    }

    private void Pause_perfromed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       OnInteractActionAlternate?.Invoke(this,EventArgs.Empty); 
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
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