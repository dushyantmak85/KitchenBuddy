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
                if (player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                    }
                }
                else
                {
                    if (GetKitchenObject().tryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }

                    }
                    else
                    {
                        Debug.LogError("Clear Counter already has an kitchenObject!!");
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
