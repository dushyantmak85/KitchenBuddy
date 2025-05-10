using System;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingProgress;
    public  event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    

    public override void Interact(PlayerController player)
    {
        if (!KitchenObjectPresent())
        {
            if (player.KitchenObjectPresent())
            {
                if (HasKitchenRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {

                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
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
                if (player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

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

    public override void OnInteractCutAction(PlayerController player)
    {
        if (KitchenObjectPresent() && HasKitchenRecipeInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            OnCut?.Invoke(this, EventArgs.Empty);
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GettingCuttingRecipeSOwithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.Cuttingprogress

            });
            if (cuttingProgress >= cuttingRecipeSO.Cuttingprogress)
            {
                KitchenObjectSO outputKitchenObjectSO = CutKitchenObject(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObjects.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
          

      
        }

    }

    public KitchenObjectSO CutKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GettingCuttingRecipeSOwithInput(kitchenObjectSO);
        if (cuttingRecipeSO == null)
        {
            Debug.LogError("Cutting recipe not found for the given kitchen object.");
            return null;
        }

        return cuttingRecipeSO.output;
            
     

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

    public CuttingRecipeSO GettingCuttingRecipeSOwithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOs)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

}
