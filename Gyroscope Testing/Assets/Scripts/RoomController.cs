using UnityEngine;

public class RoomController : MonoBehaviour
{
    private GameObject _player;
    public Vector3 teleportPosition;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void MovePosition()
    {
        _player.transform.position = teleportPosition;
        Debug.Log(_player.transform.position);
    }
}