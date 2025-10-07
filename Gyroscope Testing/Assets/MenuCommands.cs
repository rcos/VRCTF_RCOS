using UnityEngine;
using TMPro; 

public class MenuCommands : MonoBehaviour
{
    [Header("UI Settings")]
    public Vector3 commandOffset = new Vector3(5f, 0, 0); // offset to the right of the object
    public float verticalSpacing = 0.25f; // spacing between commands

    [HideInInspector] public string commandChosen = null;

    private InspectControllerTest inspectController;
    private GameObject[] commandButtons; // store references to generated buttons

    private readonly string[] commands = { "Examine", "Add to Inventory", "Placeholder" };

    private void Start()
    {
        inspectController = GetComponent<InspectControllerTest>();
        if (inspectController == null)
        {
            Debug.LogWarning("InspectController not found on " + gameObject.name);
        }
    }

    public void OnPointerClick()
    {
        // Only spawn commands if not already shown
        if (commandButtons == null || commandButtons.Length == 0)
        {
            GenerateCommands();
        }
        else
        {
            // If commands already exist, remove them
            ClearCommands();
        }
    }

    private void GenerateCommands()
    {
        commandButtons = new GameObject[commands.Length];

        for (int i = 0; i < commands.Length; i++)
        {
            // Create a new 3D text or cube with text (simple placeholder for now)
            GameObject cmdObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cmdObj.layer = LayerMask.NameToLayer("Interactive");
            cmdObj.transform.SetParent(transform);
            cmdObj.transform.localScale = new Vector3(2f, 0.3f, 0.05f);
            cmdObj.transform.Rotate(0, 90, 0);
            cmdObj.name = commands[i];

            // Position to the right + stack vertically
            cmdObj.transform.localPosition = commandOffset + new Vector3(0, -i * verticalSpacing, 0);

            // Add text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(cmdObj.transform, false);
            textObj.layer = LayerMask.NameToLayer("Interactive");

            var tmp = textObj.AddComponent<TextMeshPro>();
            tmp.text = commands[i];
            tmp.fontSize = 2f;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.black;

            // Center text
            textObj.transform.localPosition = new Vector3(0f, 0f, -1f);
            textObj.transform.localRotation = Quaternion.identity;
            textObj.transform.localScale = Vector3.one;

            // Add CommandMessage script
            var cmdMsg = cmdObj.AddComponent<CommandMessage>();
            cmdMsg.menuCommands = this;

            commandButtons[i] = cmdObj;
        }
    }

    
    private void ClearCommands()
    {   
        
        if (commandButtons != null)
        {
            foreach (var btn in commandButtons)
            {
                if (btn != null)
                    Destroy(btn, 0.1f);
            }
        }
        
        commandButtons = null;
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(commandChosen))
        {
            ClearCommands();
            ExecuteCommand();
            commandChosen = null;
        }
    }

    private void ExecuteCommand() 
    {
        if (commandChosen == "Examine")
        {
            if (inspectController != null)
            {
                inspectController.Activate();
            }
        }
        else if (commandChosen == "Add to Inventory")
        {
            Debug.Log(gameObject.name + " added to inventory!");
        }
    }
}
