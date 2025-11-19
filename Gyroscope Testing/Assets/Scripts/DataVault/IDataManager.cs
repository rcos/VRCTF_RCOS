using UnityEngine;

/// <summary>
/// Interface for data management in scenarios.
/// </summary>
public interface IDataManager
{
    void LoadData(GameDataCollection data);
    void SaveData(ref GameDataCollection data);
}
