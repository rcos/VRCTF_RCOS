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
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class PickupController : MonoBehaviour
{
    // Leftovers from sample
    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    [SerializeField] private Vector3 inspectPosition;
    [SerializeField] private float inspectScaleMultiplier;
    public UnityEvent onInspect;
    public UnityEvent offInspect;
    
    // From sample, but good to keep these in mind
    // The objects are about 1 meter in radius, so the min/max target distance are
    // set so that the objects are always within the room (which is about 5 meters
    // across).
    private const float MinObjectDistance = 2.5f;
    private const float MaxObjectDistance = 3.5f;
    private const float MinObjectHeight = 0.5f;
    private const float MaxObjectHeight = 3.5f;
    
    private Camera _cam;
    private Renderer _myRenderer;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        _cam = Camera.main;
        _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
    }

    public void Update()
    {
        
    }
    
    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        SetMaterial(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        SetMaterial(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        _cam.GetComponent<InventoryManagement>().EnterIntoInventory(gameObject);
        gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.SetActive(false);
    }
    
    private void SetMaterial(bool gazedAt) 
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
