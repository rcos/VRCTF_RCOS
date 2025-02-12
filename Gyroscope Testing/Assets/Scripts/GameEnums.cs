using UnityEngine;

public static class GameEnums
{

    // --------------------------------- FadeOutSquare ---------------------------------

    public enum FadeOutSquare_PhaseEnum
    {
        WaitingForParameters = -1,
        FadeIn = 0,
        StayFullyVisible = 1,
        FadeOut = 2,
        TeleportAway = 3,
        TimerBeforeDelete = 4
    }

    public enum FadeOutSquare_CallbackType
    {
        Natural = 0,
        FromSetParameters = 1,
        FromSettingPhase = 2
    }
}
