
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour,IKitchenObjectParent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float movespeed=7f;
    [SerializeField] private GameObject PlayerVisual;
    [SerializeField] private GameInput input;
    [SerializeField] private Transform KitchenObjectOnHold;

    private KitchenObjects KitchenObject;

    float playerHeight = 0.5f;
    private bool IsWalking;
    private BaseCounter SelectedCounter;

    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    public static PlayerController instanceField;
    public static PlayerController GetinstanceField()
    {
        return instanceField;
    }

    public static void SetInstanceField(PlayerController instanceField)
    {
        PlayerController.instanceField = instanceField;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerController instance in the scene.");
        }
            Instance = this;
    }




    private void Start()
    {
       
        input.OnInteract += GameInput_OnInteraction;
        input.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if(SelectedCounter!=null)
        {
            SelectedCounter.Interact(this); 
        }
        if(SelectedCounter == null)
        {
            Debug.Log("No Counter Selected");
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.OnInteractCutAction(this);
        }
        if (SelectedCounter == null)
        {
            Debug.Log("No Counter Selected");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleIntractions();        
        
    }





    private void HandleIntractions()
    {
        float interactionRadius =1f; // Adjust this based on how far you want the interaction to work

        Collider[] colliders = Physics.OverlapSphere(PlayerVisual.transform.position, interactionRadius);

        SelectedCounter = null; // Reset each frame

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out BaseCounter baseCounter))
            {
                SetSelectedCounter(baseCounter);
                break; // Stop once we find the first valid ClearCounter
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        
    }



    private void HandleMovement()
    {
        Vector2 inputVector = input.GetNormalizedVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        float moveDistance = movespeed * Time.deltaTime;
        float rayLength = moveDistance + 0.1f; // Add buffer
        float rayOffset = 0.5f; // How far in front of the player the ray starts
        int layerMask = ~(LayerMask.GetMask("Default"));

        Vector3 rayOrigin = transform.position + moveDir * rayOffset + Vector3.up * playerHeight; // slightly above ground




        bool canMove = !Physics.Raycast(rayOrigin, moveDir, rayLength, layerMask);

        if (!canMove)
        {
            // Try moving only in X direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            rayOrigin = transform.position + moveDirX * rayOffset + Vector3.up * 0.5f;

            canMove = moveDir.x != 0 && !Physics.Raycast(rayOrigin, moveDirX, rayLength, layerMask);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Try moving only in Z direction
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                rayOrigin = transform.position + moveDirZ * rayOffset + Vector3.up * 0.5f;
             

                canMove = moveDir.z != 0 && !Physics.Raycast(rayOrigin, moveDirZ, rayLength, layerMask);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * movespeed * Time.deltaTime;
        }

        IsWalking = moveDir != Vector3.zero;

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            PlayerVisual.transform.rotation = Quaternion.RotateTowards(PlayerVisual.transform.rotation, toRotation, 500f * Time.deltaTime);
        }
        Debug.DrawRay(rayOrigin, moveDir * rayLength, Color.red, 0f);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        SelectedCounter = selectedCounter;
        OnSelectedCounterChanged.Invoke(this, new OnSelectedCounterChangedEventArgs { SelectedCounter = SelectedCounter });


    }

    public bool Iswalking()
    {
        return IsWalking;
    }





    public Transform GetKitchenObjectTransform()
    {
        return KitchenObjectOnHold;
    }

    public void SetKitchenObject(KitchenObjects KitchenObject)
    {
        this.KitchenObject = KitchenObject;
    }

    public KitchenObjects GetKitchenObject()
    {
        return KitchenObject;
    }


    public bool KitchenObjectPresent()
    {
        return KitchenObject != null;
    }

    public void ClearKitchenObject()
    {

        KitchenObject = null;
    }

}
