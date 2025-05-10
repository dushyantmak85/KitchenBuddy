using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System;

public class PlateCompleteScript : MonoBehaviour
{
   
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<Ingredients> IngredientsList;

    [Serializable]
     public struct Ingredients {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    
    }

    void Start()
    {
      
        plateKitchenObject.OnIngredientAdded += HandleIngredientAdded;
        foreach(Ingredients ingredient in IngredientsList)
        {
            ingredient.gameObject.SetActive(false);
        }


    }

    private void HandleIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (Ingredients ingredient in IngredientsList)
        {
            if (ingredient.kitchenObjectSO == e.kitchenObjectSO)
            {
                ingredient.gameObject.SetActive(true);

            }
            
        }
    }
}
