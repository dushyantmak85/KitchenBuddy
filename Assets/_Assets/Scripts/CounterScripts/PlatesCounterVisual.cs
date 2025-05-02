using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounterScript platesCounter;
    [SerializeField] private GameObject plateVisual;
    [SerializeField] private Transform CounterTopPoint;
    private List<GameObject> plateVisualGameObjectList;
    private const float plateVisualGameObjectOffsetY = 0.1f;
    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        GameObject plate = Instantiate(plateVisual, CounterTopPoint);
        plate.transform.localPosition = new Vector3(0f,plateVisualGameObjectOffsetY*plateVisualGameObjectList.Count, 0f);
        plateVisualGameObjectList.Add(plate);

    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        if (plateVisualGameObjectList.Count > 0)
        {
            GameObject plate = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
            plateVisualGameObjectList.Remove(plate);
            Destroy(plate);

        }
    }
}
