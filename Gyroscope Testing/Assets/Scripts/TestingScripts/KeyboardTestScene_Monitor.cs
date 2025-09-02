using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class KeyboardTestScene_Monitor : MonoBehaviour
{
    [Header("Scenario Settings")]
    [SerializeField] private bool isScenario1;
    [SerializeField] private bool isScenario2;

    [Header("References")]
    [SerializeField] private string password;
    [SerializeField] private string ctf;
    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject signInUI;
    [SerializeField] private GameObject emailUI;
    [SerializeField] private GameObject searchBar;

    private GameObject keyboard = null;

    public TextMeshPro TMP_Text;
    public TextMeshPro Status;

    void Start()
    {   
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Scenario1")
        {
            isScenario1 = true;
            isScenario2 = false;
        }
        else if (currentScene == "Scenario2")
        {
            isScenario1 = false;
            isScenario2 = true;
        }

        if (signInUI != null) signInUI.SetActive(true);
        if (emailUI != null)
            emailUI.SetActive(false);
    }

    public void SpawnKeyboard()
    {
        if (keyboard != null)
        {
            Keyboard_3D_Static.destroyKeyboard(keyboard);
            keyboard = null;
        }

        keyboard = Keyboard_3D_Static.makeNewKeyboardObjectAndKeys(
            GameEnums.Keyboard_Type.LowercaseOnly, 0.1f, 0.1f,
            (charPressed, fullString) => keyPressed(charPressed, fullString),
            (fullString) => onSubmit(fullString),
            (fullString) => onCancel(fullString),
            null
        );

        Keyboard_3D_Static.setPosition(keyboard, new Vector3(-3.3f, 0.9f, 2.6f));
        Keyboard_3D_Static.setRotation(keyboard, new Vector3(-90f, -90f, 0f));
        Keyboard_3D_Static.setScale(keyboard, new Vector3(0.3f, 0.3f, 0.3f));

        TMP_Text.text = "";
    }

    public void OnPointerClick()
    {
        // Scenario 2: Phase 2 (email screen): only allow keyboard if user clicked the search bar
         if (isScenario2 && emailUI.activeSelf)
        {
            // Get clicked object
            GameObject clicked = EventSystem.current.currentSelectedGameObject;

            // Check if it's the search bar (or child of it)
            if (clicked != null && (clicked == searchBar || clicked.transform.IsChildOf(searchBar.transform)))
            {
                Debug.Log("Scenario2: Search bar clicked - spawning keyboard.");
                SpawnKeyboard();
            }
            else
            { 
                Debug.Log("Scenario2: Clicked elsewhere in emailUI - no keyboard.");
                return;
            }
        }
        else{
            //Spawn keyboard for both Scenario 1 and Scenario 2: Phase 1 
            SpawnKeyboard();
        }
    }


    void keyPressed(string charPressed, string fullString)
    {
        TMP_Text.text = fullString;
    }

    void onSubmit(string fullString)
    {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));

        //For searchBar in Scenario 2: Phase 2
        if (isScenario2 && emailUI.activeSelf){
            EmailManager.Instance.FilterEmails(fullString);
            return;
        }
        // For Scenario 1 or Scenario 2: Phase 1
        else if (isScenario1 || isScenario2) {
            if (fullString == password) {
                if (isScenario2)
                {
                    // Scenario 2: Switch from sign-in to email UI
                    if (signInUI != null) signInUI.SetActive(false);
                    if (emailUI != null) emailUI.SetActive(true);

                    emailUI.GetComponent<EmailManager>()?.ShowEmailScreen();
                }
                else
                {
                    // Scenario 1: Show "Correct!" and trigger flag
                    if (Status != null) Status.text = "Correct!";
                    if (manager != null) manager.GetComponent<ScenarioManager>().FlagTriggered();
                }
            }
            else
            {
                if (Status != null) Status.text = "Incorrect!";
            }

            TMP_Text.text = "";
        }
    }

    void onCancel(string fullString)
    {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));
        if (Status != null) Status.text = "Try again";
        TMP_Text.text = "";
    }
}
