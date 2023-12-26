using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private float spawnPlateTime;
    private float spawnPlateTimeMax = 4f;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;

    private void Update()
    {
        spawnPlateTime += Time.deltaTime;
        if(spawnPlateTime > spawnPlateTimeMax)
        {
            spawnPlateTime = 0f;
            if(GameManager.Instance.IsGameInProgress() && plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player empty hand
            if(plateSpawnAmount > 0)
            {
                //there's at least one plate
                plateSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);  
            }
        }
    }
}
