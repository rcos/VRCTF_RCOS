using UnityEngine;
using TMPro;

public class KeyboardTestScene_Button : MonoBehaviour
{
    private GameObject keyboard = null;
    public TextMeshPro TMP_Text;
    
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
        keyboard = Keyboard_3D_Static.makeNewKeyboardObject();
        Keyboard_3D_Static.spawnKeys(keyboard, 2, 0.1f, 0.1f,
                (string charPressed, string fullString) => { keyPressed(charPressed, fullString); },
                (string fullString) => { onSubmit(fullString); },
                (string fullString) => { onCancel(fullString); },
                null
        );
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, 1f, 3.35f));
        TMP_Text.text = "\"\"";
    }

    void keyPressed(string charPressed, string fullString) {
        TMP_Text.text = "\"" + fullString + "\"";
    }
    void onSubmit(string fullString) {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));
        if (fullString == "password") {
            TMP_Text.text = "Correct!";
        } else {
            TMP_Text.text = "Incorrect!";
        }
    }
    void onCancel(string fullString) {
        Keyboard_3D_Static.setPosition(keyboard, new Vector3(0, -2000f, 3.35f));
        TMP_Text.text = "Incorrect!";
    }
}
