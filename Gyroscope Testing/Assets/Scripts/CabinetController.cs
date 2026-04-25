using UnityEngine;

public class CabinetController : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private bool _open;
    void Start()
    {
        _startPosition = transform.position;
        _endPosition = transform.position + new Vector3(0.7f, 0, 0);
        _open = false;
    }


    void OnPointerClick()
    { 
        _open = !_open;
        // Uninspect any active objects inside.
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _open ? _endPosition : _startPosition, 2f * Time.deltaTime);
    }
    
}