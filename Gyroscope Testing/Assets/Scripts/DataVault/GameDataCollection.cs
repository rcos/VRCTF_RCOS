using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataCollection
{
    // Unity can’t serialize dictionaries, so we store entries as a list for serialization
    [System.Serializable]
    public class GameDataEntry
    {
        public string scenarioName;
        public GameData data;
    }

    [SerializeField] private List<GameDataEntry> serializedEntries = new List<GameDataEntry>();

    // The runtime dictionary used for fast lookups
    public Dictionary<string, GameData> allGameData = new Dictionary<string, GameData>();

    public GameDataCollection() { }

    /// <summary>
    /// Syncs the runtime dictionary to the serializable list of key-value pairs mapping scenario name to GameData.
    /// </summary>
    public void SyncDictionaryToList()
    {
        serializedEntries.Clear();
        foreach (var pair in allGameData)
        {
            serializedEntries.Add(new GameDataEntry
            {
                scenarioName = pair.Key,
                data = pair.Value
            });
        }
    }

    /// <summary>
    /// Syncs the serializable list of key-value pairs mapping scenario name to GameData back into the runtime dictionary.
    /// </summary>
    public void SyncListToDictionary()
    {
        allGameData.Clear();
        foreach (var entry in serializedEntries)
        {
            if (!allGameData.ContainsKey(entry.scenarioName))
                allGameData[entry.scenarioName] = entry.data;
        }
    }
}
