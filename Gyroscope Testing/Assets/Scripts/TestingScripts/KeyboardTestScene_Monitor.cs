using System;
using UnityEngine;
using TMPro;

public class KeyboardTestScene_Monitor : MonoBehaviour
{
    [Header("Scenario Settings")]
    [SerializeField] private bool isScenario2 = false;

    [Header("References")]
    [SerializeField] private string password;
    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject signInUI;
    [SerializeField] private GameObject emailUI;

    private GameObject keyboard = null;

    public TextMeshPro TMP_Text;
    public TextMeshPro Status;

    void Start()
    {   
        if (signInUI != null) signInUI.SetActive(true);
        if (emailUI != null)
            emailUI.SetActive(false);
    }

    public void OnPointerClick()
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

    void keyPressed(string charPressed, string fullString)
    {
        TMP_Text.text = fullString;
    }

    void onSubmit(string fullString)
    {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));

        if (fullString == password)
        {
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

    void onCancel(string fullString)
    {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));
        if (Status != null) Status.text = "Incorrect!";
        TMP_Text.text = "";
    }
}
