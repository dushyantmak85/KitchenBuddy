using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class PlateKitchenObject : KitchenObjects
{
   [SerializeField] List<KitchenObjectSO> validKitchenObjectSOs;
     List<KitchenObjectSO> KitchenObjectSOs;
    public  event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Awake()
    {
        KitchenObjectSOs = new List<KitchenObjectSO>();
    }

    public bool  TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOs.Contains(kitchenObjectSO))
        {
            Debug.LogError("This ingredient cannot be picked up on plate");
            return false;
        }

        if (KitchenObjectSOs.Contains(kitchenObjectSO))
        {
            Debug.LogError("Already has the object !!");
            return false;
        }
        KitchenObjectSOs.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
            kitchenObjectSO = kitchenObjectSO
        });
       
        return true;
        
    
    }

    public List<KitchenObjectSO> ListOfKitchenObjects()
    {
        return KitchenObjectSOs;
    }
}
