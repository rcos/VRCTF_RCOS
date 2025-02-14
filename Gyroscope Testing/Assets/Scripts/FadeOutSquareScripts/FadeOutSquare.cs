using UnityEngine;
using FadeOutEnum = GameEnums.FadeOutSquare_PhaseEnum;
using CallbackEnum = GameEnums.FadeOutSquare_CallbackType;

public static class FadeOutSquare_Static
{
    // --------------------------------- Constructor ---------------------------------

    public static GameObject makeNewFadeOutSquare(int fadeIn, int remainFullyVisible, int fadeOut, System.Action<CallbackEnum> toCallWhenAllBlack = null) {
        GameObject prefab = Resources.Load<GameObject>("MovementObjects/FadeOutSquare");
        GameObject fadeOutSquare = Object.Instantiate(prefab, Camera.main.transform);
        fadeOutSquare.GetComponent<FadeOutSquare>().setParameters(fadeIn, remainFullyVisible, fadeOut, toCallWhenAllBlack);
        return fadeOutSquare;
    }

    // --------------------------------- Methods ---------------------------------

    public static void setPhase(GameObject fadeOutSquare, FadeOutEnum phase) {
        fadeOutSquare.GetComponent<FadeOutSquare>().setPhase(phase);
    }
}

public class FadeOutSquare : MonoBehaviour
{
    private int fixedUpdatesToFadeIn;
    private int fixedUpdatesToRemainFullyVisible;
    private int fixedUpdatesToFadeOut;
    System.Action<CallbackEnum> toCallWhenAllBlack;

    private int curNumFixedUpdates;
    private FadeOutEnum curPhase = FadeOutEnum.WaitingForParameters;
    
    void FixedUpdate() {
        if (curPhase == FadeOutEnum.WaitingForParameters) return;

        if (curPhase == FadeOutEnum.FadeIn) {
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, (curNumFixedUpdates / (float)fixedUpdatesToFadeIn));
            if (curNumFixedUpdates >= fixedUpdatesToFadeIn) {
                if (toCallWhenAllBlack != null) toCallWhenAllBlack(CallbackEnum.Natural);
                curPhase = FadeOutEnum.StayFullyVisible;
                curNumFixedUpdates = 0;
            }
        }
        if (curPhase == FadeOutEnum.StayFullyVisible) {
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
            if (curNumFixedUpdates >= fixedUpdatesToRemainFullyVisible) {
                curPhase = FadeOutEnum.FadeOut;
                curNumFixedUpdates = 0;
            }
        }
        if (curPhase == FadeOutEnum.FadeOut) {
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1 - (curNumFixedUpdates / (float)fixedUpdatesToFadeOut));
            if (curNumFixedUpdates >= fixedUpdatesToFadeOut) {
                curPhase = FadeOutEnum.TeleportAway;
                curNumFixedUpdates = 0;
            }
        }
        if (curPhase == FadeOutEnum.TeleportAway) {
            transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
            curPhase = FadeOutEnum.TimerBeforeDelete;
        }
        if (curPhase == FadeOutEnum.TimerBeforeDelete) {
            if (curNumFixedUpdates >= 8) {
                Destroy(this.gameObject);
            }
        }

        curNumFixedUpdates++;
    }

    public void setParameters(int fadeIn, int remainFullyVisible, int fadeOut, System.Action<CallbackEnum> callback) {
        fixedUpdatesToFadeIn = fadeIn;
        fixedUpdatesToRemainFullyVisible = remainFullyVisible;
        fixedUpdatesToFadeOut = fadeOut;
        toCallWhenAllBlack = callback;

        curNumFixedUpdates = 0;
        curPhase = FadeOutEnum.FadeIn;

        GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * (0.01f + Camera.main.nearClipPlane);

        //skip fade in
        if (fadeIn == 0 && remainFullyVisible != 0) {
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1 - (curNumFixedUpdates / (float)fixedUpdatesToFadeOut));
            if (toCallWhenAllBlack != null) toCallWhenAllBlack(CallbackEnum.FromSetParameters);
            curPhase = FadeOutEnum.StayFullyVisible;
            curNumFixedUpdates = 0;
        }
        //skip fade in and fully visible phases
        if (fadeIn == 0 && remainFullyVisible == 0) {
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
            curPhase = FadeOutEnum.FadeOut;
            curNumFixedUpdates = 0;
        }
    }

    public void setPhase(FadeOutEnum phase) {
        curPhase = phase;
        if (phase == FadeOutEnum.FadeIn) {
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        }
        if (phase == FadeOutEnum.StayFullyVisible) {
            if (toCallWhenAllBlack != null) toCallWhenAllBlack(CallbackEnum.FromSettingPhase);
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
        }
        if (phase == FadeOutEnum.FadeOut) {
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
        }
        if (phase == FadeOutEnum.TeleportAway) {
            transform.position = new Vector3(transform.position.x, transform.position.y-2000f, transform.position.z);
        }
        if (phase == FadeOutEnum.TimerBeforeDelete) {
            curNumFixedUpdates = 0;
        }
    }
}
