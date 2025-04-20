using System;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class KeyboardTestScene_Monitor : MonoBehaviour
{
    [SerializeField] private String password;
    [SerializeField] private GameObject manager;
    private GameObject keyboard = null;
    public TextMeshPro TMP_Text;
    public TextMeshPro Status;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Runs when the camera is first looking at an object
    public void OnPointerEnter()
    {
        
    }

    // Runs when the camera is no longer looking at an object
    public void OnPointerExit()
    {
        
    }

    // Runs when the phone is tapped on while looking at an object
    public void OnPointerClick()
    {
        if (keyboard != null) {
            Keyboard_3D_Static.destroyKeyboard(keyboard);
            keyboard = null;
        }
        keyboard = Keyboard_3D_Static.makeNewKeyboardObjectAndKeys(GameEnums.Keyboard_Type.LowercaseOnly, 0.1f, 0.1f,
                    (string charPressed, string fullString) => { keyPressed(charPressed, fullString); },
                    (string fullString) => { onSubmit(fullString); },
                    (string fullString) => { onCancel(fullString); },
                    null
                  );
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(-3.3f, 0.9f, 2.6f));
        Keyboard_3D_Static.setRotation(keyboard, new Vector3(-90f, -90f, 0f));
        Keyboard_3D_Static.setScale(keyboard, new Vector3(0.3f, 0.3f, 0.3f));
        TMP_Text.text = "";
    }

    void keyPressed(string charPressed, string fullString) {
        TMP_Text.text = fullString;
    }
    void onSubmit(string fullString) {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));
        if (fullString == password) {
            Status.text = "Correct!";
            manager.GetComponent<ScenarioManager>().FlagTriggered();
        } else {
            Status.text = "Incorrect!";
        }
        TMP_Text.text = "";
    }
    void onCancel(string fullString) {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));
        Status.text = "Incorrect!";
        TMP_Text.text = "";
    }
}
