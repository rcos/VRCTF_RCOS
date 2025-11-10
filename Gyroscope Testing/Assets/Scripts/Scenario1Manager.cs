using UnityEngine;
using UnityEngine.Events;

public class Scenario1Manager : MonoBehaviour, IDataManager
{
    /// <summary>
    /// Loads the scenario 1 data from the provided game data collection.
    /// </summary>
    public void LoadData(GameDataCollection gameDataCollection)
    {
        Debug.Log("Loading Scenario 1 Data");
        GameObject player = GameObject.Find("Player");
        if (!gameDataCollection.allGameData.TryGetValue("Scenario1", out var scenario1Data))
        {
            scenario1Data = new GameData("Scenario1");
            gameDataCollection.allGameData["Scenario1"] = scenario1Data;
        }

        player.transform.position = scenario1Data.snapshot.position;
        player.transform.rotation = scenario1Data.snapshot.rotation;

        ScenarioManager scenarioManager = FindObjectOfType<ScenarioManager>();
        if (scenario1Data.scenarioCompleted)
        {
            scenarioManager.FlagTriggered();
        }

    }

    /// <summary>
    /// Saves the scenario 1 data into the provided game data collection.
    /// </summary>
    /// <param name="gameDataCollection"></param>
    public void SaveData(ref GameDataCollection gameDataCollection)
    {
        Debug.Log("Saving Scenario 1 Data");
        GameObject player = GameObject.Find("Player");

        if (!gameDataCollection.allGameData.TryGetValue("Scenario1", out var scenario1Data))
        {
            scenario1Data = new GameData("Scenario1");
            gameDataCollection.allGameData["Scenario1"] = scenario1Data;
        }

        scenario1Data.snapshot = new GameData.TransformSnapshot(player.transform.position, player.transform.rotation);

        ScenarioManager scenarioManager = FindObjectOfType<ScenarioManager>();
        if (scenarioManager != null && scenarioManager.GetFlagStatus())
        {
            scenario1Data.scenarioCompleted = true;
        }
        
        gameDataCollection.allGameData["Scenario1"] = scenario1Data;
    }
}
