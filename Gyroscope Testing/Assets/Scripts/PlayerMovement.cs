using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject dotToShowNewPosition = null;
    private GameObject FadeOutSquare = null;
    bool hideDot = false;

    private const int fadeOutTimerMax = 8;
    private const int bufferTimeBeforeTeleport = fadeOutTimerMax/2;
    private int fadeOutTimerLeft = fadeOutTimerMax;
    private int fadeOutPhase = 4;
    private Vector3 positionToGoTo = new Vector3(0, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject prefab1 = Resources.Load<GameObject>("MovementObjects/LocationToGoTo");
        GameObject prefab2 = Resources.Load<GameObject>("MovementObjects/FadeOutSquare");
        dotToShowNewPosition = Instantiate(prefab1, this.transform);//new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z), Quaternion.identity);
        FadeOutSquare = Instantiate(prefab2, this.transform);//new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z), Quaternion.identity);
        hideDot = true;
        FadeOutSquare.transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
    }

    private void doFadeOut() {
        if (fadeOutPhase == 4) return;
        if (fadeOutTimerLeft > fadeOutTimerMax) { fadeOutPhase++; fadeOutTimerLeft = 0; }

        if (fadeOutPhase == 0) {
            FadeOutSquare.GetComponent<Renderer>().material.color = new Color(0, 0, 0, ((fadeOutTimerLeft-bufferTimeBeforeTeleport) / (float)fadeOutTimerMax));
        }
        if (fadeOutPhase == 1) {
            fadeOutTimerLeft = fadeOutTimerMax;
            transform.position = positionToGoTo;
        }
        if (fadeOutPhase == 2) {
            FadeOutSquare.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1 - ((fadeOutTimerLeft-bufferTimeBeforeTeleport) / (float)fadeOutTimerMax));
        }
        if (fadeOutPhase == 3) {
            fadeOutTimerLeft = fadeOutTimerMax;
            FadeOutSquare.transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
        }
        fadeOutTimerLeft++;
    }

    void FixedUpdate() {
        doFadeOut();
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
                fadeOutTimerLeft = 0;
                fadeOutPhase = 0;
                positionToGoTo = new Vector3(hit.point.x, hit.point.y+2f, hit.point.z);
                FadeOutSquare.transform.position = transform.position + (0.06f*transform.forward);
                FadeOutSquare.transform.rotation = Quaternion.LookRotation(FadeOutSquare.transform.position - transform.position);
            }
        } else if (hideDot) {
            dotToShowNewPosition.transform.position = new Vector3(transform.position.x, transform.position.y-600f, transform.position.z);
        }
    }
}
