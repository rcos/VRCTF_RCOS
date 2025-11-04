using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    // Directory path for saving and loading files
    private string dataDir;
    private string dataFileName;

    // XOR-based simple encryption key
    private bool useEncryption = false;
    private readonly string encryptionKey = "word";

    public FileDataHandler(string dataDir, string dataFileName, bool useEncryption = false)
    {
        this.dataDir = dataDir;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {

        string path = Path.Combine(dataDir, dataFileName);
        GameData loadedData = null;
        if (File.Exists(path))
        {
            try
            {
                // Loading serialized data from file
                string jsonData = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        jsonData = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    jsonData = EncryptDecrypt(jsonData);
                }

                // Deserialize the data from JSON back into a GameData object
                loadedData = JsonUtility.FromJson<GameData>(jsonData);

            }

            catch (Exception e)
            {
                Debug.LogError("Error loading game progress from " + path + ": " + e.Message);
            }
        }

        return loadedData;


    }

    public void Save(ref GameData data)
    {
        string path = Path.Combine(dataDir, dataFileName);

        try
        {
            // If the directory DNE, create it 
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            // Serialize GameData object to JSON
            // Second parameter is for json formatting
            string dataJson = JsonUtility.ToJson(data, true);

            // Using simple XOR encryption if enabled
            if (useEncryption)
            {
                dataJson = EncryptDecrypt(dataJson);
            }

            // Write the serialized data to the file
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataJson);
                }
            }

        }

        catch (Exception e)
        {
            Debug.LogError("Error saving game progress to " + path + ": " + e.Message);
        }
    }   

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
