using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnOpenCloseVisual;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            KitchenObject.SpwanKitchenObject(player, kitchenObjectSO);
            OnOpenCloseVisual?.Invoke(this, EventArgs.Empty);
        }
        
    }
   
}
