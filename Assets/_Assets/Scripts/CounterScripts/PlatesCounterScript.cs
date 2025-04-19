using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatesCounterScript : BaseCounter
{
    private float SpawnTimer;
    private float SpawnTimerMax = 4f;
    [SerializeField] private KitchenObjectSO KitchenObjectSO;

    private void Update()
    {
        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnTimerMax)
        {
            SpawnTimer = 0f;
            
               KitchenObjects.SpawnKitchenObject(KitchenObjectSO, this);
            
        }
    }
}
