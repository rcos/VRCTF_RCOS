using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public string objectName;
    public Sprite objectSprite;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public GameObject originalObject; 

    public InventoryItemData(string name, Sprite sprite, Vector3 pos, Quaternion rot, Vector3 scl, GameObject obj)
    {
        objectName = name;
        objectSprite = sprite;
        position = pos;
        rotation = rot;
        scale = scl;
        originalObject = obj;
    }
}
