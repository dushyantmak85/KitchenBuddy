using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
     
    public override void Interact(PlayerController player)
    {
        if (!KitchenObjectPresent())
        {
            if (player.KitchenObjectPresent())
            {
                if (HasKitchenRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {

                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else
                {
                    Debug.Log("Player does not have a kitchen object with cutting recipe");
                }
            }
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

    public override void OnInteractCutAction(PlayerController player)
    {
        if (KitchenObjectPresent() && HasKitchenRecipeInput(GetKitchenObject().GetKitchenObjectSO()))
        {
           
           KitchenObjectSO outputKitchenObjectSO=CutKitchenObject(GetKitchenObject().GetKitchenObjectSO());
           GetKitchenObject().DestroySelf();    
           KitchenObjects.SpawnKitchenObject(outputKitchenObjectSO, this);
        }

    }

    public KitchenObjectSO CutKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in  cuttingRecipeSOs)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO)
            {
                Debug.Log("Cutting " + kitchenObjectSO.objectName + " into " + cuttingRecipeSO.output.objectName);
        
               
                return cuttingRecipeSO.output;
            }
        }
        Debug.Log("No recipe found for " + kitchenObjectSO.objectName);
        return null ;

    }

    private bool HasKitchenRecipeInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOs)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }


}
