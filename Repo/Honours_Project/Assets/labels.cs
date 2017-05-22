using System;
using UnityEngine;
using UnityEngine.UI;
public class labels : MonoBehaviour {

    private Text mTime; 

	// Use this for initialization
	void Start ()
    {
        mTime = GetComponentInChildren<Text>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        mTime.text = String.Format("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);
        transform.LookAt(Camera.main.transform);	
	}
}
