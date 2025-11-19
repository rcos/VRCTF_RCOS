using UnityEngine;

public class Scenario2Manager : MonoBehaviour, IDataManager
{
    /// <summary>
    /// Loads the scenario 2 data from the provided game data collection.
    /// </summary>
    public void LoadData(GameDataCollection gameDataCollection)
    {
        Debug.Log("Loading Scenario 2 Data");
        GameObject player = GameObject.Find("Player"); 
        if (!gameDataCollection.allGameData.TryGetValue("Scenario2", out var scenario2Data))
        {
            scenario2Data = new GameData("Scenario2"); 
            gameDataCollection.allGameData["Scenario2"] = scenario2Data; 
        }

        player.transform.position = scenario2Data.snapshot.position; 
        player.transform.rotation = scenario2Data.snapshot.rotation;

        ScenarioManager scenarioManager = FindObjectOfType<ScenarioManager>(); 
        if (scenario2Data.scenarioCompleted)
        {
            scenarioManager.FlagTriggered();
        }
    }

    /// <summary>
    /// Saves the scenario 2 data into the provided game data collection.
    /// </summary>
    /// <param name="gameDataCollection"></param>
    public void SaveData(ref GameDataCollection gameDataCollection)
    {
        Debug.Log("Saving Scenario 2 Data");
        GameObject player = GameObject.Find("Player");
        if (!gameDataCollection.allGameData.TryGetValue("Scenario2", out var scenario2Data))
        {
            scenario2Data = new GameData("Scenario2"); 
            gameDataCollection.allGameData["Scenario2"] = scenario2Data; 
        }

        scenario2Data.snapshot = new GameData.TransformSnapshot(player.transform.position, player.transform.rotation);

        ScenarioManager scenarioManager = FindObjectOfType<ScenarioManager>(); 
        if (scenarioManager != null && scenarioManager.GetFlagStatus())
        {
            scenario2Data.scenarioCompleted = true; 
        }

        else 
        {
            scenario2Data.scenarioCompleted = false;
        }

        gameDataCollection.allGameData["Scenario2"] = scenario2Data;
    }
}
