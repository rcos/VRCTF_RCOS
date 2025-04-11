using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    private string typedInput = "";
    // Used to output the message typed on the keyboard
    public TextMeshProUGUI Output;
    public Material Material;
    private Renderer Rend; 

    private void Start()
    {
        // Initialize the Renderer
        Rend = GetComponent<Renderer>();

        if (Output == null)
        {
            Debug.LogError("Output (TextMeshProUGUI) is not assigned in the Inspector.");
        }
    }

    public void initKeyboard()
    {
        this.gameObject.SetActive(true);
    }

    public void destroyKeyboard()
    {
        this.gameObject.SetActive(false);
    }
    
    // Use to set the dimensions of the keyboard
    public void setKeyboardSize(float x, float y, float z)
    {
        this.gameObject.transform.localScale = new Vector3(x, y, z);
    }

    public void setKeyboardMaterial(Material mat)
    {
        if (Rend != null && mat != null)
        {
            Rend.material = mat;
        }
        else
        {
            Debug.LogError("Renderer or material is missing in setKeyboardMaterial.");
        }
    }
    
    public static Vector3 getPosition(GameObject keyboard) { return keyboard.transform.position; }

    public static Vector3 getRotation(GameObject keyboard) { return keyboard.transform.rotation.eulerAngles; }

    public static Vector3 getScale(GameObject keyboard) { return keyboard.transform.localScale; }
    
    public void AddLetter(string keyInput)
    {
        if (keyInput == "Undo") 
        {
            if (typedInput.Length > 0)
            {
                typedInput = typedInput.Substring(0, typedInput.Length - 1); // Removes last character
            }
        }
        else if (keyInput == "Space")
        {
            typedInput += " ";
        }
        else
        {
            typedInput += keyInput;
        }

        // Ensure Output is assigned before updating the text
        if (Output != null)
        {
            Output.text = typedInput;
        }
        else
        {
            Debug.LogError("Output (TextMeshProUGUI) is not assigned.");
        }

        Debug.Log("Current Typed Input: " + typedInput);
    }
}
