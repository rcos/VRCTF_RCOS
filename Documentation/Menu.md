# Menu. By David Li

## Overview
The script in `Assets/Scripts/MenuController.cs` contains the script to give UI panels functionality. NOTE THAT THIS COULD POSSIBLY BE MERGED WITH `TravelController.cs` TO SIMPLIFY UI INTERACTION AND GIVES MORE FLEXIBILITY FOR DIFFERENT SCENARIOS. 

Attach `MenuController.cs` to UI GameObject in the `Interactible` layer. In the Inspector window's Script Component, add functions to the `FinishedEvent()` field that you want called when this button is clicked.
For example, if you want the button to close the menu, add a function, drag in the panel the button is on, and select GameObject.SetActive(). Note that you can call most functions in every script current used in this scene.

When running the scene, interacting with the your UI object will call all the functions you specified.

## Code Documentation

#### [SerializeField] private UnityEvent finishedEvent;

A completely empty UnityEvent with no listeners. The scenario designer must determine what this object will do in the Unity Editor Inspector.


