using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private KitchenRecipeListSO RecipeList;
    private List<KitchenRecipeSO> waitingRecipeSOList;

    public  event EventHandler RecipeInWaiting;
    public  event EventHandler RecipeWaitingEnd;

    private int waitingRecipeMax = 4;
    private float DeliverRecipeCounter;
    private float DeliverRecipeCounterMax = 4f;
    public static  DeliveryManager Instance { get; private set; }
   

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<KitchenRecipeSO>();
    }

    private void Update()
    {
        DeliverRecipeCounter -= Time.deltaTime;
        if (DeliverRecipeCounter <= 0f)
        {
            DeliverRecipeCounter = DeliverRecipeCounterMax;
            // Fixing CS1955: Accessing the list property correctly  
            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                KitchenRecipeSO waitingRecipe = RecipeList.KitchenRecipeList[UnityEngine.Random.Range(0, RecipeList.KitchenRecipeList.Count)];
                Debug.Log(waitingRecipe.RecipeName);
                waitingRecipeSOList.Add(waitingRecipe);
                RecipeInWaiting?.Invoke(this,EventArgs.Empty);

            }
            

        }
    }

    public void CheckIngredients(PlateKitchenObject plateKitchenObject)
    {
        for (int i=0; i < waitingRecipeSOList.Count; i++){
            KitchenRecipeSO waitingrecipe= waitingRecipeSOList[i];
            if (waitingrecipe.RecipeIngredients.Count == plateKitchenObject.ListOfKitchenObjects().Count)
            {
                bool KitchenObjectContentMatches = true; // number of items in the plate is equal to the number items in the waitingRecipe
                foreach (KitchenObjectSO kitchenRecipe in waitingrecipe.RecipeIngredients)
                {
                    bool IngredientFound = false;
                    foreach (KitchenObjectSO PlateKitchenIngredients in plateKitchenObject.ListOfKitchenObjects())
                    {
                        if (kitchenRecipe == PlateKitchenIngredients)
                        {
                            IngredientFound = true;
                            break;
                        }
                    }
                    if (!IngredientFound)
                    {
                        KitchenObjectContentMatches = false;
                    }
                }

                if (KitchenObjectContentMatches)
                {
                    Debug.Log("Successfully delivered the required order ");
                    waitingRecipeSOList.Remove(waitingrecipe);
                    RecipeWaitingEnd?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }          

        }

        Debug.Log("Plate doesn't contain the required recipe!!");
     
        return;
    }

    public List<KitchenRecipeSO> GetKitchenRecipeListSO()
    {
       return waitingRecipeSOList;
    }
}
