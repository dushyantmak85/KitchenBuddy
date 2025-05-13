using UnityEngine;

public class DeliveryCounterVisual : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if (player.KitchenObjectPresent())
        {
            if(player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                player.GetKitchenObject().DestroySelf();
            }
        }


    }
    
}
