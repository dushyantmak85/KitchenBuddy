using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipeNameText;
    [SerializeField] private Transform IconContainer;
    [SerializeField] private Transform IconTemplate;

    private void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }   

    public void SetRecipeSO(KitchenRecipeSO recipeSO)
    {
        if (recipeSO == null)
        {
            RecipeNameText.text = "No Recipe";
            return;
        }
        RecipeNameText.text = recipeSO.RecipeName;

        foreach(Transform child in IconContainer)
        {
            if (child == IconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.RecipeIngredients)
        {
            Transform iconTransform=Instantiate(IconTemplate, IconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }

    }

}
