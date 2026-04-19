using UnityEngine;
using TMPro;

public class KeypadMonitor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string correctCode;

    [Header("UI")]
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private TextMeshPro statusText;

    private GameObject keyboard;

    public void SpawnKeypad()
    {
        if (keyboard != null)
        {
            Keyboard_3D_Static.destroyKeyboard(keyboard);
        }

        keyboard = Keyboard_3D_Static.makeNewKeyboardObjectAndKeys(
            GameEnums.Keyboard_Type.numberpad,
            0.1f, 0.1f,
            (c, full) => OnKeyPressed(full),
            (full) => OnSubmit(full),
            (full) => OnCancel(),
            null
        );

        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, 1, 2));
        Keyboard_3D_Static.setRotation(keyboard, new Vector3(-90f, 0f, 0f));
        Keyboard_3D_Static.setScale(keyboard, new Vector3(0.3f, 0.3f, 0.3f));

        displayText.text = "";
    }

    void OnKeyPressed(string fullString)
    {
        displayText.text = fullString;
    }

    void OnSubmit(string input)
    {
        HideKeyboard();

        if (input == correctCode)
        {
            statusText.text = "Correct!";
            // TODO: trigger door
        }
        else
        {
            statusText.text = "Incorrect!";
            displayText.text = "";
        }
    }

    void OnCancel()
    {
        HideKeyboard();
        statusText.text = "Cancelled";
        displayText.text = "";
    }

    void HideKeyboard()
    {
        if (keyboard != null)
        {
            Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 0));
        }
    }
}