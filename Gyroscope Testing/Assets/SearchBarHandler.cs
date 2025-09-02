using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SearchBarHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField searchInput;

    private void Start()
    {
        if (searchInput == null)
            searchInput = GetComponent<TMP_InputField>();

        if (searchInput != null)
            searchInput.onValueChanged.AddListener((text) => EmailManager.Instance.FilterEmails(text));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Search bar clicked");
        FindFirstObjectByType<KeyboardTestScene_Monitor>().SpawnKeyboard();
    }

}


