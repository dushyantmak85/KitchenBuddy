using Mono.Cecil;
using UnityEngine;

public interface IKitchenObjectParent 
    
{
    public Transform GetKitchenObjectTransform();



    public void SetKitchenObject(KitchenObjects KitchenObject);



    public KitchenObjects GetKitchenObject();




    public bool KitchenObjectPresent();



    public void ClearKitchenObject();
   

}
