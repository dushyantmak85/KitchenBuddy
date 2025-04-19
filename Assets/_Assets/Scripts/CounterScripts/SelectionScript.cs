using UnityEditor.Search;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    [SerializeField] private BaseCounter Counter;
    [SerializeField] private GameObject[] VisualGameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }
        else
        {
            Debug.LogError("PlayerController.Instance is null. Make sure PlayerController is present in the scene.");
        }
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        
            if (e.SelectedCounter == Counter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        
      
    }
    private void Show()
    {
        foreach (GameObject gameobject in VisualGameObject)
        {

            gameobject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject gameobject in VisualGameObject)
        {

            gameobject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.OnSelectedCounterChanged -= Player_OnSelectedCounterChanged;
        }
    }


    // Update is called once per frame

}
