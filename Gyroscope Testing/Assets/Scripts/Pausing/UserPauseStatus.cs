using UnityEngine;
using UnityEngine.InputSystem;

public class UserPauseStatus : MonoBehaviour
{
    private PauseBehavior CurrentPauseType;
    private GameObject progressBar;
    private Transform fillColor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentPauseType = new PauseBehavior_HoldInteract();
        CreateProgressBar();
    }

    void CreateProgressBar() {
        GameObject prefab = Resources.Load<GameObject>("ProgressBar");
        progressBar = Object.Instantiate(prefab, Camera.main.transform);
        progressBar.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        fillColor = progressBar.transform.Find("FillColor");
        fillColor.localPosition = new Vector3(0f, 0f, 0f);
    }
    void updateProgressBar(float fillAmt /* 0.00 - 1.00 */) {
        if (fillAmt == 0.0f || StaticPausingFunctions.currentlyPaused || StaticPausingFunctions.currentlyUnPausing || StaticPausingFunctions.currentlyPausing) {
            progressBar.transform.localPosition = new Vector3(0f, 0f, -1f);
            return;
        }
        progressBar.transform.localPosition = new Vector3(0f, -0.05f, 0.6f);

        float newX = -0.14f + (fillAmt*0.14f);
        float scaleX = (0.28f*fillAmt);
        fillColor.localPosition = new Vector3(newX, 0f, 0f);
        fillColor.localScale = new Vector3(scaleX, 0.04f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPauseType.RunEveryFrame(gameObject);
        updateProgressBar(CurrentPauseType.GetProgressBarSize(gameObject));
    }
}


public class PauseBehavior {
    public virtual void RunEveryFrame(GameObject cur) { Debug.Log("PauseBehavior's RunEveryFrame() function was called"); }
    public virtual float GetProgressBarSize(GameObject cur) { Debug.Log("PauseBehavior's GetProgressBarSize() function was called"); return 0.9f; }
}

public class PauseBehavior_HoldInteract : PauseBehavior {
    public static float AllowedTimeForDroppedHoldInput = 0.05f; // in seconds
    public static float totalTimeHoldRequired = 1f; // in seconds
    private float currentTimeHeld = 0.0f;
    private float currentTimeDropped = 0.0f;

    // Checks if interact is held
    public override void RunEveryFrame(GameObject cur) {
        float distanceToIt = 9999f;
        if (GameObject.Find("FadeOutSquare(Clone)")) {
            distanceToIt = Vector3.Distance(cur.transform.position, GameObject.Find("FadeOutSquare(Clone)").transform.position);
        }
        if (distanceToIt < 1900f) {
            currentTimeHeld = 0f;
            return;
        }

        bool isButtonPressed = Google.XR.Cardboard.Api.IsTriggerPressed || Mouse.current.leftButton.isPressed;
        
        currentTimeHeld = (currentTimeHeld > 0.01f || isButtonPressed) ? (currentTimeHeld + Time.deltaTime) : 0;
        currentTimeDropped = (currentTimeHeld > 0.01f && !isButtonPressed) ? (currentTimeDropped + Time.deltaTime) : 0;
        if (currentTimeDropped >= AllowedTimeForDroppedHoldInput) {
            currentTimeHeld = 0.0f;
            currentTimeDropped = 0.0f;
        }

        if (currentTimeHeld > totalTimeHoldRequired) {
            StaticPausingFunctions.PauseGame();
        }
    }
    public override float GetProgressBarSize(GameObject cur) {
        if (currentTimeHeld < 0.1f) return 0.0f;
        return currentTimeHeld / totalTimeHoldRequired;
    }
}