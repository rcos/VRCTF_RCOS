using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SearchBarHandler : MonoBehaviour, IPointerClickHandler
{
    private GameObject keyboard = null;
    public GameObject emailUI;
    public TMP_InputField searchInput;
    private bool isScenario2 = true;

    private void Start()
    {
        if (searchInput == null)
            searchInput = GetComponent<TMP_InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isScenario2 && emailUI.activeSelf)
        {
            Debug.Log("Search bar clicked, spawning keyboard...");
            SpawnKeyboard();
        }
    }

    private void SpawnKeyboard()
    {
        if (keyboard != null)
        {
            Keyboard_3D_Static.destroyKeyboard(keyboard);
            keyboard = null;
        }

        keyboard = Keyboard_3D_Static.makeNewKeyboardObjectAndKeys(
            GameEnums.Keyboard_Type.LowercaseOnly, 0.1f, 0.1f,
            (charPressed, fullString) => KeyPressed(charPressed, fullString),
            (fullString) => OnSubmit(fullString),
            (fullString) => OnCancel(fullString),
            null
        );

        Keyboard_3D_Static.setPosition(keyboard, new Vector3(-3.3f, 0.9f, 2.6f));
        Keyboard_3D_Static.setRotation(keyboard, new Vector3(-90f, -90f, 0f));
        Keyboard_3D_Static.setScale(keyboard, new Vector3(0.3f, 0.3f, 0.3f));

        searchInput.text = ""; 
    }

    private void KeyPressed(string charPressed, string fullString)
    {
        searchInput.text = fullString;
    }

    private void OnSubmit(string fullString)
    {
        Debug.Log("Search Submitted: " + fullString);
        searchInput.text = fullString;

        EmailManager.Instance.FilterEmails(fullString);
    }

    private void OnCancel(string fullString)
    {
        Debug.Log("Search Canceled.");
        searchInput.text = "";
    }

    /*
    private void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected != null && selected.GetComponent<TMP_InputField>() == searchInput)
        {
            if (keyboard == null)
                SpawnKeyboard();
            else
                keyboard.SetActive(true);
        }
        else
        {
            if (keyboard != null)
                keyboard.SetActive(false);
        }
    }
    */

}


