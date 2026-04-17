using UnityEngine;

public class CabinetController : MonoBehaviour
{
    private GameObject _drawer;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private bool _open;
    void Start()
    {
        GameObject drawer = transform.GetChild(0).gameObject;
        _startPosition = drawer.transform.position;
        _endPosition = drawer.transform.position + new Vector3(0, 0, 1);
        _open = false;
    }


    void OnPointerClick()
    { 
        _open = !_open;
            
        // Uninspect any active objects inside.
    }

    void Update()
    {
        _drawer.transform.position = Vector3.MoveTowards(_drawer.transform.position, _open ? _endPosition : _startPosition, 0.1f * Time.deltaTime);
    }
    
}