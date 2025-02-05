# Keyboard. By Nicholas Busaba

## Overview
The script in `Assets/Scripts/KeyboardScripts/Keyboard_Parent_Example.cs` shows all the function in use. All you need to do is call methods within a static class and the class will take care of everything for you.
All code for the static class is in `Assets/Scripts/KeyboardScripts/Keybaord_3D.cs` in a class called `Keyboard_3D_Static`. Here are its methods. The keyboard and keys is spawned from the prefab in `Assets/Resources/KeyboardPrefabs/*.prefab`. <br><br>

Also the keyboard can be interacted with by looking at the keys. It will change color to tell you that you have selected it.

#### public static GameObject makeNewKeyboardObject()

ALWAYS CALL THIS FUNCTION FIRST!!!! This makes a new keyboard object and returns the gameobject to it.

#### public static void spawnKeys(GameObject keyboard, int keyboard_type, float hor_margin, float ver_margin, System.Action<string, string> onKeyPress_func, System.Action<string> onSubmit_func, System.Action<string> onCancel_func, System.Action<string> onDestroy_func)

This is the function you should call immediately after the previous one. This places keys on the keyboard and allows it to call functions so the parent knows when attributes about the keyboard object was updated.<br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above
&ensp; int keyboard_type - 0 for normal qwert keyboard (not recommended), 1 for a number pad, 2 for all lowercase keys, 3 for all uppercase keys, and 4 for all lowercase and uppercase letters
&ensp; float hor_margin - margin space between all keys in the horizontal direction (recommend to keep below 1.0f)
&ensp; float ver_margin - margin space between all keys in the vertical direction (recommend to keep below 1.0f)
&ensp; System.Action<string, string> onKeyPress_func - a function that receives a string of the key pressed, and the entire string the user typed on this keyboard. This is called every time a key is pressed
&ensp; System.Action<string> onSubmit_func - a function that receives the entire string the user typed on this keyboard. This is called when the "enter" key is pressed
&ensp; System.Action<string> onCancel_func - a function that receives the entire string the user typed on this keyboard. This is called when the "esc" key is pressed
&ensp; System.Action<string> onDestroy_func - a function that receives the entire string the user typed on this keyboard. This is called when the keyboard is deleted, for whatever reason that may be

#### public static void destroyKeyboard(GameObject keyboard)

This destroys the keyboard passed in the function parameter and removes it from the Unity scene. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above

#### public static void setPosition(GameObject keyboard, Vector3 position)

Does as the name implies. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above
&ensp; Vector3 position - new position to set the keyboard to

#### public static void setRotation(GameObject keyboard, Vector3 rotation)

Does as the name implies. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above
&ensp; Vector3 rotation - new rotation values to set the keyboard to (x,y,z)

#### public static void setScale(GameObject keyboard, Vector3 scale)

Does as the name implies. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above
&ensp; Vector3 scale - new scale values to set the keyboard to

#### public static void setCurrentString(GameObject keyboard, string currentString)

Sets the string the user typed to with the keyboard to the value in the second parameter <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above
&ensp; string currentString - sets the string typed by the user on the keyboard to this value

#### public static Vector3 getPosition(GameObject keyboard)

Does as the name implies. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above

#### public static Vector3 getRotation(GameObject keyboard)

Does as the name implies. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above

#### public static Vector3 getScale(GameObject keyboard)

Does as the name implies. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above

#### public static string getCurrentString(GameObject keyboard)

Gets the string the user typed into the keyboard. <br>
&ensp; GameObject keyboard - result from `makeNewKeyboardObject` function above