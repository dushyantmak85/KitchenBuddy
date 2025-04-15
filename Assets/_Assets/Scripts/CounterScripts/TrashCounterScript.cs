using UnityEngine;

public class TrashCounterScript : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if (player.KitchenObjectPresent())
        {
            player.GetKitchenObject().DestroySelf();
        }
        else
        {
            Debug.Log("No kitchen object to destroy");
        }
    }
    



}
