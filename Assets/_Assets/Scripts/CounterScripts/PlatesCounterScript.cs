using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatesCounterScript : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO platekitchenObjectSO;

    private float SpawnTimer;
    private float SpawnTimerMax = 4f;
    private int platesSpawnedAmount = 0;

    private void Update()
    {
        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnTimerMax && platesSpawnedAmount != 4)
        {
            SpawnTimer = 0f;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            platesSpawnedAmount++;
        }
    }

    public override void Interact(PlayerController player)
    {
        if (!player.KitchenObjectPresent() && platesSpawnedAmount>0)
        {
            Transform KitchenObjectTransform = Instantiate(platekitchenObjectSO.prefab);
            KitchenObjectTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            platesSpawnedAmount--;
        }
        else
        {
            Debug.Log("No kitchen object present");
        }
    }
}
