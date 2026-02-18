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
public class OutlineRender : MonoBehaviour
{
    
    private Renderer _myRenderer;
    private Material[] _outlineMaterials;
    private Material[] _startingMaterials;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();
        _startingMaterials = _myRenderer.materials;
        _outlineMaterials = new Material[_startingMaterials.Length + 1];
        for (int i = 0; i < _startingMaterials.Length; i++)
        {
            _outlineMaterials[i] = _startingMaterials[i];
        }
        _outlineMaterials[^1] = Resources.Load<Material>("Materials/Outline");
        SetMaterial(false);
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
    
    private void SetMaterial(bool gazedAt) 
    {
        if (_startingMaterials != null && _outlineMaterials != null)
        {
            _myRenderer.materials = gazedAt ? _outlineMaterials : _startingMaterials;
        }
    }
}
