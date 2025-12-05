# Fade Out Transition. By Nicholas Busaba

## Overview

When wanting to teleport, weather to a different location or to a different room we need to temporarily block the camera to avoid motion sickness. The functions and script in `Assets/Scripts/FadeOutSquareScripts/FadeOutSquare.cs` will do all of that for you. You will just need to call one of two functions in the static class. <br><br>

IN MOST CASES YOU JUST NEED TO CALL ONE FUNCTION

#### public static GameObject makeNewFadeOutSquare(int fadeIn, int remainFullyVisible, int fadeOut, System.Action<GameEnums.FadeOutSquare_CallbackType> toCallWhenAllBlack = null)

Example Use case: `FadeOutSquare_Static.makeNewFadeOutSquare(10,10,10, (GameEnums.FadeOutSquare_CallbackType reason) => { transform.position = positionToGoTo; } );`<br><br>

This function makes the black box that is used to block the camera. It will automatically appear and disappear and delete itself when no longer needed. If an existing object already exists, it overrides that one. <br>
&ensp; int fadeIn - the number of Unity's FixedUpdate cycles to make the black square become fully visible. If 0 the square is automatically fully visible and it won't fade in. `toCallWhenAllBlack` is also called.
&ensp; int remainFullyVisible - the number of Unity's FixedUpdate cycles to keep the black square in front of the camera blocking the user's view. If this and `fadeIn` is 0 then the square spawns in fully visible and immediately starts fading out.
&ensp; int fadeOut - the number of Unity's FixedUpdate cycles to make the black square become fully transparent.
&ensp; System.Action<GameEnums.FadeOutSquare_CallbackType> toCallWhenAllBlack - the function that is called when the square turns fully visible. The parameter returns the context in which why the square turned fully black. if it gives `GameEnums.FadeOutSquare_CallbackType.Natural` then the entire fade in timer went through. If it gives `GameEnums.FadeOutSquare_CallbackType.FromSetParameters` then you gave fadein a parameter of 0. If it gives `GameEnums.FadeOutSquare_CallbackType.FromSettingPhase` then you manually set the phase to that where the square is fully visible.
&ensp; returns - the GameObject created. aka the square that blocks the camera

#### public static void setPhase(GameObject fadeOutSquare, GameEnums.FadeOutSquare_PhaseEnum phase)

This function allows you to set the phase of the black square to fade in, fade out, etc. Not recommended to use but you can.
&ensp; GameObject fadeOutSquare - the returned value from the above function. If null it looks for a gameobject called "FadeOutSquare(Clone)" and uses that
&ensp; GameEnums.FadeOutSquare_PhaseEnum phase - Set to `GameEnums.FadeOutSquare_PhaseEnum.WaitingForParameters` to freeze it in whatever state it is currently in. Set to `GameEnums.FadeOutSquare_PhaseEnum.FadeIn` to make it fully invisible and have it restart the fade in. Set to `GameEnums.FadeOutSquare_PhaseEnum.StayFullyVisible` to make it fully visible and have it restart the time it'll remain fully visible. The callback function is also called. Set to `GameEnums.FadeOutSquare_PhaseEnum.FadeOut` to make it fully visible and have it restart the fade out. Set to `GameEnums.FadeOutSquare_PhaseEnum.TeleportAway` to make it teleport far below the player allowing them to interact with other objects again.


