using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private Transform container;
   
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
       
    }
    private void Start()
    {
        DeliveryManager.Instance.OnSpawnRecipe += DeliveryManager_OnSpawnRecipe;
        DeliveryManager.Instance.OnDestroyRecipe += DeliveryManager_OnDestroyRecipe;
        UpdateVisual();
    }

    private void DeliveryManager_OnDestroyRecipe(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnSpawnRecipe(object sender, System.EventArgs e)
    {
        
        UpdateVisual();
    }
    
    
    private void UpdateVisual()
    {
        
       
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.GetComponent<DeliverySingleUI>().SetRecipeTextUI(recipeSO);
            recipeTransform.gameObject.SetActive(true);
        }
     
        
    }
}
