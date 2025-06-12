using UnityEngine;

public class TrashCounterScript : BaseCounter
{
    public static event System.EventHandler OnAnyObjectDestroyed;
    public override void Interact(PlayerController player)
    {
        if (player.KitchenObjectPresent())
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectDestroyed?.Invoke(this, System.EventArgs.Empty);
        }
        else
        {
            Debug.Log("No kitchen object to destroy");
        }
    }
    



}
