using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SearchBarHandler : MonoBehaviour
{   
    public GameObject emailContent;
    public GameObject noResult;

    public void Start()
    {

    }

    public void OnPointerClick()
    {   
        if (noResult.activeSelf) {
            noResult.SetActive(false);
            
            foreach (Transform child in emailContent.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        
        FindFirstObjectByType<KeyboardTestScene_Monitor>().SpawnKeyboard();
    }
    
    public void OnPointerEnter()
    {
    }
}


