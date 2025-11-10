using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    // Directory for saving and loading data
    private string persistentDataPath;
    // File name that contains the data
    private string persistentDataFileName;

    // XOR-based encryption key
    private bool useEncryption = false;
    private readonly string encryptionKey = "word";
    /// <summary>
    /// Constructor for FileDataHandler. Initializes load/save file path, name, and encryption option. 
    /// </summary>
    /// <param name="persistentDataPath">The path to the directory where data files are stored.</param>
    /// <param name="persistentDataFileName">The name of the file containing the data.</param>
    /// <param name="useEncryption">Whether to use XOR-based encryption for the data file.</param>
    public FileDataHandler(string persistentDataPath, string persistentDataFileName, bool useEncryption = false)
    {
        this.persistentDataPath = persistentDataPath;
        this.persistentDataFileName = persistentDataFileName;
        this.useEncryption = useEncryption;
    }

    /// <summary>
    /// Loads the data for all scenarios from the player's persistent data path.
    /// </summary>
    /// <returns>A GameDataCollection object containing all loaded game data, or null if no file exists.</returns>
    public GameDataCollection Load()
    {
        // Combine the file name and persistent path to generate the full path
        string path = Path.Combine(persistentDataPath, persistentDataFileName);
        // Initially starts as null if no file found
        GameDataCollection loadedGameDataCollection = null;
        if (File.Exists(path))
        {
            try
            {
                // Loading serialized data from file
                string jsonData = "";
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        jsonData = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    jsonData = EncryptDecrypt(jsonData);
                }
                // Deserialize the data from JSON back into a GameDataCollection object
                loadedGameDataCollection = JsonUtility.FromJson<GameDataCollection>(jsonData);

                // Sync the deserialized list data back into the runtime dictionary
                loadedGameDataCollection.SyncListToDictionary();
            }

            catch (Exception e)
            {
                Debug.LogError("Error loading game progress from " + path + ": " + e.Message);
            }
        }
        return loadedGameDataCollection;
    }
    
    /// <summary>
    /// Saves the data for all scenarios to the player's persistent data path.
    /// </summary>
    /// <param name="gameDataCollection">The GameDataCollection object containing all game data to be saved.</param>
    public void Save(ref GameDataCollection gameDataCollection)
    {
        string path = Path.Combine(persistentDataPath, persistentDataFileName);
        try
        {
            // If this is the player's first time saving, create the directory
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            // Sync the dictionary data to the serializable list before saving
            gameDataCollection.SyncDictionaryToList();

            // Serialize the data from GameDataCollection object to JSON
            string jsonData = JsonUtility.ToJson(gameDataCollection, true);

            if (useEncryption)
            {
                jsonData = EncryptDecrypt(jsonData);
            }

            // Write the serialized data to the file
            using (var stream = new FileStream(path, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(jsonData);
                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError("Error saving game progress to " + path + ": " + e.Message);
        }
    }

    /// <summary>
    /// Simple XOR-based encryption/decryption method.
    /// </summary>
    /// <param name=""data">The string to be encrypted or decrypted.</param>
    /// <returns>The modified string after encryption or decryption.</returns>
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            // Uses XOR to modify each character with the encryption key
            modifiedData += (char)(data[i] ^ encryptionKey[i % encryptionKey.Length]);
        }
        return modifiedData;
    }
}
