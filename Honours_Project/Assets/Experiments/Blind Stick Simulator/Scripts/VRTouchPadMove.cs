using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTouchPadMove : MonoBehaviour
{
    [SerializeField]
    private Transform rig;
    [SerializeField]
    private Transform head;
    private Gravitational_Pulsarv2 pulsar;

    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller {  get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = Vector2.zero;

	void Start ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        pulsar = FindObjectOfType<Gravitational_Pulsarv2>();
	}
	
	void Update ()
    {
	    if (controller == null)
        {
            Debug.Log("Controller not initialised");
            return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (controller.GetPress(triggerButton))
        {
            float delta = controller.GetAxis(triggerButton).x;
            pulsar.b_Grab = true;
            pulsar.b_Fire = true;
        }

        if (controller.GetTouch(touchpad))
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if (rig != null)
            {
                rig.position += (head.right * axis.x + head.forward * axis.y) * Time.deltaTime;
                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
            }
        }
	}
}
