using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject dotToShowNewPosition = null;
    bool hideDot = false;

    private Vector3 positionToGoTo = new Vector3(0, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject prefab1 = Resources.Load<GameObject>("MovementObjects/LocationToGoTo");
        dotToShowNewPosition = Instantiate(prefab1, this.transform);
        dotToShowNewPosition.transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
        hideDot = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool checkForFloor = Physics.Raycast(transform.position, transform.forward, out hit, 10f) && hit.transform.tag == "WalkableFloor";
        bool checkForObstruction = true;
        if (checkForFloor) { checkForObstruction = Physics.Raycast(hit.point, Vector3.up, 5f); }
        if (checkForFloor && !checkForObstruction)
        {
            hideDot = true;
            dotToShowNewPosition.transform.position = new Vector3(hit.point.x, hit.point.y - 0.15f, hit.point.z);
            if (Google.XR.Cardboard.Api.IsTriggerPressed || UnityEngine.Input.GetMouseButtonDown(0)) {
                positionToGoTo = new Vector3(hit.point.x, hit.point.y+2f, hit.point.z);
                FadeOutSquare_Static.makeNewFadeOutSquare(10,8,10,
                    (GameEnums.FadeOutSquare_CallbackType reason) => { transform.position = positionToGoTo; }
                );
            }
        } else if (hideDot) {
            dotToShowNewPosition.transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
        }
    }

    public void TransitionArea()
    {
        FadeOutSquare_Static.makeNewFadeOutSquare(10,50,10, (GameEnums.FadeOutSquare_CallbackType reason) => { transform.position = transform.position; } );
    }
}