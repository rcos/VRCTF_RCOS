//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class TravelController : MonoBehaviour
{
    [SerializeField] private String sceneName;
    
    // Leftovers from sample
    public Material InactiveMaterial;
    public Material GazedAtMaterial;

    // The objects are about 1 meter in radius, so the min/max target distance are
    // set so that the objects are always within the room (which is about 5 meters
    // across).
    private const float MinObjectDistance = 2.5f;
    private const float MaxObjectDistance = 3.5f;
    private const float MinObjectHeight = 0.5f;
    private const float MaxObjectHeight = 3.5f;

    private GameObject player;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        
    }
    
    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        player.GetComponentInChildren<PlayerMovement>().TransitionArea();
        StartCoroutine(LoadScene());
    }
    
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.Find("EventSystem"));
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncLoad.isDone);

        Scene newScene = SceneManager.GetSceneByName(sceneName);
        if (!newScene.IsValid())
        {
            Debug.Log("Scene is not Valid");
            yield break;
        }

        foreach (var rootObject in newScene.GetRootGameObjects())
        {
            if (rootObject.name == "Player")
            {
                Destroy(rootObject);
                break;
            }
        }
        
        if (currentScene.IsValid())
        {            
            SceneManager.MoveGameObjectToScene(player, newScene);
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene);
            asyncUnload.completed += OnSceneUnloaded;
        } else {
            // An error occurred
            Debug.Log("Scene is not Valid");
        }
    }

    private void OnSceneUnloaded(AsyncOperation obj)
    {
        FadeOutSquare_Static.setPhase(null, GameEnums.FadeOutSquare_PhaseEnum.FadeOut);
    }
}
