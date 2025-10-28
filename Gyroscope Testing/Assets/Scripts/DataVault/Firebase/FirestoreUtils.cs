using System.Collections.Generic;
using UnityEngine;

public static class FirestoreUtils
{
    // Convert a Vector3 to a Dictionary for Firestore storage
    public static Dictionary<string, object> Vector3ToDictionary(Vector3 vector)
    {
        return new Dictionary<string, object>
        {
            { "x", vector.x },
            { "y", vector.y },
            { "z", vector.z }
        };
    }

    public static Dictionary<string, object> GameDataToDictionary(GameData gameData)
    {
        return new Dictionary<string, object>
        {
            {"position", Vector3ToDictionary(gameData.snapshot.position)},
            {"rotation", Vector3ToDictionary(gameData.snapshot.rotation)},
            {"time", gameData.snapshot.time}
        };
    }
}

