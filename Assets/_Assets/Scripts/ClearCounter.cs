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
                Debug.Log("Player already has a kitchen object");

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
    }
      

 

}
