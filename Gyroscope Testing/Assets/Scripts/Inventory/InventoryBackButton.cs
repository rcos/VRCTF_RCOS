using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryBackButton : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public void OnPointerClick()
    {
        if (inventoryManager != null)
        {
            inventoryManager.PreviousPage();
        }
    }
}
