using UnityEngine;
using TMPro; 
using System.Collections;

public class MenuCommands : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject commandStripPrefab;
    public Vector3 commandOffset = new Vector3(0f, 2, -2); // offset to the right of the object
    public float verticalSpacing = 0.5f; // spacing between commands

    [HideInInspector] public string commandChosen = null;

    private InspectControllerTest inspectController;
    private GameObject[] commandButtons; // store references to generated buttons

    private readonly string[] commands = {"Examine", "Add to Inventory"};

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
        commandButtons = new GameObject[commands.Length + 1];

        Transform roomTransform = transform;
        while (roomTransform != null && roomTransform.name != "Room")
        {
            roomTransform = roomTransform.parent;
        }

        if (roomTransform == null)
        {
            Debug.LogWarning($"No 'Room' ancestor found for {gameObject.name}. Using self as fallback.");
            roomTransform = transform;
        }
        
        for (int i = 0; i < commands.Length + 1; i++)
        {
            GameObject cmdObj = Instantiate(commandStripPrefab, transform);

            if (i == 0) {
                cmdObj.name = gameObject.name;
            }
            else{
                cmdObj.name = commands[i - 1];
                cmdObj.layer = LayerMask.NameToLayer("Interactive");

                // Add CommandMessage script
                var cmdMsg = cmdObj.AddComponent<CommandMessage>();
                cmdMsg.menuCommands = this;
                
            }
            
            commandButtons[i] = cmdObj;
            // Position to the right + stack vertically
            cmdObj.transform.localPosition = commandOffset + new Vector3(0, -i * verticalSpacing, 0);

            // Command strip's size
            Vector3 prefabWorldScale = commandStripPrefab.transform.lossyScale;
            Vector3 roomScale = roomTransform.lossyScale;
            Vector3 parentScale = transform.lossyScale;
            // Determine how big we want it in world space
            Vector3 targetWorldScale = prefabWorldScale * roomScale.magnitude; 
            // Adjust for parent’s scaling
            cmdObj.transform.localScale = new Vector3(
                targetWorldScale.x / parentScale.x,
                targetWorldScale.y / parentScale.y,
                targetWorldScale.z / parentScale.z
            );

            //Command strip's rotation
             if (Camera.main != null)
            {
                float objY = transform.localRotation.eulerAngles.y;
                float finalY = objY - 180f;
                cmdObj.transform.localRotation = Quaternion.Euler(0f, finalY, 0f);
            }

            //Command strip's text
            TextMeshProUGUI tmp = cmdObj.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {   
                tmp.text = (i == 0) ? gameObject.name : commands[i - 1];
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = Color.black;
            }
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
        }
        else if (commandChosen == "Add to Inventory")
        {
            Debug.Log(gameObject.name + " added to inventory!");
        }
    }
}
