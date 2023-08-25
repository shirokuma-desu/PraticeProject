using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchen object here in the counter
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if(HasRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    //player is carrying something that can be cut
                }
                
            }
            else
            {
                //player not carrying anything
            }
        }
        else
        {
            //There is a kitchen object here in the counter
            if (player.HasKitchenObject())
            {
                //player is carrying something 
            }
            else
            {
                //player is not carrying anything get the object in the counter
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasRecipeInput(GetKitchenObject().GetKitchenObjectSO())) 
        {
            //player contain kitchen obj here and it cant be cut
            KitchenObjectSO outputKitchenObjecSO = GetOutPutForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjecSO, this);
        }
    }

    private KitchenObjectSO GetOutPutForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

    //create bool function check recipewithinput    
    private bool HasRecipeInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }
}
