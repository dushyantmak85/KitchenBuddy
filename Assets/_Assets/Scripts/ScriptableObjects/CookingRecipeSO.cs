using UnityEngine;

[CreateAssetMenu()]

public class CookingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;

    public float CookingTime;
}
