using UnityEngine;

public class MenuCommands : MonoBehaviour
{
    [Header("Prefabs & UI")]
    public GameObject commandsUIPrefab;   // Prefab with buttons
    public GameObject inventoryUI;        // Inventory canvas/UI

    [HideInInspector] public string commandChosen = null;

    private InspectController inspectController;
    private GameObject activeCommandsUI;

    private void Start()
    {
        inspectController = GetComponent<InspectController>();
        if (inspectController == null)
        {
            Debug.LogWarning("InspectController not found on " + gameObject.name);
        }
    }

    public void OnPointerClick()
    {
        // If no command chosen yet, spawn the menu UI
        if (string.IsNullOrEmpty(commandChosen))
        {
            if (commandsUIPrefab != null && activeCommandsUI == null)
            {
                activeCommandsUI = Instantiate(commandsUIPrefab, transform.position, Quaternion.identity);

                // Optional: offset to the right of the object
                activeCommandsUI.transform.position += transform.right * 0.5f;

                // Make sure it faces the camera
                activeCommandsUI.transform.LookAt(Camera.main.transform);
                activeCommandsUI.transform.Rotate(0, 180, 0); // flip so it's not backwards

                // Wire buttons so they know which MenuCommands they belong to
                CommandMessage[] cmds = activeCommandsUI.GetComponentsInChildren<CommandMessage>();
                foreach (var cmd in cmds)
                {
                    cmd.SetMenuCommands(this);
                }
            }
        }
        else
        {
            ExecuteCommand();
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(commandChosen))
        {
            if (activeCommandsUI != null)
            {
                Destroy(activeCommandsUI);
            }
            ExecuteCommand();
            commandChosen = null; // Reset after execution
        }
    }

    private void ExecuteCommand()
    {
        if (commandChosen == "Examine")
        {
            if (inspectController != null)
            {
                inspectController.Initialize(); 
                inspectController.OnPointerClick(); // toggle spin
            }
        }
        else if (commandChosen == "Add to Inventory")
        {
            if (inventoryUI != null) inventoryUI.SetActive(true);
            Debug.Log(gameObject.name + " added to inventory!");
        }
    }

}
