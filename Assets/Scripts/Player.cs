using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }  

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private Vector2 inputVector = Vector2.zero;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 lastInteractDir;
    private Vector3 moveDir = Vector3.zero;
    private float playerRadius = .7f;
    private float playerHeight = 2f;
    private float moveDistance;
    private float interactDistance = 2f;
    private bool isWalking = false;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("there is mor than one player instance");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
        inputVector = gameInput.GetMovementVectorNormalized();

        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        inputVector = gameInput.GetMovementVectorNormalized();

        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = CapsuleRaycastDetect(moveDir, moveDistance);

        if (!canMove)
        {
            //Cannot move toward moveDir

            //Attempt only X movement

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);

            canMove = CapsuleRaycastDetect(moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on the X

                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on the X

                //Attemp only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);

                canMove = CapsuleRaycastDetect(moveDirZ, moveDistance);
                if (canMove)
                {
                    //Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero; //Check player is moving or not

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private bool CapsuleRaycastDetect(Vector3 moveDir, float moveDistance)
    {
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        return canMove;
    }

    private void HandleInteraction()
    {
        inputVector = gameInput.GetMovementVectorNormalized();

        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
        Debug.Log(selectedCounter);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter= selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}