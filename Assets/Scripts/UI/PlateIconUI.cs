using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;
    private List<Transform> iconInstances = new List<Transform>();
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
        
    }
    private void Start()
    {
        // plateKitchenObject.OnPlateVisualChanged += PlateKitchenObject_OnPlateVisualChanged;
        plateKitchenObject.OnPlateVisualChanged += PlateKitchenObject_OnPlateVisualChanged;

    }

    private void PlateKitchenObject_OnPlateVisualChanged(object sender, PlateKitchenObject.OnPlateVisualChangedEventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        
        foreach (Transform iconInstance in iconInstances)
        {
            Destroy(iconInstance.gameObject);
        }
        iconInstances.Clear();
        /*foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }*/
        
           
            
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.GetComponent<IconSingleUI>().SetSpriteKichenObjectSO(kitchenObjectSO);
            iconTransform.gameObject.SetActive(true);
            iconInstances.Add(iconTransform);
            
        }
    }
}
