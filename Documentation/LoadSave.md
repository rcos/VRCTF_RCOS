# LoadSave. By Sathya Regunathan

## Overview

The scripts under `Assets/Scripts/DataVault` contain the implementation of the load/ save system to both device's persistent file and to firebase firestore. 

Attach `DataManager.cs` to an empty game object; this integrates both the local and database saving functionality. It looks for objects that implement `IDataManager`, which provides an interface for implementing what attributes to load/ save based on the scenario's requirements. Follow the code in `Scenario1Manager.cs` for a rough outline on how to create your own ScenarioManager using the `IDataManager` interface. 

## Overview of Each Component

**GameData.cs**
- The C# object that will be compressed into a JSON file containing game-relevant data. The game-relevant data are:
	- TransformSnapshot structure consisting of the player's position and rotation.
	- The scenario's name.
	- Whether the scenario is completed or not.
	- A dictionary containing the scenario name mapped to all the quests in the scenario and their completion status.
- Each scenario gets its own GameData instance, keeping the scenario's persistent state isolated from the others. 

**ScenarioDefaults.cs**
	- Contains the default values for each scenario's starting transform (position + rotation) and flags.

**DataManager.cs**
- Contains the functions that loads and saves game data. 
- Loads data when game is launched. 
- Saves data when application is quit. 

**IDataManager.cs**
- Interface used to describe the functions used to load and save data. 

**FileDataHandler.cs**
- Saves the entire game progress (GameDataCollection object consists of multiple GameData objects for each scenario) locally. 
- Contain's optional XOR encryption.