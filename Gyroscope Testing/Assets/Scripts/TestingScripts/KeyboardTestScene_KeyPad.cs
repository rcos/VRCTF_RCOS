using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class KeyboardTestScene_Keypad : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private string password;
    [SerializeField] private GameObject manager;

    private GameObject keyboard = null;

    public TextMeshPro TMP_Text;
    public TextMeshPro Status;

    void Start()
    {   
        string currentScene = SceneManager.GetActiveScene().name;
    }

    public void SpawnKeyboard()
    {
        if (keyboard != null)
        {
            Keyboard_3D_Static.destroyKeyboard(keyboard);
            keyboard = null;
        }

        keyboard = Keyboard_3D_Static.makeNewKeyboardObjectAndKeys(
            GameEnums.Keyboard_Type.numberpad, 0.1f, 0.1f,
            (charPressed, fullString) => keyPressed(charPressed, fullString),
            (fullString) => onSubmit(fullString),
            (fullString) => onCancel(fullString),
            null
        );

        Keyboard_3D_Static.setPosition(keyboard, new Vector3(3.377f, 6.9f, 5.77f));
        Keyboard_3D_Static.setRotation(keyboard, new Vector3(-90f, 90f, -90f));
        Keyboard_3D_Static.setScale(keyboard, new Vector3(0.15f, 0.27f, 0.27f));

        TMP_Text.text = "";
    }

    public void OnPointerClick()
    { 
        SpawnKeyboard();
    }


    void keyPressed(string charPressed, string fullString)
    {
        TMP_Text.text = fullString;
    }

    void onSubmit(string fullString)
    {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));
        if (fullString == password) {
            // Scenario 1: Show "Correct!" and trigger flag
            if (Status != null) Status.text = "Correct!";
            TMP_Text.text = "";
            GetComponent<RoomController>().MovePosition();
        }
        else
        {
            if (Status != null) Status.text = "Incorrect!";
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
