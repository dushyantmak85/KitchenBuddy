using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
  
    [SerializeField] private Transform CounterTopPoint;
    private KitchenObjects KitchenObject;
    public static event EventHandler OnAnyObjectPlacedOnCounter;
    public Transform GetKitchenObjectTransform()
    {
        return CounterTopPoint;
    }

    public void SetKitchenObject(KitchenObjects KitchenObject)
    {
        this.KitchenObject = KitchenObject;
        OnAnyObjectPlacedOnCounter?.Invoke(this, EventArgs.Empty);
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

  public virtual void Interact(PlayerController player)
    {
        Debug.Log("Interacting with base counter");
    }

    public virtual void OnInteractCutAction(PlayerController player)
    {
        Debug.Log("Interacting with base counter");
    }
}
