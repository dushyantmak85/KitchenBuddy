using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

public class PlateKitchenObject : KitchenObjects
{
   [SerializeField] List<KitchenObjectSO> KitchenObjectSOs;
    private void Awake()
    {
        KitchenObjectSOs = new List<KitchenObjectSO>();
    }

    public bool  TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (KitchenObjectSOs.Contains(kitchenObjectSO))
        {
            Debug.LogError("This ingredient cannot be picked up on plate");
            return false;
        }
        KitchenObjectSOs.Add(kitchenObjectSO);
        return true;
    
    }
}
