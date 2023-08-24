using UnityEngine;

public interface IKitchenObjectParent
{
    public void ClearKitchenObject();

    public KitchenObject GetKitchenObject();

    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public bool HasKitchenObject();
}