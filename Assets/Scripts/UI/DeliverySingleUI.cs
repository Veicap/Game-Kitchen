using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeTextUI(RecipeSO recipeSO)
    {
        recipeText.text = recipeSO.nameRecipe;
        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            iconTransform.gameObject.SetActive(true);
        }
    }
}
