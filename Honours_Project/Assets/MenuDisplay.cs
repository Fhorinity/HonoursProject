using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplay : MonoBehaviour
{
   // public GameObject RightController;
    [HideInInspector]
    public bool menuDisplay;
	// Use this for initialization
	void Start ()
    {
        menuDisplay = true;
	}
	
	// Update is called once per frame

    public void Display()
    {
        menuDisplay = !menuDisplay;
    }
}
