using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandMessage : MonoBehaviour
{  
    public MenuCommands menuCommands;

    public void OnPointerClick()
    {
        if (menuCommands != null)
        {   
            string commandText = GetComponentInChildren<TextMeshProUGUI>().text;
            menuCommands.commandChosen = commandText;
        }
    }
}
