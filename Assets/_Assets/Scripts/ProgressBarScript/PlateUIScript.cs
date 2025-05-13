using UnityEngine;

public class PlateUIScript : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform IconTemplate;

    private void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += UnableIcon;

        
    }

    private void UnableIcon(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(Transform childObject in transform)
        {
            if (childObject == IconTemplate) continue;
            Destroy(childObject.gameObject);
        }
        UpdateVisual();


    }

    private void UpdateVisual()
    {
        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.ListOfKitchenObjects())
        {
            Transform IconTransform=Instantiate(IconTemplate, transform);
            IconTransform.gameObject.SetActive(true);
            IconTransform.GetComponent<PlateSingleIconUI>().SetKitchenObjectSO(kitchenObjectSO);
          
        }
    }
}
