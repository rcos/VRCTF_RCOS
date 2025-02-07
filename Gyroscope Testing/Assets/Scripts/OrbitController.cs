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
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class OrbitController : MonoBehaviour
{
    public Vector3 inpectPosition;
    public Vector3 inspectScale;

    // The objects are about 1 meter in radius, so the min/max target distance are
    // set so that the objects are always within the room (which is about 5 meters
    // across).
    private const float MinObjectDistance = 2.5f;
    private const float MaxObjectDistance = 3.5f;
    private const float MinObjectHeight = 0.5f;
    private const float MaxObjectHeight = 3.5f;
    
    private bool _spinning;
    private Vector3 _startingPosition;
    private Quaternion _startingRotation;
    private Vector3 _startingScale;
    private Camera _cam;


    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        _spinning = false;
        _startingPosition = transform.localPosition;
        _startingRotation = transform.rotation;
        _startingScale = transform.localScale;
        _cam = Camera.main;
    }

    public void Update()
    {
        if (_spinning) // Spinning is on hold, need to manually make smooth rotation function.
        {
            Debug.Log("SPINNING");
            
            transform.position = Vector3.Lerp(transform.position, inpectPosition, Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, inspectScale, Time.deltaTime * 5);
            
            Vector3 targeted = (transform.position - _cam.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targeted, Vector3.up);
            Quaternion startRotation = _cam.transform.rotation;
            Quaternion differenceRotation = targetRotation * Quaternion.Inverse(startRotation);
            smoothRotate(differenceRotation, targeted);
            
            
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
            Debug.Log("NO SPINNING");
            transform.position = Vector3.Lerp(transform.position, _startingPosition, Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, _startingScale, Time.deltaTime * 5);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _startingRotation, Time.deltaTime * 500f);
        }
    }

    public void smoothRotate(Quaternion direction, Vector3 axis)
    {
        float yAngle = direction.eulerAngles.y;
        if (direction.eulerAngles.y > 180)
        {
            yAngle = 0 - (360 - yAngle);
        }
        Vector3 yAxis = new Vector3(0f, axis.y, 0f);
        float xAngle = direction.eulerAngles.x;
        if (direction.eulerAngles.x > 180)
        {
            xAngle = 0 - (360 - xAngle);
        }
        
        Debug.Log(xAngle);
        transform.RotateAround(transform.position, yAxis, Mathf.Clamp(yAngle * 50f, -300f, 300f) * Time.deltaTime);
        transform.RotateAround(transform.position, _cam.transform.right, Mathf.Clamp(xAngle * 50f, -300f, 300f) * Time.deltaTime);
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
        _spinning = !_spinning;
    }
}
