using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public string objectName;
    public Sprite objectSprite;
    public Vector3 position;
    public Quaternion rotation;

    public InventoryItemData(string name, Sprite sprite, Vector3 pos, Quaternion rot)
    {
        objectName = name;
        objectSprite = sprite;
        position = pos;
        rotation = rot;
    }
}
