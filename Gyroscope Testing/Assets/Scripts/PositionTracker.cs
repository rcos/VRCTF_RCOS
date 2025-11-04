using UnityEngine;

public class PositionTracker : MonoBehaviour, IDataManager
{
    public void LoadData(GameData data)
    {
        // Load position data from the GameData object
        this.transform.position = data.snapshot.position;
        this.transform.eulerAngles = data.snapshot.rotation;

        // Temporary debug log to confirm loading
        Debug.Log($"Position: {data.snapshot.position}, Rotation: {data.snapshot.rotation}");
    }

    public void SaveData(ref GameData data)
    {
        // Save position data to the GameData object
        data.snapshot.position = this.transform.position;
        data.snapshot.rotation = this.transform.eulerAngles;

        // Temporary debug log to confirm saving
        Debug.Log($"Position saved: {data.snapshot.position}, Rotation saved: {data.snapshot.rotation}");
    }
}
