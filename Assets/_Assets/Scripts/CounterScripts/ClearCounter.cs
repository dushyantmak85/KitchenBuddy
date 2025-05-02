using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;






    public override void Interact(PlayerController player)
    {
        if (!KitchenObjectPresent())
        {
            if (player.KitchenObjectPresent())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            };
        }
        else
        {
            if (player.KitchenObjectPresent())
            {
                if (player.GetKitchenObject() is PlateKitchenObject)
                {
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                    }
        

                }

            }

           
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
    }
      

 

}
