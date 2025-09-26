using UnityEngine;
using System.Linq;
using System.Collections.Generic;


// Singleton class to manage data saving and loading
public class DataManager : MonoBehaviour
{
    // Instance to privately set and publicly get
    public static DataManager Instance { get; private set; }
    // Reference to the game data
    private GameData gameData;
    // Reference to every script that implements IDataManager
    private List<IDataManager> dataManagerObjects;

    // File handling variables
    [Header("File Handling")]
    [SerializeField] private string fileName;

    private FileDataHandler dataHandler;
    
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Another instance of DataManager already exists. Destroying this one.");
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        // Find all objects that implement IDataManager
        this.dataManagerObjects = FindAllDataManagerObjects();
        // Load any saved data at the start of the game
        LoadGame();
    }


    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load any saved data from a file using a data handling class
        this.gameData = this.dataHandler.Load();
        // If no data to load, create a new game

        if (this.gameData == null)
        {
            Debug.Log("No game data found, creating new game data.");
            NewGame();
        }

        // TODO - push the loaded data to all other relevant scripts
        foreach (IDataManager dataManager in dataManagerObjects)
        {
            dataManager.LoadData(this.gameData);
        }
    }

    public void SaveGame()
    {
        // TODO - pass the data to other scripts so they can update it
        foreach (IDataManager dataManager in dataManagerObjects)
        {
            dataManager.SaveData(ref this.gameData);
        }

        // Save the data to a file using a data handling class
        this.dataHandler.Save(ref this.gameData);
        // Debug.Log("Game data saved successfully.");
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataManager> FindAllDataManagerObjects()
    {
        // Note: for this to work, all scripts that implement IDataManager must also inherit from MonoBehaviour
        IEnumerable<IDataManager> dataManagers = FindObjectsOfType<MonoBehaviour>().OfType<IDataManager>();
        return dataManagers.ToList();
    }
}
