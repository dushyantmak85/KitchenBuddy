using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu]

public class KitchenRecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> RecipeIngredients;
    public string RecipeName;   

}
