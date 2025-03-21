using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject dotToShowNewPosition = null;
    private bool IsValidTeleport = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject prefab1 = Resources.Load<GameObject>("MovementObjects/LocationToGoTo");
        dotToShowNewPosition = Instantiate(prefab1, this.transform);
        dotToShowNewPosition.transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
        IsValidTeleport = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerLeave()
    {
        dotToShowNewPosition.transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
        IsValidTeleport = false;
    }

    public void PointerLooking(RaycastHit hit)
    {
        RaycastHit[] m_Results = new RaycastHit[2];
        int numHits_Center = Physics.RaycastNonAlloc(hit.point + new Vector3(0f,5f,0f), Vector3.down, m_Results, 5.09f, ~0); // ~0 is all layermasks
        int numHits_XPlus = Physics.RaycastNonAlloc(hit.point + new Vector3(0.1f,5f,0f), Vector3.down, m_Results, 5.09f, ~0);
        int numHits_XMinus = Physics.RaycastNonAlloc(hit.point + new Vector3(-0.1f,5f,0f), Vector3.down, m_Results, 5.09f, ~0);
        int numHits_ZPlus = Physics.RaycastNonAlloc(hit.point + new Vector3(0f,5f,0.1f), Vector3.down, m_Results, 5.09f, ~0);
        int numHits_ZMinus = Physics.RaycastNonAlloc(hit.point + new Vector3(0f,5f,-0.1f), Vector3.down, m_Results, 5.09f, ~0);
        bool checkForObstruction = numHits_Center != 1 || numHits_XPlus != 1 || numHits_XMinus != 1 || numHits_ZPlus != 1 || numHits_ZMinus != 1; // only expected to hit floor

        IsValidTeleport = !checkForObstruction;
        if (IsValidTeleport) {
            dotToShowNewPosition.transform.position = new Vector3(hit.point.x, hit.point.y - 0.15f, hit.point.z);
        } else {
            dotToShowNewPosition.transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
        }
    }

    public void OnPointerClickMove(RaycastHit hit)
    {
        if (IsValidTeleport)
        {
            Vector3 positionToGoTo = new Vector3(hit.point.x, 1.2f, hit.point.z);
            FadeOutSquare_Static.makeNewFadeOutSquare(10, 8, 10,
                (GameEnums.FadeOutSquare_CallbackType reason) =>
                {
                    // this is run when screen is fully black
                    transform.parent.transform.position = positionToGoTo;
                }
            );
        }
    }

    public void TransitionArea()
    {
        GameObject fadeOutSquareForNewScene = null;
        fadeOutSquareForNewScene = FadeOutSquare_Static.makeNewFadeOutSquare(10,50,10,
                (GameEnums.FadeOutSquare_CallbackType reason) => {

                    FadeOutSquare_Static.setPhase(fadeOutSquareForNewScene, GameEnums.FadeOutSquare_PhaseEnum.WaitingForParameters);
                });
    }
}