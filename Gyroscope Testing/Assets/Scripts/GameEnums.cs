using UnityEngine;

public static class GameEnums
{
    // --------------------------------- FadeOutSquare ---------------------------------

    public enum Keyboard_Type
    {
        qwert = 0,
        numberpad = 1,
        LowercaseOnly = 2,
        UppercaseOnly = 3,
        UpperAndLowerCase = 4
    }

    // --------------------------------- FadeOutSquare ---------------------------------

    public enum FadeOutSquare_PhaseEnum
    {
        WaitingForParameters = -1,
        FadeIn = 0,
        StayFullyVisible = 1,
        FadeOut = 2,
        TeleportAway = 3
    }

    public enum FadeOutSquare_CallbackType
    {
        Natural = 0,
        FromSetParameters = 1,
        FromSettingPhase = 2
    }
}
