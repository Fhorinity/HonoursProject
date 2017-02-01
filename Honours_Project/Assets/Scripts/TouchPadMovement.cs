using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPadMovement : MonoBehaviour {
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private SteamVR_TrackedController controller;
    public GameObject player;
    private float speed = 2f;

	// Use this for initialization
	void Start ()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
        
        //controller.PadClicked += Controller_PadClicked;	
        
	}

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
       /* if (device.GetAxis().x != 0 || device.GetAxis().y != 0)
        {
            Debug.Log(device.GetAxis().x + " " + device.GetAxis().y);
        }*/
    }

    private void Controller_PadTouched(object sender, ClickedEventArgs e)
    {

      //  print(device.GetAxis().x);
        //print(device.GetAxis().y);
       
        if (device.GetAxis().x != 0 || device.GetAxis().y != 0)
        {
            player.transform.position += Vector3.forward * Time.deltaTime * speed;
        }
    }
	// Update is called once per frame
	void Update ()
    {
        controller.PadTouched += Controller_PadTouched;
       
        device = SteamVR_Controller.Input((int)trackedObject.index);	
	}
}
