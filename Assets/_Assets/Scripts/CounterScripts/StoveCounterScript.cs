using System;
using UnityEngine;
using static CuttingCounter;

public class StoveCounterScript : BaseCounter
{
    [SerializeField] private CookingRecipeSO[] cookingRecipeSOs;
    [SerializeField] private BurningRecipeSO[] BurningRecipeSOs;

    private float Cookingtimer;
    private float burningTimer;
    private CookingRecipeSO cookingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }   

    State state;

    public enum State
    {
        Idle,
        Cooking,
        Cooked,
        Burned
    }
    private void Start()
    {
         state = State.Idle;
       
    }

    private void Update()
    {
     

        switch (state) { 
            case State.Idle:
                break;

            case State.Cooking:
                if (cookingRecipeSO == null || !KitchenObjectPresent()) return;

                Debug.Log(Cookingtimer);
                Cookingtimer += Time.deltaTime;

                if (Cookingtimer > cookingRecipeSO.CookingTime)
                {
                    Cookingtimer = 0f;
                    Debug.Log("Cooking complete");
                    KitchenObjectSO outputKitchenObjectSO = CookingKitchenObject(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                    KitchenObjects.SpawnKitchenObject(outputKitchenObjectSO, this);
                    state = State.Cooked;
                    burningTimer = 0f;
                    burningRecipeSO = GettingBurningRecipeSOwithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                }
                Debug.Log("Cooking in progress");
                break;

            case State.Cooked:
                if (burningRecipeSO == null || !KitchenObjectPresent()) return;

                Debug.Log(burningTimer);
                burningTimer += Time.deltaTime;

                if (burningTimer > burningRecipeSO.BurningTimeMax)
                {
                    burningTimer = 0f;
                    Debug.Log("Burning complete");
                    KitchenObjectSO outputKitchenObjectSO = BurningKitchenObject(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                    KitchenObjects.SpawnKitchenObject(outputKitchenObjectSO, this);
                    state = State.Burned;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
                Debug.Log("Burning in progress");
                break;
                

            case State.Burned:
                break;

        }




        

    }

    public override void Interact(PlayerController player)
    {
        if (!KitchenObjectPresent())
        {
            if (player.KitchenObjectPresent())
            {
                if (HasCookingRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {

                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    state = State.Cooking;
                    cookingRecipeSO = GettingCookingRecipeSOwithInput(GetKitchenObject().GetKitchenObjectSO());                    
                    Cookingtimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
                else
                {
                    Debug.Log("Player does not have a kitchen object with cutting recipe");
                }
            }
        }
        else
        {
            if (player.KitchenObjectPresent())
            {
                Debug.Log("Player already has a kitchen object");

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
            }

        }
    }



    

    public KitchenObjectSO CookingKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        CookingRecipeSO cookingRecipeSO = GettingCookingRecipeSOwithInput(kitchenObjectSO);
        if (cookingRecipeSO == null)
        {
            Debug.LogError("Cooking recipe not found for the given kitchen object.");
            return null;
        }

        return cookingRecipeSO.output;

    }


    public KitchenObjectSO BurningKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        BurningRecipeSO burningRecipeSO = GettingBurningRecipeSOwithInput(kitchenObjectSO);
        if (burningRecipeSO == null)
        {
            Debug.LogError("Burning Recipe not found !!");
            return null;
        }

        return burningRecipeSO.output;

    }
    private bool HasCookingRecipeInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSOs)
        {
            if (cookingRecipeSO.input == kitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    public CookingRecipeSO GettingCookingRecipeSOwithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSOs)
        {
            if (cookingRecipeSO.input == kitchenObjectSO)
            {
                return cookingRecipeSO;
            }
        }
        return null;
    }



    public BurningRecipeSO GettingBurningRecipeSOwithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in BurningRecipeSOs)
        {
            if (burningRecipeSO.input == kitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }


}
