using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform slotsArea;
    public TMP_Text pageText;
    public GameObject inventorySlotPrefab;

    [Header("Settings")]
    public int slotsPerPage = 3;
    public int pageNumber = 1;

    private Queue<InventoryItemData> itemQueue = new Queue<InventoryItemData>();
    private List<GameObject> allSlots = new List<GameObject>();
    private int totalPages => Mathf.CeilToInt((float)itemQueue.Count / slotsPerPage);


    public static InventoryManager instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateSlots();
        RefreshUI();
    }

    private void GenerateSlots()
    {
        // Create a set number of slots that we can reuse (3 per page)
        for (int i = 0; i < slotsPerPage; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, slotsArea);
            slot.name = $"Slot {i + 1}";
            allSlots.Add(slot);

            // Initialize visuals
            Image img = slot.GetComponentInChildren<Image>();
            TMP_Text txt = slot.GetComponentInChildren<TMP_Text>();
            if (img != null) img.sprite = null;
            if (txt != null) txt.text = slot.name;
        }

        LayoutSlotsEvenly();
    }

    public void AddItem(InventoryItemData itemData)
    {
        itemQueue.Enqueue(itemData);
        Debug.Log("Added item complete");
        RefreshUI();
    }

    public void RemoveItem(InventoryItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogWarning("Tried to remove null item!");
            return;
        }

        itemData.originalObject.SetActive(true);

        // Remove from queue
        List<InventoryItemData> tempList = new List<InventoryItemData>(itemQueue);
        if (tempList.Remove(itemData))
        {
            itemQueue = new Queue<InventoryItemData>(tempList);
            Debug.Log(itemData.objectName + " removed from inventory.");
        }
        else
        {
            Debug.LogWarning("Item not found in inventory!");
        }

        // Refresh UI
        RefreshUI();
    }


    public void RefreshUI()
    {
        // Get items in queue as array
        InventoryItemData[] itemsArray = itemQueue.ToArray();

        // Calculate pages
        int total = Mathf.Max(1, totalPages);
        pageNumber = Mathf.Clamp(pageNumber, 1, total);
        pageText.text = $"Page {pageNumber} of {total}";

        // Show correct 3 items
        int start = (pageNumber - 1) * slotsPerPage;
        int end = Mathf.Min(start + slotsPerPage, itemsArray.Length);

        for (int i = 0; i < allSlots.Count; i++)
        {
            var slot = allSlots[i];
            Image img = slot.GetComponentInChildren<Image>();
            TMP_Text txt = slot.GetComponentInChildren<TMP_Text>();

            if (i + start < end)
            {
                var data = itemsArray[i + start];
                if (txt != null) txt.text = data.objectName;
                if (img != null) img.sprite = data.objectSprite;
                slot.SetActive(true);

                CloseButton closeBtn = slot.GetComponentInChildren<CloseButton>(true);
                if (closeBtn != null)
                {
                    closeBtn.Initialize(this, data);
                }
            }
            else
            {
                if (txt != null) txt.text = $"Slot {i + 1}";
                if (img != null) img.sprite = null;
                slot.SetActive(false);
            }
        }
    }

    private void LayoutSlotsEvenly()
    {
        RectTransform areaRect = slotsArea.GetComponent<RectTransform>();
        float width = areaRect.rect.width;

        // Assuming all slots are the same size
        if (allSlots.Count == 0) return;

        RectTransform slotRect = allSlots[0].GetComponent<RectTransform>();
        float slotWidth = slotRect.rect.width;

        int count = Mathf.Min(slotsPerPage, allSlots.Count);

        // Compute total width of all slots + gaps
        float totalSlotWidth = count * slotWidth;
        float availableSpace = width - totalSlotWidth;
        float gap = availableSpace / (count + 1);

        // Start from left edge
        float x = -width / 2f + gap + slotWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            RectTransform rt = allSlots[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(x, 0);
            x += slotWidth + gap;
        }
    }


    // Called externally by Next/Back scripts
    public void NextPage()
    {
        if (pageNumber < totalPages)
        {
            pageNumber++;
            RefreshUI();
        }
    }

    public void PreviousPage()
    {
        if (pageNumber > 1)
        {
            pageNumber--;
            RefreshUI();
        }
    }
}
