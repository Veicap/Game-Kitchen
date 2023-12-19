using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    [SerializeField] private CuttingObjectSO[] cuttingObjectSOArray;
    private int cuttingProgress;
    public event EventHandler <IHasProgress.OnBarUIChangedEventArgs> OnBarUIChanged;
    
    public event EventHandler OnCut;
    public override void Interact(Player player)
    {
        if(HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                //player is carrying anything
                if (player.HasKitchenObject())
                {
                    //player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {

                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
        else
        {
            if(player.HasKitchenObject())
            {
                if(HasCuttingObjectInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    CuttingObjectSO cuttingObjectSO = GetInputCuttingObjectSO(GetKitchenObject().GetKitchenObjectSO());
                    cuttingProgress = 0;
                    OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                    {
                        fillNomarlized = (float)cuttingProgress / cuttingObjectSO.cuttingProgressMax
                    });
                }
                
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            // counter has a objects need to cut 
            if(HasCuttingObjectInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                cuttingProgress++;
                CuttingObjectSO cuttingObjectSO = GetInputCuttingObjectSO(GetKitchenObject().GetKitchenObjectSO());
                OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                {
                    fillNomarlized = (float)cuttingProgress / cuttingObjectSO.cuttingProgressMax
                });
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                if (cuttingProgress >= cuttingObjectSO.cuttingProgressMax)
                {
                    KitchenObjectSO kitchenObjectSO = GetOutputfromInput(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpwanKitchenObject(this, kitchenObjectSO);
                    
                }
                
            }
            
            
        }
    }
    private bool HasCuttingObjectInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingObjectSO cuttingObjectSO = GetInputCuttingObjectSO(kitchenObjectSO);
        return cuttingObjectSO != null;
    }
    private KitchenObjectSO GetOutputfromInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingObjectSO cuttingObjectSO = GetInputCuttingObjectSO(kitchenObjectSO);
        if(cuttingObjectSO != null)
        {
            return cuttingObjectSO.output;
        }
        return null;
    }
    private CuttingObjectSO GetInputCuttingObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingObjectSO cuttingObjectSO in cuttingObjectSOArray)
        {
            if (cuttingObjectSO.input == kitchenObjectSO)
            {
                return cuttingObjectSO;
            }

        }
        return null;
    }
}
