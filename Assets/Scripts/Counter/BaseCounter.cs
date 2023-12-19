using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    public static event EventHandler OnDropObject;
    public static void ResetStaticData()
    {
        OnDropObject = null;
    }
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.Log("I love you");

    }
    public virtual void InteractAlternate(Player player) 
    {
        Debug.Log("I loveed you");

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKichenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        OnDropObject?.Invoke(this, EventArgs.Empty);
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
