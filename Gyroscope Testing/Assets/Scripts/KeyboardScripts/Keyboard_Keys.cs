using UnityEngine;
using TMPro;

public class Keyboard_Keys : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetComponent<Renderer>().material.color = Color.blue;
        if (transform.parent.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text == " ") {
            transform.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Runs when the camera is first looking at an object
    public void OnPointerEnter()
    {
        transform.GetComponent<Renderer>().material.color = Color.red;
    }

    // Runs when the camera is no longer looking at an object
    public void OnPointerExit()
    {
        transform.GetComponent<Renderer>().material.color = Color.blue;
    }

    // Runs when the phone is tapped on while looking at an object
    public void OnPointerClick()
    {
        string key = transform.parent.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text;
        transform.parent.transform.parent.GetComponent<Keyboard_3D>().keyPressed(key);
    }
}
