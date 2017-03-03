using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    LineRenderer lineRend;
    public Transform startPos;
    public Transform endPos;

	// Use this for initialization
	void Start ()
    {
        lineRend = GetComponent<LineRenderer>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        lineRend.SetPosition(0, startPos.position);
        lineRend.SetPosition(1, endPos.position);	
	}
}
