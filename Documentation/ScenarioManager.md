# ScenarioManager. By David Li

## Overview
The script in `Assets/Scripts/ScenarioManager.cs` logs and receives events that determine the state of the scene and user progress. Controls UI, Inspection, and completion flags.

Attach `ScenarioManager.cs` to a GameObject designated with the "GameManager" tag. Preferably, this object is out of view or an empty GameObject. Drag your starting and ending UI panels into the corresponding fields in the inspector.

When running the scene, the GameManager will initialize the scene with the starting panel active and will wait for `FlagTriggered()` to activate the ending panel. This object will also limit one object to be inspected at all times. More systems and functions will be added as more complicated scenarios are added.

## Code Documentation

### public void PickUp(GameObject inspect)

Called when an item is picked up to be inspected. Causes the GameManager to remember this object is being held. If another object is already held, `InspectController.ForceStop()` is called to drop it.


### public void PutDown()

Called when an item is put down from being inspected. Causes the GameManager's current inspected object to point to null.

### public void FlagTriggered()

Called when the conditions for completion are done. Sets boolean `flagSet` to true and activates ending panel.



