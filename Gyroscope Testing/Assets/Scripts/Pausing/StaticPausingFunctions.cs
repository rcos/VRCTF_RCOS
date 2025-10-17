using UnityEngine;
using TMPro;

public static class StaticPausingFunctions
{
    public static Vector3 pauseAreaLocation = new Vector3(1000,1000,1000);
    public static Vector3 lastKnownPlayerPosition;

    public static bool allowedToPause = true;
    public static bool currentlyPausing = false;
    public static bool currentlyPaused = false;

    public static (string, System.Type)[] allPauseOptions = { ("Resume", typeof(PauseMenu_Resume)) };

    public static void PauseGame() {
        if (currentlyPausing || currentlyPaused) return;
        currentlyPausing = true;

        // setup
        GameObject player = GameObject.Find("Player");
        GameObject thePauseArea = GameObject.Find("PauseArea(Clone)");
        if (thePauseArea == null) {
            GameObject prefab = Resources.Load<GameObject>("Pausing/PauseArea");
            thePauseArea = Object.Instantiate(prefab);
        }
        thePauseArea.transform.position = pauseAreaLocation;
        lastKnownPlayerPosition = player.transform.position;

        // show all pause options
        Transform PauseOptionsBox = thePauseArea.transform.Find("SimplePauseOptions").transform.Find("SimplePauseOptions_Background");
        MeshRenderer PauseOptionsBox_Mesh = PauseOptionsBox.GetComponent<MeshRenderer>();
        if (PauseOptionsBox_Mesh == null) { Debug.LogError("Couldn't find an object within a prefab"); }
        float upperY = PauseOptionsBox_Mesh.bounds.max.y-1.0f; // text saying "Paused" takes up space
        float lowerY = PauseOptionsBox_Mesh.bounds.min.y+0.5f;
        float differencePerButton = (upperY - lowerY) / (allPauseOptions.Length+1);
        GameObject buttonPreFab = Resources.Load<GameObject>("Pausing/IndividualButton");
        for (int i = 0; i < allPauseOptions.Length; i++) {
            GameObject curButton = Object.Instantiate(buttonPreFab);
            curButton.transform.position = new Vector3(PauseOptionsBox.position.x, upperY-differencePerButton, PauseOptionsBox.position.z);

            PauseMenu_ButtonParent logicScript = (PauseMenu_ButtonParent)curButton.AddComponent(allPauseOptions[0].Item2); // add instance of class so the object knows what function to call
            curButton.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = allPauseOptions[0].Item1;
            curButton.transform.Find("Button").GetComponent<PauseMenuButtonScript>().scriptToCall = logicScript;
        }

        // make black fade in appear
        GameObject fadeOutSquareForNewScene = null;
        fadeOutSquareForNewScene = FadeOutSquare_Static.makeNewFadeOutSquare(10,10,10, (GameEnums.FadeOutSquare_CallbackType _) => {
            player.transform.position = pauseAreaLocation;
            currentlyPausing = false;
            currentlyPaused = true;
        });
    }

    public static void UnpauseGame() {
        
    }
}


public class PauseMenu_ButtonParent : MonoBehaviour {
    public virtual void Pressed() { Debug.Log("PauseMenu_ButtonParent's function was called when pressed"); }
}

public class PauseMenu_Resume : PauseMenu_ButtonParent {
    public override void Pressed() { Debug.Log("PauseMenu_Resume's function was called when pressed"); }
}