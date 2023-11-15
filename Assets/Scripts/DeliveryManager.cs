using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeSOList recipeSOList;

    private List<RecipeSO> wattingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int wattingRecipesMax = 4;
    private int successfulRecipesAmount;
    private bool ingredientFound;
    private bool plateContentMatchesRecipe;

    private void Awake()
    {
          Instance = this;
          wattingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(wattingRecipeSOList.Count < wattingRecipesMax)
            {
                RecipeSO wattingRecipeSO = recipeSOList.recipeSOList[UnityEngine.Random.Range(0, recipeSOList.recipeSOList.Count)];
                Debug.Log(wattingRecipeSO.name);
                wattingRecipeSOList.Add(wattingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i =0; i < wattingRecipeSOList.Count; i++)
        {
            RecipeSO wattingRecipeSO = wattingRecipeSOList[i];
            if (wattingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                plateContentMatchesRecipe = true;
                //has the same nubmer of ingredients
                foreach (KitchenObjectSO recipeKitchenObjectSO in wattingRecipeSO.kitchenObjectSOList)
                {
                    ingredientFound = false;
                    //cycle through each ingredient in the recipe
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycle through each ingredient in the plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //ingredient match
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        
                        //ingredient not found on the plate
                        plateContentMatchesRecipe = false;
                        break;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    Debug.Log("Recipe completed");
                    //Player delivered the correct recipe
                    successfulRecipesAmount++;
                    wattingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        Debug.Log("player not deliver correct recipe");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWattingRecipeSOList()
    {
        return wattingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
