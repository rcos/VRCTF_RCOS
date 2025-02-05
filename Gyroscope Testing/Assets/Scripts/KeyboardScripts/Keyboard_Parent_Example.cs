using UnityEngine;

public class Keyboard_Parent_Example : MonoBehaviour
{
    private GameObject keyboard = null;

    public int keyboard_type = 0;
    public float keyboard_margin_hor = 0f;
    public float keyboard_margin_ver = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            keyboard = Keyboard_3D_Static.makeNewKeyboardObject();
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            Keyboard_3D_Static.spawnKeys(keyboard, keyboard_type, keyboard_margin_hor, keyboard_margin_ver,
                (string charPressed, string fullString) => { keyPressed(charPressed, fullString); },
                (string fullString) => { onSubmit(fullString); },
                (string fullString) => { onCancel(fullString); },
                (string fullString) => { onDestroy(fullString); }
            );
        }
    }

    public void OnPointerClick()
    {
        keyboard = Keyboard_3D_Static.makeNewKeyboardObject();
        Keyboard_3D_Static.spawnKeys(keyboard, keyboard_type, keyboard_margin_hor, keyboard_margin_ver,
            (string charPressed, string fullString) => { keyPressed(charPressed, fullString); },
            (string fullString) => { onSubmit(fullString); },
            (string fullString) => { onCancel(fullString); },
            (string fullString) => { onDestroy(fullString); }
        );
    }

    void keyPressed(string charPressed, string fullString) {
        Debug.Log("Keypressed: " + charPressed + "~~~" + fullString);
    }
    void onSubmit(string fullString) {
        Debug.Log("onSubmit: " + fullString);
        Keyboard_3D_Static.destroyKeyboard(keyboard);
    }
    void onCancel(string fullString) {
        Debug.Log("onCancel: " + fullString);
        Keyboard_3D_Static.destroyKeyboard(keyboard);
    }
    void onDestroy(string fullString) {
        Debug.Log("onDestroy: " + fullString);
    }
}
