using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPickUp;
    public event EventHandler OnFootStep;
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform holdPoint;
    private BaseCounter selectedCounter;
    private bool isWalking;
    private Vector3 lastInteractDir;
    private KitchenObject kitchenObject;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There are more than one player");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this); 
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteract();
    }
    private void HandleInteract()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit rayCastHit, interactDistance, counterLayerMask))
        {
            if(rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if(baseCounter != selectedCounter )
                {
                    GetSelectedCounter(baseCounter);
                }
            }
            else
            {
                GetSelectedCounter(null);
            }
           
        } 
        else
        {
            GetSelectedCounter(null);
        }
      
        
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float playerRadius = .7f;
        float moveDistance = Time.deltaTime * moveSpeed;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
                if (moveDir != Vector3.zero)
                {
                    OnFootStep?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                    if (moveDir != Vector3.zero)
                    {
                        OnFootStep?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {

                }
            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
            if (moveDir != Vector3.zero)
            {
                OnFootStep?.Invoke(this, EventArgs.Empty);
            }
           
        }
        isWalking = moveDir != Vector3.zero;
        float speedRotation = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * speedRotation);
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void GetSelectedCounter(BaseCounter baseCounter)
    {
        selectedCounter = baseCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return holdPoint;
    }
    public void SetKichenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        OnPickUp?.Invoke(this, EventArgs.Empty);
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
