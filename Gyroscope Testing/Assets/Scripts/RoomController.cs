using UnityEngine;

public class RoomController : MonoBehaviour
{
    private GameObject _player;
    public Vector3 teleportPosition;
    public bool requiresCondition;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void MovePosition()
    {
        FadeOutSquare_Static.makeNewFadeOutSquare(10, 8, 10,
            (GameEnums.FadeOutSquare_CallbackType reason) =>
            {
                // this is run when screen is fully black
                _player.transform.position = teleportPosition;
            }
        );
        Debug.Log(_player.transform.position);
    }

    void OnPointerClick()
    {
        if (!requiresCondition)
        {
            MovePosition();
        }
    }
}