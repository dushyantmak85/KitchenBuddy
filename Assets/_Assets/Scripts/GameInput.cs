using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerinput;
    public  event EventHandler OnInteract;
    public event EventHandler OnInteractAlternateAction;


    private void Awake()
    { 
        playerinput = new PlayerInputActions();
        playerinput.player.Enable();
        playerinput.player.Interact.performed += Interact_performed;
        playerinput.player.OnInteractAlternateAction.performed += InteractAlternate_performed;

    }
    public void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 

    {
      
        OnInteract?.Invoke(this, EventArgs.Empty);  

    }

    public void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetNormalizedVector()
    {

        Vector2 newVector = playerinput.player.Move.ReadValue<Vector2>();


        return newVector.normalized;
    }
}
