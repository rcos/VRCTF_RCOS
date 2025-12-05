using UnityEngine;
using TMPro;

public static class StaticPausingFunctions
{
    /* ---------------------------------------------------------------------- */
    /* ---------------------------------------------------------------------- */
    /* --------------------------- Pausing ---------------------------------- */
    /* ---------------------------------------------------------------------- */
    /* ---------------------------------------------------------------------- */

    public static Vector3 pauseAreaLocation = new Vector3(1000,1000,1000);
    public static Vector3 lastKnownPlayerPosition;

    public static bool allowedToPause = true;
    public static bool currentlyUnPausing { get; private set; } = false;
    public static bool currentlyPausing { get; private set; } = false;
    public static bool currentlyPaused { get; private set; } = false;

    public static (string, PauseMenu_ButtonParent)[] allPauseOptions = { ("Resume", new PauseMenu_Resume()), 
                                                                         ("Settings", new PauseMenu_Settings()), 
                                                                         ("Inventory", new PauseMenu_Inventory())};
    private static GameObject[] allButtonsMade = null;

    public static void PauseGame() {
        if (!allowedToPause || currentlyPausing || currentlyPaused) return;
        currentlyPausing = true;
        allButtonsMade = new GameObject[allPauseOptions.Length];

        // setup
        GameObject player = GameObject.Find("Player");
        GameObject thePauseArea = GameObject.Find("PauseArea(Clone)");
        if (thePauseArea == null) {
            GameObject prefab = Resources.Load<GameObject>("Pausing/PauseArea");
            thePauseArea = Object.Instantiate(prefab);
        }
        thePauseArea.transform.position = pauseAreaLocation;
        thePauseArea.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        lastKnownPlayerPosition = player.transform.position;
        if (settingsOpen) toggleSettings();

        // show all pause options
        Transform PauseOptionsContainer = thePauseArea.transform.Find("SimplePauseOptions");
        Transform PauseOptionsRenderer = PauseOptionsContainer.transform.Find("SimplePauseOptions_Background");
        MeshRenderer PauseOptionsBox_Mesh = PauseOptionsRenderer.GetComponent<MeshRenderer>();
        if (PauseOptionsBox_Mesh == null) { Debug.LogError("Couldn't find an object within a prefab"); }
        float upperY = PauseOptionsBox_Mesh.bounds.max.y-1.0f; // text saying "Paused" takes up space
        float lowerY = PauseOptionsBox_Mesh.bounds.min.y+0.5f;
        float differencePerButton = (upperY - lowerY) / (allPauseOptions.Length+1);
        GameObject buttonPreFab = Resources.Load<GameObject>("Pausing/IndividualButton");
        for (int i = 0; i < allPauseOptions.Length; i++) {
            GameObject curButton = Object.Instantiate(buttonPreFab, PauseOptionsContainer);
            allButtonsMade[i] = curButton;
            curButton.transform.position = new Vector3(PauseOptionsContainer.position.x, upperY-((i+1)*differencePerButton), PauseOptionsContainer.position.z);
            curButton.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = allPauseOptions[i].Item1;
            curButton.transform.Find("Button").GetComponent<PauseMenuButtonScript>().scriptToCall = allPauseOptions[i].Item2;
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
        if (!currentlyPaused || currentlyPausing || currentlyUnPausing) return;
        currentlyUnPausing = true;

        //Reset
        settingsOpen = false;
        inventoryOpen = false;
        GameObject pauseArea = GameObject.Find("PauseArea(Clone)");
        if (pauseArea == null) return;

        Transform SettingsPanel = pauseArea.transform.Find("SimpleSettingsOptions");
        if (SettingsPanel == null) return;

        SettingsPanel.gameObject.SetActive(false);
        InventoryManager.instance.gameObject.SetActive(false);

        // make black fade in appear
        GameObject fadeOutSquareForNewScene = null;
        fadeOutSquareForNewScene = FadeOutSquare_Static.makeNewFadeOutSquare(10,10,10, (GameEnums.FadeOutSquare_CallbackType _) => {
            GameObject.Find("Player").transform.position = lastKnownPlayerPosition;
            currentlyUnPausing = false;
            currentlyPaused = false;
            for (int i = 0; i < allButtonsMade.Length; i++) {
                Object.Destroy(allButtonsMade[i]);
            }
            allButtonsMade = null;
        });

        
    }

    /* ---------------------------------------------------------------------- */
    /* ---------------------------------------------------------------------- */
    /* ------------------------ Settings Menu ------------------------------- */
    /* ---------------------------------------------------------------------- */
    /* ---------------------------------------------------------------------- */

    private static bool settingsOpen = false;

    public static void toggleSettings() {
        settingsOpen = !settingsOpen;
        GameObject pauseArea = GameObject.Find("PauseArea(Clone)");
        if (pauseArea == null) return;
        Transform SettingsPanel = pauseArea.transform.Find("SimpleSettingsOptions");
        if (SettingsPanel == null) return;

        if (InventoryManager.instance != null) {
            InventoryManager.instance.gameObject.SetActive(false);
        }
        SettingsPanel.gameObject.SetActive(settingsOpen);
    }

    private static bool inventoryOpen = false;

    public static void showInventory() {
        inventoryOpen = !inventoryOpen; 
        GameObject pauseArea = GameObject.Find("PauseArea(Clone)");
        if (pauseArea == null) return;

        Transform SettingsPanel = pauseArea.transform.Find("SimpleSettingsOptions");
        if (SettingsPanel == null) return;

        // Hide settings if it's open
        SettingsPanel.gameObject.SetActive(false);

        if (InventoryManager.instance != null) {
            InventoryManager.instance.gameObject.SetActive(inventoryOpen);
            InventoryManager.instance.transform.position = SettingsPanel.position;
            InventoryManager.instance.transform.rotation = SettingsPanel.rotation;
        }
    }
}