using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();
    public static event EventHandler OnPickUpIngredient;
    public event EventHandler <OnPlateVisualChangedEventArgs> OnPlateVisualChanged;
    public class OnPlateVisualChangedEventArgs: EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnPlateVisualChanged?.Invoke(this, new OnPlateVisualChangedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            OnPickUpIngredient?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
