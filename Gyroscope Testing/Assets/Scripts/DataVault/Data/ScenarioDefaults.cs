using UnityEngine;
using System.Collections.Generic;
public static class ScenarioDefaults
{
    public static readonly Dictionary<string, GameData.TransformSnapshot> transformDefaults = new Dictionary<string, GameData.TransformSnapshot> {
        {"Scenario1", new GameData.TransformSnapshot(new Vector3(-2.477f, 1f, -1.635f), Quaternion.identity)}
    };

    public static readonly Dictionary<string, Dictionary<string, bool>> flagDefaults = new Dictionary<string, Dictionary<string, bool>> {
        {"Scenario1", new Dictionary<string, bool> {
            {"FoundPassword", false}
        }}
    };
}