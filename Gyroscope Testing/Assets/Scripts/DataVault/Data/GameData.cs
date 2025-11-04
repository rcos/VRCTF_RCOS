using UnityEngine;

[System.Serializable]
public class GameData
{   
    [System.Serializable]
    public struct PositionSnapshot
    {
        public Vector3 position;
        public Vector3 rotation;
        public float time;
    }

    public PositionSnapshot snapshot;

    public GameData()
    {
        // Initialize the GameData
        this.snapshot.position = new Vector3(-2.477f, 1f, -1.635f);
        this.snapshot.rotation = new Vector3(0f, -90f, 0f);
        this.snapshot.time = 0f;

    }

}
