using UnityEngine;

// Interface to describe the DataManager functionality
public interface IDataManager
{
    void LoadData(GameData data);
    // Note: must be passed by ref to update the original data
    void SaveData(ref GameData data);

}
