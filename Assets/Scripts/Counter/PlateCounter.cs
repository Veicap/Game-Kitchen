using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnSpawnPlate;
    public event EventHandler OnDestroyPlate;
    private float spawnFlateTimer;
    private float spawnFlateTimerMax = 4f;
    private int plateAmount;
    private int plateAmountMax = 4;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private void Update()
    {
        spawnFlateTimer += Time.deltaTime;
        if(KitchenGameManager.Instance.IsGamePlaying() &&spawnFlateTimer > spawnFlateTimerMax)
        {
            spawnFlateTimer = 0;
            if(plateAmount <= plateAmountMax)
            {
                plateAmount++;
                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            plateAmount--;
            KitchenObject.SpwanKitchenObject(player, kitchenObjectSO);
            OnDestroyPlate?.Invoke(this, EventArgs.Empty);
        }
    }
}
