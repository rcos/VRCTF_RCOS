using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions; 
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    // Singleton instance ensuring one DataManager active in the entire game.
    public static DataManager Instance { get; private set; }
    public GameDataCollection gameDataCollection;
    private List<IDataManager> dataManagerObjects;
    private string persistentDataFileName = "data.save";
    private bool useEncryption = true;
    
    // Option to turn on/off saving
    [SerializeField] private bool enableSaving = true;
    private FileDataHandler fileDataHandler;

    // Firebase-relevant members
    private FirebaseFirestore db;
    private string documentPath;

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
        this.db = FirebaseFirestore.DefaultInstance;
        this.documentPath = $"users/{GetOrCreateUserId()}";
        this.gameDataCollection = this.fileDataHandler.Load();
        if (this.gameDataCollection == null)
        {
            NewGame();
        }
        // Force a scan for the very first scene (Home)
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
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
        this.gameDataCollection.UpdateSummaryTotals();  
        string jsonData = JsonUtility.ToJson(this.gameDataCollection);

        var uploadData = new Dictionary<string, object>
        {
            {"timestamp", FieldValue.ServerTimestamp },
            {"data", jsonData }
        };

        var leaderboardData = new Dictionary<string, object>
        {
            {"playerName", GetOrCreateUserId()},
            {"totalScenariosCompleted", this.gameDataCollection.totalScenariosCompleted },
            {"totalPoints", this.gameDataCollection.totalPoints }
        };

         // Upload to Firestore
        if (db != null)
        {
            var docRef = db.Document(documentPath);
            docRef.SetAsync(uploadData);
            db.Document($"leaderboards/{GetOrCreateUserId()}").SetAsync(leaderboardData);
            Debug.Log("Game data uploaded to firestore at " + documentPath);
        }
    }

    /// <summary>
    /// Saves the game data when the application is quitting.
    /// </summary>
    public void OnApplicationQuit()
    {
        if (enableSaving)
        {
            SaveGame();
        }

        else
        {
            Debug.Log("Saving is disabled. Game data not saved on application quit.");
        }
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
    /// <summary>
    /// Generates or retrieves a unique user ID for the player.
    /// </summary>
    private string GetOrCreateUserId()
    {
        const string key = "USER_ID";
        // PlayerPrefs is built-in to store data on the local device, permanently until deleted
        if (!PlayerPrefs.HasKey(key))
        {
            string newUserId = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString(key, newUserId);
            return newUserId;
        }

        return PlayerPrefs.GetString(key);
    }

    /// <summary>
    /// Function to retrieve available levels from the database. This can be used to populate a level select menu or for other purposes.> 
    /// <summary> 
    public void GetGlobalLevels(System.Action<List<LevelData>> callback)
    {
        if (db == null)
        {
            db = FirebaseFirestore.DefaultInstance;
        }


        db.Collection("levels").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Error getting levels: " + task.Exception);
                callback(new List<LevelData>());
                return;
            }

            List<LevelData> levels = new List<LevelData>();
            foreach (DocumentSnapshot document in task.Result.Documents)
            {
                if (document.Exists)
                {
                    LevelData levelData = document.ConvertTo<LevelData>();
                    levels.Add(levelData);
                }
            }
            callback(levels);
        });
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This runs every time you change scenes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-scan for ScenarioManagers in the new scene
        this.dataManagerObjects = FindAllDataManagerObjects();
        
        // Auto-load the data into the new managers
        foreach (IDataManager scenarioDataManager in dataManagerObjects)
        {
            scenarioDataManager.LoadData(this.gameDataCollection);
        }
    }

    



}