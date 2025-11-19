using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryNextButton : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public void OnPointerClick()
    {
        if (inventoryManager != null)
        {
            inventoryManager.NextPage();
        }
    }
}
