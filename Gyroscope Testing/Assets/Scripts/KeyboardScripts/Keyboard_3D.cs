using UnityEngine;
using TMPro;

public static class Keyboard_3D_Static
{
    // --------------------------------- "Constructor" ---------------------------------

    public static GameObject makeNewKeyboardObject()
    {
        GameObject prefab = Resources.Load<GameObject>("KeyboardPrefabs/Keyboard_Base"); // Load the prefab from the Resources folder
        if (prefab == null) {
            Debug.LogError("Prefab \"KeyboardPrefabs/Keyboard_Base\" not found in Resources folder");
            return null;
        }
        return Object.Instantiate(prefab);
    }

    // --------------------------------- Other ---------------------------------

    public static void spawnKeys(GameObject keyboard, int keyboard_type, float hor_margin, float ver_margin, System.Action<string, string> onKeyPress_func,
                                    System.Action<string> onSubmit_func, System.Action<string> onCancel_func,
                                    System.Action<string> onDestroy_func) {
        switch (keyboard_type) {
            case 0: //normal qwert keyboard
                Keyboard_3D_Static.setScale(keyboard, new Vector3(0.5f, 1f, 0.5f));
                break;
            case 1: //numberpad
                Keyboard_3D_Static.setScale(keyboard, new Vector3(0.2f, 1f, 0.4f));
                break;
            case 2: //lowercase
                Keyboard_3D_Static.setScale(keyboard, new Vector3(0.5f, 1f, 0.5f));
                break;
            case 3: //uppercase
                Keyboard_3D_Static.setScale(keyboard, new Vector3(0.5f, 1f, 0.5f));
                break;
            case 4: //uppercase and lowercase
                Keyboard_3D_Static.setScale(keyboard, new Vector3(1f, 1f, 1f));
                break;
            default:
                Debug.LogError("Keyboard type not recognized");
                return;
        }
        keyboard.GetComponent<Keyboard_3D>().invokeKeyboardSpawn(keyboard_type, hor_margin, ver_margin, onKeyPress_func, onSubmit_func, onCancel_func, onDestroy_func);
    }

    public static void destroyKeyboard(GameObject keyboard) {
        keyboard.GetComponent<Keyboard_3D>().sendDestroyKeyboardMessage();
        Object.Destroy(keyboard);
    }
    
    // --------------------------------- Setters ---------------------------------

    public static void setPosition(GameObject keyboard, Vector3 position) { keyboard.transform.position = position; }

    public static void setRotation(GameObject keyboard, Vector3 rotation) { keyboard.transform.rotation = Quaternion.Euler(rotation); }

    public static void setScale(GameObject keyboard, Vector3 scale) { keyboard.transform.localScale = scale; }

    public static void setCurrentString(GameObject keyboard, string currentString) { keyboard.GetComponent<Keyboard_3D>().currentString = currentString; }

    // --------------------------------- Getters ---------------------------------

    public static Vector3 getPosition(GameObject keyboard) { return keyboard.transform.position; }

    public static Vector3 getRotation(GameObject keyboard) { return keyboard.transform.rotation.eulerAngles; }

    public static Vector3 getScale(GameObject keyboard) { return keyboard.transform.localScale; }

    public static string getCurrentString(GameObject keyboard) { return keyboard.GetComponent<Keyboard_3D>().currentString; }
}

public class Keyboard_3D : MonoBehaviour
{
    public string currentString = "";
    private GameObject keyPrefab = null;

    private GameObject[][] Allkeys = null;
    private (string, int)[][] KeysValuesAndSizes = null;
    private (int, int, int) currentPosition = (0, 0, 0); // x, y, indexX

    private System.Action<string, string> onKeyPress = null;
    private System.Action<string> onSubmit = null;
    private System.Action<string> onCancel = null;
    private System.Action<string> onDestroy = null;

