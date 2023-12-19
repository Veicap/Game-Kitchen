using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    private struct PlateCompleteVisualObject
    {
        public GameObject gameObject;
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<PlateCompleteVisualObject> plateCompleteVisualObjectList;

    private void Start()
    {
        plateKitchenObject.OnPlateVisualChanged += PlateKitchenObject_OnPlateVisualChanged;
        foreach (PlateCompleteVisualObject plateCompleteVisualObject in plateCompleteVisualObjectList)
        {
            plateCompleteVisualObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnPlateVisualChanged(object sender, PlateKitchenObject.OnPlateVisualChangedEventArgs e)
    {
        foreach(PlateCompleteVisualObject plateCompleteVisualObject in plateCompleteVisualObjectList)
        {
            
            if(plateCompleteVisualObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                plateCompleteVisualObject.gameObject.SetActive(true);
            }
        }
    }
}
