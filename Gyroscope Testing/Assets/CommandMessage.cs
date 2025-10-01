using UnityEngine;
using UnityEngine.UI;

public class CommandMessage : MonoBehaviour
{  
    private MenuCommands menuCommands;

    public void SetMenuCommands(MenuCommands mc)
    {
        menuCommands = mc;
    }

    public void OnPointerClick()
    {
        if (menuCommands != null)
        {
            string commandText = GetComponentInChildren<Text>().text;
            menuCommands.commandChosen = commandText;
        }
    }
}
