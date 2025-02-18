using UnityEngine;

public class CameraInit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!Application.isEditor && Application.platform != RuntimePlatform.WindowsPlayer)
        {
            gameObject.SetActive(false);
        }
    }
}
