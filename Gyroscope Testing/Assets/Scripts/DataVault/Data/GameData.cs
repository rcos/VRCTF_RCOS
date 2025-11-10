using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/// <summary>
/// Class to hold all game data for a particular scenario.
/// </summary>
public class GameData
{
    /// <summary>
    /// Struct to hold a snapshot of a transform's position, rotation, and time.
    /// </summary>
    [System.Serializable]
    public  struct TransformSnapshot
    {
        public Vector3 position;
        public Quaternion rotation;
        public float time;

        public TransformSnapshot(Vector3 position, Quaternion rotation, float time = 0f)
        {
            this.position = position;
            this.rotation = rotation;
            this.time = time;
        }
    }
    
    public TransformSnapshot snapshot;
    public string scenarioName;

    public bool scenarioCompleted;
    // If a scenario contains multiple side quests/flags, we can store them in a dictionary
    public Dictionary<string, bool> flags;

    /// <summary>
    /// Constructor for GameData. Initializes with default values based on scenario name if available.
    /// </summary>
    /// <param name="scenarioName"></param>
    public GameData(string scenarioName)
    {
        this.scenarioName = scenarioName;
        // Loads default transform based on scenario name located in ScenarioDefaults.cs
        if (ScenarioDefaults.transformDefaults.TryGetValue(scenarioName, out var transformDef))
        {
            this.snapshot = transformDef;
        }

        else
        {
            this.snapshot = new TransformSnapshot(Vector3.zero, Quaternion.identity);
        }

        // Loads default flags bsaed on scenario name located in ScenarioDefaults.cs
        if (ScenarioDefaults.flagDefaults.TryGetValue(scenarioName, out var flagDef))
        {
            // Create a new dictionary to avoid reference issues from scenario defaults
            this.flags = new Dictionary<string, bool>(flagDef);
        }

        else 
        {
            this.flags = new Dictionary<string, bool>();
        }
        this.scenarioCompleted = false;
    }
}

