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
public class InspectControllerSimple : MonoBehaviour
{
    // Leftovers from sample
    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    
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

    private Vector3 _inspectScale;
    private bool _spinning;
    private Vector3 _startingPosition;
    private Vector3 _inspectPosition;
    private Quaternion _startingRotation;
    private Camera _cam;
    private Renderer _myRenderer;
    private Material[] _outlineMaterials;
    private Material[] _startingMaterials;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        _spinning = false;
        _startingPosition = transform.position;
        _startingRotation = transform.rotation; 
        _inspectScale = transform.localScale;
        _inspectPosition = _startingPosition + Vector3.up;
        _cam = Camera.main;
        onInspect.AddListener(() => GameObject.FindGameObjectWithTag("GameController").GetComponent<ScenarioManager>().PickUp(gameObject));
        offInspect.AddListener(() => GameObject.FindGameObjectWithTag("GameController").GetComponent<ScenarioManager>().PutDown());
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
    

    public void Update()
    { 
        if (_spinning) // Spinning is on hold, need to manually make smooth rotation function.
        {
            transform.position = Vector3.Lerp(transform.position, _inspectPosition, Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, _inspectScale, Time.deltaTime * 5);
            
            Vector3 targeted = (transform.position - _cam.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targeted, Vector3.up);
            Quaternion startRotation = _cam.transform.rotation;
            Quaternion differenceRotation = targetRotation * Quaternion.Inverse(startRotation);
            SmoothRotate(differenceRotation, targetRotation, targeted);
            
            
            // This method sets the object rotation to follow the camera's and stops when it matches. Might be useful later?
            /*
             Vector3 targeted = transform.position - camera.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targeted, Vector3.up);
            Quaternion startRotation = camera.transform.rotation;
            Quaternion differenceRotation = targetRotation * Quaternion.Inverse(startRotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, differenceRotation, Time.deltaTime * 10f);
            */
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _startingPosition, Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, _inspectScale, Time.deltaTime * 5);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _startingRotation, Time.deltaTime * 500f);
        }
    }

    public void SmoothRotate(Quaternion rotationDifference, Quaternion rotationTarget, Vector3 rotationDirection) // Fix the format of this later cause it is currently random if statements at random places
    {
        float yAngle = rotationDifference.eulerAngles.y;
        if (rotationDifference.eulerAngles.y > 180)
        {
            yAngle = 0 - (360 - yAngle);
        }

        if (_cam.transform.position.y > transform.position.y)
        {
            yAngle = -yAngle;
        }
        Vector3 yAxis = new Vector3(0f, rotationDirection.y, 0f);
        float xAngle = rotationDifference.eulerAngles.x;
        if (rotationDifference.eulerAngles.x > 180)
        {
            xAngle = 0 - (360 - xAngle);
        }
        float zAngle = rotationDifference.eulerAngles.z;
        
        if (rotationDifference.eulerAngles.z > 180)
        {
            zAngle = 0 - (360 - zAngle);
        }
        
        Quaternion rotationX = Quaternion.Euler(xAngle, 0, 0);
        Quaternion rotationZ = Quaternion.Euler(0, 0, zAngle);

        // Combine the rotations
        Quaternion combinedRotation = rotationX * rotationZ;
        float combinedAngle = 0f;
        combinedRotation.ToAngleAxis(out combinedAngle, out _);
        float heightDifference = rotationTarget.eulerAngles.x;
        if (heightDifference >= 270f)
        {
            heightDifference = 0 - (360 - heightDifference);
        }
        
        // Maybe could be phrased better
        if ((heightDifference > 0 && _cam.transform.rotation.eulerAngles.x > heightDifference && _cam.transform.rotation.eulerAngles.x <= 90f)
            || (heightDifference < 0 && (_cam.transform.rotation.eulerAngles.x <= 90f || _cam.transform.rotation.eulerAngles.x > 360f+heightDifference)))
        {
            Debug.Log("Reversed > 0");
            combinedAngle = -combinedAngle;
        }

#if UNITY_EDITOR
        if (Math.Abs(yAngle) > 5f)
        {
            transform.RotateAround(transform.position, yAxis, Mathf.Clamp(yAngle * 50f, -300f, 300f) * Time.deltaTime);
        }
        if (Math.Abs(combinedAngle) > 5f)
        {
            transform.RotateAround(transform.position, _cam.transform.right, Mathf.Clamp(combinedAngle * 50f, -300f, 300f) * Time.deltaTime);
        }
#else
        if (Math.Abs(yAngle) > 3f)
        {
            transform.RotateAround(transform.position, yAxis, Mathf.Clamp(yAngle * 50f, -300f, 300f) * Time.deltaTime);
        }
        if (Math.Abs(combinedAngle) > 5f)
        {
            transform.RotateAround(transform.position, _cam.transform.right, Mathf.Clamp(combinedAngle * 50f, -300f, 300f) * Time.deltaTime);
        }
#endif
    }
    
    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        SetMaterial(true);
        Debug.Log("HELLo" + transform.name);
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
#if UNITY_EDITOR
        if (_spinning) // Consolidate this into one later
        {
            offInspect.Invoke();
        }
        else
        {
            onInspect.Invoke();
        }
        _spinning = !_spinning;
#else
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            if (_spinning)
            {
                offInspect.Invoke();
            }
            else
            {
                onInspect.Invoke();
            }
            _spinning = !_spinning;
        }
#endif
    }

    public void ForceStop()
    {
        _spinning = false;
    }
    
    private void SetMaterial(bool gazedAt) 
    {
        if (_startingMaterials != null && _outlineMaterials != null)
        {
            _myRenderer.materials = gazedAt ? _outlineMaterials : _startingMaterials;
        }
    }
}
