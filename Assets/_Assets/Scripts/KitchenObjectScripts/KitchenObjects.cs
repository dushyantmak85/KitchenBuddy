using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] KitchenObjectSO KitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return KitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObjectParent.KitchenObjectPresent())
        {
            Debug.LogError("IkitchenObject already have a parent!!");
            return;
        }

        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);

    }

    public static KitchenObjects SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObjects kitchenObject = KitchenObjectTransform.GetComponent<KitchenObjects>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;

    }

    public  bool tryGetPlate(out PlateKitchenObject platesCounter)
    {
        if (this is PlateKitchenObject)
        {
            platesCounter = this as PlateKitchenObject;
            return true;
        }
        else
        {
            platesCounter = null;
            return false;
        }
    }
}
