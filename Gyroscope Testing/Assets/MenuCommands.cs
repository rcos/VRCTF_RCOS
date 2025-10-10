using UnityEngine;
using TMPro; 
using System.Collections;

public class MenuCommands : MonoBehaviour
{
    [Header("UI Settings")]
    public Vector3 commandOffset = new Vector3(0f, 2, -2); // offset to the right of the object
    public float verticalSpacing = 0.5f; // spacing between commands

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
        if (inspectController.IsInspecting)
        {
            inspectController.Activate(); // this will toggle off
        }
        // Only spawn commands if not already shown
        else if (commandButtons == null || commandButtons.Length == 0)
        {   
            GenerateCommands();
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
            cmdObj.transform.SetParent(transform, false);
            Vector3 parentScale = transform.lossyScale;
            cmdObj.transform.localScale = new Vector3(
                1f / parentScale.x,
                0.2f / parentScale.y,
                0.05f / parentScale.z
            );
            
            // Get current Y rotation in degrees
            float currentY = cmdObj.transform.eulerAngles.y;
            // Compute the difference from 180
            float diff = currentY - 180f;
            // Compute new target rotation (90 degrees relative to "ideal" 180)
            float newY = diff - 90f;
            // Apply rotation
            cmdObj.transform.rotation = transform.rotation * Quaternion.Euler(0, newY, 0);


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

    
    private IEnumerator ClearCommandsDelayed()
    {
        yield return null; // wait one frame
        if (commandButtons != null)
        {
            foreach (var btn in commandButtons)
            {
                if (btn != null) {
                    btn.SetActive(false);
                    Destroy(btn, 0.1f);
                }
            
            commandButtons = null;

            }
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(commandChosen))
        {   
            StartCoroutine(ClearCommandsDelayed());
            ExecuteCommand();
            commandChosen = null;     
        }
    }

    private void ExecuteCommand() 
    {
        if (commandChosen == "Examine")
        {
            inspectController.Activate();
            // Log camera rotation
            if (Camera.main != null)
            {
                //Debug.Log("Camera rotation: " + Camera.main.transform.rotation.eulerAngles);
            }
        }
        else if (commandChosen == "Add to Inventory")
        {
            Debug.Log(gameObject.name + " added to inventory!");
        }
    }
}
