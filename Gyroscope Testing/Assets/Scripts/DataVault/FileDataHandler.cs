using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    // Directory path for saving and loading files
    private string dataDir;
    private string dataFileName;

    public FileDataHandler(string dataDir, string dataFileName)
    {
        this.dataDir = dataDir;
        this.dataFileName = dataFileName;
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

            // Write the serialized data to the file
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataJson);
                }
            }

        }

        catch
        {
            Debug.LogError("Error saving game progress to " + path);
        }
    }   
}
