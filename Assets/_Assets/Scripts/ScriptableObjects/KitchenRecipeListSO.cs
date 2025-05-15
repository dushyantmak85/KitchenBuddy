using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu]
public class KitchenRecipeListSO : ScriptableObject
{
    [SerializeField] private List<KitchenRecipeSO> KitchenRecipeList;

    
}
