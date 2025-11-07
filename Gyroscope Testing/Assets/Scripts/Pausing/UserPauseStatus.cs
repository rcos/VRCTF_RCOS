using UnityEngine;
using UnityEngine.InputSystem;

public class UserPauseStatus : MonoBehaviour
{
    private PauseBehavior CurrentPauseType = null;
    private GameObject progressBar = null;
    private Transform fillColor = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switchControlType(new PauseBehavior_HoldInteract());
        CreateProgressBar();
    }

    void switchControlType(PauseBehavior switchTo) {
        if (CurrentPauseType != null) CurrentPauseType.OnDelete(gameObject);
        CurrentPauseType = switchTo;
        CurrentPauseType.OnCreate(gameObject);
    }
    private void CreateProgressBar() {
        GameObject prefab = Resources.Load<GameObject>("ProgressBar");
        progressBar = Object.Instantiate(prefab, Camera.main.transform);
        progressBar.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        fillColor = progressBar.transform.Find("FillColor");
    }
    private void updateProgressBar(float fillAmt /* 0.00 - 1.00 */) {
        // fail safes
        if (fillColor == null && progressBar != null) progressBar.transform.Find("FillColor");
        if (progressBar == null) CreateProgressBar();
        
        
        // hide if pausing or fill is 0
        if (fillAmt == 0.0f || StaticPausingFunctions.currentlyPaused || StaticPausingFunctions.currentlyUnPausing || StaticPausingFunctions.currentlyPausing) {
            progressBar.transform.localPosition = new Vector3(999f, 999f, 999f); // hides it
            return;
        }
        // display with proper fill otherwise
        progressBar.transform.localPosition = new Vector3(0f, -0.05f, 0.6f);
        float newX = -0.14f + (fillAmt*0.14f);
        float scaleX = (0.28f*fillAmt);
        fillColor.localPosition = new Vector3(newX, 0f, 0f);
        fillColor.localScale = new Vector3(scaleX, 0.04f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPauseType == null) switchControlType(new PauseBehavior_HoldInteract());
        CurrentPauseType.RunEveryFrame(gameObject);
        updateProgressBar(CurrentPauseType.GetProgressBarSize(gameObject));
    }
}


public class PauseBehavior {
    public virtual void OnCreate(GameObject cur) { Debug.Log("PauseBehavior's OnCreate() function was called"); }
    public virtual void OnDelete(GameObject cur) { Debug.Log("PauseBehavior's OnDelete() function was called"); }
    public virtual void RunEveryFrame(GameObject cur) { Debug.Log("PauseBehavior's RunEveryFrame() function was called"); }
    public virtual float GetProgressBarSize(GameObject cur) { Debug.Log("PauseBehavior's GetProgressBarSize() function was called"); return 0.9f; }
}

public class PauseBehavior_HoldInteract : PauseBehavior {
    public static float AllowedTimeForDroppedHoldInput = 0.05f; // in seconds
    public static float totalTimeHoldRequired = 1f; // in seconds
    private float currentTimeHeld = 0.0f;
    private float currentTimeDropped = 0.0f;

    public override void OnCreate(GameObject cur) {}
    public override void OnDelete(GameObject cur) {}

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
        if (currentTimeHeld < 0.18f) return 0.0f;
        return currentTimeHeld / totalTimeHoldRequired;
    }
}