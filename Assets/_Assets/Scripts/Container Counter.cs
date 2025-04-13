using System;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerCounter :BaseCounter
{


    [SerializeField] private KitchenObjectSO KitchenObjectSO;

    public  event EventHandler OnInteractionWithContainer;



    public override void Interact(PlayerController player)
    {
        if (!player.KitchenObjectPresent())
        {
            Transform KitchenObjectTransform = Instantiate(KitchenObjectSO.prefab);
            KitchenObjectTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(player);
            OnInteractionWithContainer?.Invoke(this, EventArgs.Empty);
        }

    }



}
