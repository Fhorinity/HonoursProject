﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private Camera cam;
	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            cam.enabled = !cam.enabled;
        }
	}
}
