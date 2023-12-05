using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    public static event EventHandler OnAnyCounterPlaceHere;

    public static void ResetStaticData()
    {
        OnAnyCounterPlaceHere = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.Log("basecounter should not called");
    }


    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("basecounter method shouldn't called");
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnAnyCounterPlaceHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}