    private bool showKeyHighlighted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Allkeys != null) {
            if (Input.GetKeyDown(KeyCode.I)) { updatedHighlightedKey(0, -1); }
            if (Input.GetKeyDown(KeyCode.J)) { updatedHighlightedKey(-1, 0); }
            if (Input.GetKeyDown(KeyCode.K)) { updatedHighlightedKey(0, 1); }
            if (Input.GetKeyDown(KeyCode.L)) { updatedHighlightedKey(1, 0); }
            if (Input.GetKeyDown(KeyCode.O)) { addToString(); }
        }
    }

    public void invokeKeyboardSpawn(int keyboard_type, float hor_margin, float ver_margin, System.Action<string, string> onKeyPress_func,
                                    System.Action<string> onSubmit_func, System.Action<string> onCancel_func,
                                    System.Action<string> onDestroy_func) {
        keyPrefab = Resources.Load<GameObject>("KeyboardPrefabs/Keyboard_key");
        if (keyPrefab == null) {
            Debug.LogError("Prefab \"KeyboardPrefabs/Keyboard_key\" not found in Resources folder");
            return;
        }
        
        onKeyPress = onKeyPress_func;
        onSubmit = onSubmit_func;
        onCancel = onCancel_func;
        onDestroy = onDestroy_func;

        switch (keyboard_type) {
            case 0: //normal qwert keyboard
                makeKeyboard(hor_margin, ver_margin, new (string, int)[][] {
                    new (string, int)[] {("esc", 1), ("1", 1), ("2", 1), ("3", 1), ("4", 1), ("5", 1), ("6", 1), ("7", 1), ("8", 1), ("9", 1), ("0", 1), ("-", 1), ("=", 1), ("<--", 1)},
                    new (string, int)[] {("Tab", 1), ("q", 1), ("w", 1), ("e", 1), ("r", 1), ("t", 1), ("y", 1), ("u", 1), ("i", 1), ("o", 1), ("p", 1), ("[", 1), ("]", 1), ("\\", 1)},
                    new (string, int)[] {("CAPS", 1), ("a", 1), ("s", 1), ("d", 1), ("f", 1), ("g", 1), ("h", 1), ("j", 1), ("k", 1), ("l", 1), (";", 1), ("'", 1), ("Enter", 2)},
                    new (string, int)[] {("Shift", 2), ("z", 1), ("x", 1), ("c", 1), ("v", 1), ("b", 1), ("n", 1), ("m", 1), (",", 1), (".", 1), ("/", 1), ("Shift", 2)},
                    new (string, int)[] {("Ctrl", 2), ("Alt", 2), ("Space", 6), ("Alt", 2), ("Ctrl", 2)}
                });
                break;
            case 1: //numberpad
                makeKeyboard(hor_margin, ver_margin, new (string, int)[][] {
                    new (string, int)[] {("esc", 2), ("<--", 2)},
                    new (string, int)[] {("0", 1), ("1", 1), ("2", 1), ("3", 1)},
                    new (string, int)[] {("4", 1), ("5", 1), ("6", 1), ("7", 1)},
                    new (string, int)[] {("8", 1), ("9", 1), ("0", 1), ("enter", 1)},
                });
                break;
            case 2: //lowercase
                makeKeyboard(hor_margin, ver_margin, new (string, int)[][] {
                    new (string, int)[] {("esc", 1), ("q", 1), ("w", 1), ("e", 1), ("r", 1), ("t", 1), ("y", 1), ("u", 1), ("i", 1), ("o", 1), ("p", 1), ("<--", 1)},
                    new (string, int)[] {(" ", 1), ("a", 1), ("s", 1), ("d", 1), ("f", 1), ("g", 1), ("h", 1), ("j", 1), ("k", 1), ("l", 1), (" ", 1)},
                    new (string, int)[] {(" ", 2), ("z", 1), ("x", 1), ("c", 1), ("v", 1), ("b", 1), ("n", 1), ("m", 1), (",", 1), (".", 1), (" ", 2)},
                    new (string, int)[] {(" ", 2), ("Space", 6), ("enter", 2)}
                });
                break;
            case 3: //uppercase
                makeKeyboard(hor_margin, ver_margin, new (string, int)[][] {
                    new (string, int)[] {("esc", 1), ("Q", 1), ("W", 1), ("E", 1), ("R", 1), ("T", 1), ("Y", 1), ("U", 1), ("I", 1), ("O", 1), ("P", 1), ("<--", 1)},
                    new (string, int)[] {(" ", 1), ("A", 1), ("S", 1), ("D", 1), ("F", 1), ("G", 1), ("H", 1), ("J", 1), ("K", 1), ("L", 1), (" ", 2)},
                    new (string, int)[] {(" ", 2), ("Z", 1), ("X", 1), ("C", 1), ("V", 1), ("B", 1), ("N", 1), ("M", 1), (" ", 4)},
                    new (string, int)[] {(" ", 2), ("Space", 6), ("enter", 2)}
                });
                break;
            case 4: //uppercase and lowercase
                makeKeyboard(hor_margin, ver_margin, new (string, int)[][] {
                    new (string, int)[] {("A", 1), ("B", 1), ("C", 1), ("D", 1), ("E", 1), ("F", 1), ("G", 1), ("H", 1), ("I", 1), ("J", 1), ("K", 1), ("L", 1), ("M", 1)},
                    new (string, int)[] {("N", 1), ("O", 1), ("P", 1), ("Q", 1), ("R", 1), ("S", 1), ("T", 1), ("U", 1), ("V", 1), ("W", 1), ("X", 1), ("Y", 1), ("Z", 1)},
                    new (string, int)[] {("a", 1), ("b", 1), ("c", 1), ("d", 1), ("e", 1), ("f", 1), ("g", 1), ("h", 1), ("i", 1), ("j", 1), ("k", 1), ("l", 1), ("m", 1)},
                    new (string, int)[] {("n", 1), ("o", 1), ("p", 1), ("q", 1), ("r", 1), ("s", 1), ("t", 1), ("u", 1), ("v", 1), ("w", 1), ("x", 1), ("y", 1), ("z", 1)},
                    new (string, int)[] {("esc", 4), ("Space", 9), ("enter", 4)}
                });
                break;
            default:
                Debug.LogError("Keyboard type not recognized");
                break;
        }
    }

    public void keyPressed(string key) {
        addToString(key);
    }

    void addToString(string c = null) {
        if (c == null) c = KeysValuesAndSizes[currentPosition.Item2][currentPosition.Item3].Item1;

        switch (c.ToLower()) {
            case "<--":
            case "<-":
            case "backspace":
                if (currentString.Length > 0) {
                    currentString = currentString.Substring(0, currentString.Length - 1);
                }
                break;
            case "space":
                currentString += " ";
                break;
            case "enter":
                if (onSubmit != null) {
                    onSubmit(currentString);
                }
                return;
            case "esc":
            case "cancel":
                if (onCancel != null) {
                    onCancel(currentString);
                }
                return;
            default:
                currentString += c;
                break;
        }
        if (onKeyPress != null) {
            onKeyPress(c, currentString);
        }
    }

    /* Expects deltaX and deltaY to be either -1, 0, or 1. Only one parameter should have a non-zero value */
    void updatedHighlightedKey(int deltaX, int deltaY) {
        if (!showKeyHighlighted) { return; }
        if (Allkeys == null) { return; }

        int x = currentPosition.Item1;
        int y = currentPosition.Item2;
        int indexX = currentPosition.Item3;
        if (x + deltaX >= 0 /* && x + deltaX < Allkeys[y].Length */) {
            if (deltaX == 1) {
                x += KeysValuesAndSizes[y][indexX].Item2; // deltaX == 1
            } else {
                x += deltaX; // deltaX == -1
            }
        }
        if (y + deltaY >= 0 && y + deltaY < Allkeys.Length) {
            y += deltaY;
        }

        int distanceX = 0;
        int i = 0;
        for (; i < KeysValuesAndSizes[y].Length; i++) {
            distanceX += KeysValuesAndSizes[y][i].Item2;
            if (x < distanceX) {
                indexX = i;
                x = distanceX - KeysValuesAndSizes[y][i].Item2;
                break;
            }
        }
        if (i == KeysValuesAndSizes[y].Length) {
            indexX = i-1;
            x = distanceX - KeysValuesAndSizes[y][i-1].Item2;
        }
        // Debug.Log("x: " + x + ", y: " + y + ", indexX: " + indexX + ", distanceX: " + distanceX);

        Allkeys[currentPosition.Item2][currentPosition.Item3].transform.Find("default").GetComponent<Renderer>().material.color = Color.blue;
        currentPosition = (x, y, indexX);
        Allkeys[y][indexX].transform.Find("default").GetComponent<Renderer>().material.color = Color.red;
    }

    void destroyAllPreviousKeys() {
        if (Allkeys != null) {
            for (int i = 0; i < Allkeys.Length; i++) {
                for (int j = 0; j < Allkeys[i].Length; j++) {
                    Destroy(Allkeys[i][j]);
                }
            }
        }
        Allkeys = null;
        KeysValuesAndSizes = null;
        currentPosition = (0, 0, 0);
        currentString = "";
    }

    public void sendDestroyKeyboardMessage() {
        // destroyAllPreviousKeys();
        if (onDestroy != null) {
            onDestroy(currentString);
        }
    }

    void makeKeyboard(float marginSpacing_Hor, float marginSpacing_Ver, (string, int)[][] keysAndSizes) {
        destroyAllPreviousKeys();
        KeysValuesAndSizes = keysAndSizes;

        Allkeys = new GameObject[keysAndSizes.Length][];
        for (int i = 0; i < keysAndSizes.Length; i++) {
            makeRow(marginSpacing_Hor, marginSpacing_Ver, keysAndSizes.Length, i, keysAndSizes[i]);
        }
        updatedHighlightedKey(0, 0);
    }

    void makeRow(float marginSpacing_Hor, float marginSpacing_Ver, int totalRows, int rowNumber, (string, int)[] keysAndSizes)
    {
        Vector3 oldScale = transform.localScale;
        transform.localScale = new Vector3(1, 1, 1);
        
        Mesh mesh = transform.Find("default").GetComponent<MeshFilter>().mesh;
        Vector3 center = new Vector3(mesh.bounds.center.x + transform.position.x, mesh.bounds.center.y + transform.position.y, mesh.bounds.center.z + transform.position.z);
        Vector3 bounds = new Vector3(mesh.bounds.size.x, mesh.bounds.size.y, mesh.bounds.size.z);
        Mesh keyMesh = keyPrefab.transform.Find("default").GetComponent<MeshFilter>().sharedMesh;
        Vector3 keyBounds = new Vector3(keyMesh.bounds.size.x, keyMesh.bounds.size.y, keyMesh.bounds.size.z);
        
        Allkeys[rowNumber] = new GameObject[keysAndSizes.Length];
        int totalKeys = keysAndSizes.Length;
        int totalSizes = 0;
        for (int i = 0; i < totalKeys; i++) { totalSizes += keysAndSizes[i].Item2; }
        
        float totalMarginSpace_Hor = marginSpacing_Hor * (totalSizes+1);
        float widthPerKey = (bounds.x - totalMarginSpace_Hor) / (totalSizes);
        
        float totalMarginSpace_Ver = marginSpacing_Ver * (totalRows);
        float heightPerKey = (bounds.z - totalMarginSpace_Ver) / (totalRows);

        for (int i = 0; i < totalKeys; i++)
        {
            Allkeys[rowNumber][i] = Instantiate(keyPrefab, transform);

            float originalWidth = Allkeys[rowNumber][i].transform.Find("default").GetComponent<MeshFilter>().mesh.bounds.size.x;
            float originalHeight= Allkeys[rowNumber][i].transform.Find("default").GetComponent<MeshFilter>().mesh.bounds.size.z;

            float ThisWidth = (widthPerKey * keysAndSizes[i].Item2) + (marginSpacing_Hor * (keysAndSizes[i].Item2-1));
            Allkeys[rowNumber][i].GetComponent<Transform>().localScale = new Vector3(ThisWidth/originalWidth, 1, heightPerKey/originalHeight);

            int sizesTillHere = 0;
            for (int j = 0; j < i; j++) { sizesTillHere += keysAndSizes[j].Item2; }
            float XPos = (center.x-(bounds.x/2)) + (ThisWidth/2) + (marginSpacing_Hor) + ((widthPerKey+marginSpacing_Hor) * (sizesTillHere)); // (XPos in center, account for that) + (width of all keys before this one) + (initial margin)
            float YPos = Allkeys[rowNumber][i].GetComponent<Transform>().position.y;// + 0.1f;
            float ZPos = (center.z+(bounds.z/2)) - (heightPerKey/2) - (marginSpacing_Ver) - ((heightPerKey+marginSpacing_Ver) * (rowNumber));
            Allkeys[rowNumber][i].GetComponent<Transform>().position = new Vector3(XPos, YPos, ZPos);
            
            
            Transform childTransform = Allkeys[rowNumber][i].transform.Find("Text (TMP)");
            childTransform.GetComponent<TextMeshPro>().text = keysAndSizes[i].Item1;

            //Allkeys[rowNumber][i].transform.Find("default").GetComponent<Renderer>().material.color = Color.blue;
        }

        transform.localScale = oldScale;
    }
}
