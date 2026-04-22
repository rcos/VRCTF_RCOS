using Firebase.Firestore;

[FirestoreData]
/// <summary>
/// Class representing the levels and its attribute for Firestore storage.
/// </summary>
public class LevelData
{
    [FirestoreProperty]
    public string levelID { get; set; }
    
    [FirestoreProperty]
    public string levelName { get; set; }
    
    [FirestoreProperty]
    public string sceneName { get; set; }
    
    [FirestoreProperty]
    public int order { get; set; }
}