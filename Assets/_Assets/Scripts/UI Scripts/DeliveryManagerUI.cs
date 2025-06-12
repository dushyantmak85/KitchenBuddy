using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform Container;
    [SerializeField] private Transform KitchenTemplate;

    private void Awake()
    {
        KitchenTemplate.gameObject.SetActive(false);

    }

    private void Start()
    {
        DeliveryManager.Instance.RecipeInWaiting += HandleRecipeInWaiting;
        DeliveryManager.Instance.RecipeWaitingEnd += Delivery_RecipeWaitingEnd;
        UpdateVisual();
    }

    private void Delivery_RecipeWaitingEnd(object sender, System.EventArgs e)
    {
        UpdateVisual();   
    }

    private void HandleRecipeInWaiting(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in Container)
        {
            if (child == KitchenTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenRecipeSO recipeSO in DeliveryManager.Instance.GetKitchenRecipeListSO())
        {
            Transform transform = Instantiate(KitchenTemplate, Container);
            transform.gameObject.SetActive(true);
            transform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }




}
