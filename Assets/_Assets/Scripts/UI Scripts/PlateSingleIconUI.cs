using UnityEngine;
using UnityEngine.UI;

public class PlateSingleIconUI : MonoBehaviour
{

    [SerializeField] private Image Iconimage;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        Iconimage.sprite = kitchenObjectSO.sprite;

    }
}
