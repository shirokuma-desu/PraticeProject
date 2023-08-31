using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchentObject))
            {
                //Only accept plates
                DeliveryManager.Insctance.DeliverRecipe(plateKitchentObject);  
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
