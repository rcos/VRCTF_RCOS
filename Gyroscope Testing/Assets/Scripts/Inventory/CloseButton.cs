using UnityEngine;
using UnityEngine.EventSystems;

public class CloseButton : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private InventoryItemData itemData;

    // This is called by InventoryManager when creating the slot
    public void Initialize(InventoryManager manager, InventoryItemData data)
    {
        inventoryManager = manager;
        itemData = data;
    }

    public void OnPointerClick()
    {
        if (inventoryManager != null && itemData != null)
        {
            inventoryManager.RemoveItem(itemData);
        }
        else
        {
            Debug.LogWarning("CloseButton missing references!");
        }
    }
}
