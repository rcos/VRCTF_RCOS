using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Firebase.Firestore;
using System.IO;

public class DataManager : MonoBehaviour
{
    // Singleton instance ensuring one DataManager active in the entire game.
    public static DataManager Instance { get; private set; }
    public GameDataCollection gameDataCollection;
    private List<IDataManager> dataManagerObjects;
    private string persistentDataFileName = "data.save";
    private bool useEncryption = true;

    private FileDataHandler fileDataHandler;

    // Firebase-relevant members
    private FirebaseFirestore db;
    private string documentPath = "game_data/SathR12";

    /// <summary>
    /// Awake ensures the singleton pattern for DataManager.
    /// </summary>
    private void Awake()
    {
        // Ensure only one instance of DataManager exists in the game.
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Another instance of DataManager already exists. Destroying this one.");
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    /// <summary>
    /// Loads game data at the start of the game.
    /// </summary>
    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, persistentDataFileName, useEncryption);
        this.dataManagerObjects = FindAllDataManagerObjects();
        this.db = FirebaseFirestore.DefaultInstance;
        LoadGame();
    }

    /// <summary>
    /// Starts a new game with a blank game data collection object.
    /// </summary>
    public void NewGame()
    {
        this.gameDataCollection = new GameDataCollection();
    }

    /// <summary>
    /// Loads the game data from persistent storage for all scenarios.
    /// </summary>
    public void LoadGame()
    {
        // Load any saved data from a file using a data handling class
        this.gameDataCollection = this.fileDataHandler.Load();
        // If no data to load, create a new game
        if (this.gameDataCollection == null)
        {
            Debug.Log("No game data found, creating new game data.");
            NewGame();
        }
        // Each class that implements IDataManager loads its data
        foreach (IDataManager scenarioDataManager in dataManagerObjects)
        {
            // Each scenario data manager loads its respective data from the collection for each scenario
            scenarioDataManager.LoadData(this.gameDataCollection);
        }
    }

    /// <summary>
    /// Saves the game data to persistent storage for all scenarios.
    /// </summary>
    public void SaveGame()
    {
        // Each class that implements IDataManager saves its data
        foreach (IDataManager scenarioDataManager in dataManagerObjects)
        {
            // Each scenario data manager saves its respective data to the collection for each scenario
            scenarioDataManager.SaveData(ref this.gameDataCollection);
        }
        // Save the data to a file using a data handling class
        this.fileDataHandler.Save(ref this.gameDataCollection);

        // Upload to firebase firestore
        this.gameDataCollection.SyncDictionaryToList();
        string jsonData = JsonUtility.ToJson(this.gameDataCollection);

        var uploadData = new Dictionary<string, object>
        {
            {"timestamp", FieldValue.ServerTimestamp },
            {"data", jsonData }
        };
        
         // Upload to Firestore
        if (db != null)
        {
            var docRef = db.Document(documentPath);
            docRef.SetAsync(uploadData);
            Debug.Log("Game data uploaded to firestore at " + documentPath);
        }
    }

    /// <summary>
    /// Saves the game data when the application is quitting.
    /// </summary>
    public void OnApplicationQuit()
    {
        SaveGame();
    }

    /// <summary>
    /// Finds all objects in the scene that implement the IDataManager interface.
    /// This is essentially a way to find all scenario managers to load and save scenario relevant data.
    /// </summary>
    /// <returns>a list of all objects in the scene that implement the IDataManager interface</returns>
    private List<IDataManager> FindAllDataManagerObjects()
    {
        // Note: for this to work, all scripts that implement IDataManager must also inherit from MonoBehaviour
        IEnumerable<IDataManager> dataManagers = FindObjectsOfType<MonoBehaviour>().OfType<IDataManager>();
        return dataManagers.ToList();
    }
}
