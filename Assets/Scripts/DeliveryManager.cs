using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    //private List waitingRecipe;
    public event EventHandler OnSpawnRecipe;
    public event EventHandler OnDestroyRecipe;
    public event EventHandler OnDeliverSuccess;
    public event EventHandler OnDeliverFailure;

    [SerializeField] private List<RecipeSO> recipeSOList;
    private float timeToSpwanRecipe;
    private float timeToSpawnRecipeMax = 4f;
    private List<RecipeSO> waitingRecipeSOList;
    private int waitingRecipeSOListMax = 4;
    private int amountPlateDeliveredSuccess;
    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        timeToSpwanRecipe -= Time.deltaTime;
        if(KitchenGameManager.Instance.IsGamePlaying()&&timeToSpwanRecipe < 0f)
        {
            timeToSpwanRecipe = timeToSpawnRecipeMax;
            if(waitingRecipeSOList.Count < waitingRecipeSOListMax)
            {
                RecipeSO waititngRecipeSO = recipeSOList[UnityEngine.Random.Range(0, recipeSOList.Count)];
               
                waitingRecipeSOList.Add(waititngRecipeSO);
                OnSpawnRecipe?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }
    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++ )
        {
            RecipeSO recipeKitchenObjectSO = waitingRecipeSOList[i];
            if(recipeKitchenObjectSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // They have same number of ingredient
                bool matchRecipe = true;
                foreach(KitchenObjectSO reicpeKitchenObjectSO in recipeKitchenObjectSO.kitchenObjectSOList)
                {
                    // cycling all ingredient in recipe
                    bool foundIngredient = false;
                    foreach(KitchenObjectSO plateKitchenObjetSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(plateKitchenObjetSO == reicpeKitchenObjectSO)
                        {
                            foundIngredient = true; 
                            break;
                        }
                    }
                    if (!foundIngredient)
                    {
                        //just first time foundingridient == false it mathrecipe equal false
                        matchRecipe = false;
                    }
                }
                if(matchRecipe)
                {
                    // match all ingredient then check in compiler
                    amountPlateDeliveredSuccess++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnDestroyRecipe?.Invoke(this, EventArgs.Empty); 
                    OnDeliverSuccess.Invoke(this, EventArgs.Empty);
                    return;

                }
            }
        }
        // No match any recipe // check in compiler
        OnDeliverFailure?.Invoke(this, EventArgs.Empty);
    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    public int GetAmountPlateDeliveredSuccess()
    {
        return amountPlateDeliveredSuccess;
    }
   
}
