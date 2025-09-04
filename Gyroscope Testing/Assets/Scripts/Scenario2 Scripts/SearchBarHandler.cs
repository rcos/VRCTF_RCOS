using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SearchBarHandler : MonoBehaviour
{

    public void Start()
    {

    }

    public void OnPointerClick()
    {
        FindFirstObjectByType<KeyboardTestScene_Monitor>().SpawnKeyboard();
    }
    
    public void OnPointerEnter()
    {
    }
}


