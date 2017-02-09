using UnityEngine;
using UnityEngine.Events;
using System.Collections;


[System.Serializable]
public class TouchpadAxisEvent : UnityEvent<Vector2> { }

[System.Serializable]
public class TriggerAxisEvent : UnityEvent<float> { }

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class VRControllerEvents : MonoBehaviour {

    public Transform rig;
    private float accelmultipler = 5;
    public GameObject headset;
    private float deceleration = 4;
    public Transform headsetRotation;
    public bool gravitation_pulsar = false;
    public bool plasmatic_grappler = false;

    // Trigger
    // press 
    public UnityEvent onTriggerPress;

    // down (float axis) 
    public TriggerAxisEvent onTrigger;

    // up
    public UnityEvent onTriggerRelease;


    // Application button
    // press
    public UnityEvent onApplicationMenuPress;

    // down
    public UnityEvent onApplicationMenu;

    // up
    public UnityEvent onApplicationMenuRelease;

    

    // grip button
    // press
    public UnityEvent onGripPress;

    // down
    public UnityEvent onGrip;

    // up
    public UnityEvent onGripRelease;


    // touchpad touch
    // press (vector2 axis)
    public TouchpadAxisEvent onTouchPress;

    // down (vector2 axis)
    public TouchpadAxisEvent onTouch;

    // up (vector2 axis)
    public TouchpadAxisEvent onTouchRelease;



    // touchpad press
    // press (vector2 axis)
    public TouchpadAxisEvent onTouchpadPress;

    // down (vector2 axis)
    public TouchpadAxisEvent onTouchpad;

    // up (vector2 axis)
    public TouchpadAxisEvent onTouchpadRelease;



    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId applicationMenu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = Vector2.zero;
	
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}


	
	
	void Update () {

       if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        // TRIGGER
        // down
        if (controller.GetPressDown(triggerButton))
        {
            onTriggerPress.Invoke();
            if (gravitation_pulsar)
            {

            }
            if (plasmatic_grappler)
            {

            }
        }

        // press
        if (controller.GetPress(triggerButton))
        {
            //float delta = controller.hairTriggerDelta;
            float delta = controller.GetAxis(triggerButton).x;
            onTrigger.Invoke(delta);
        }

        // up
        if (controller.GetPressUp(triggerButton))
        {
            onTriggerRelease.Invoke();
        }


        // APPLICATION BUTTON
        // down
        if (controller.GetPressDown(applicationMenu))
        {
            onApplicationMenuPress.Invoke();
            
        }

        // press
        if (controller.GetPress(applicationMenu))
        {
            onApplicationMenu.Invoke();
        }

        // up
        if (controller.GetPressUp(applicationMenu))
        {
            onApplicationMenuRelease.Invoke();
        }



        // GRIP BUTTON
        // down
        if (controller.GetPressDown(gripButton))
        {
            onGripPress.Invoke();
            if (gravitation_pulsar)
            {

            }
            if (plasmatic_grappler)
            {

            }
        }

        // press
        if (controller.GetPress(gripButton))
        {
            onGrip.Invoke();
        }

        // up
        if (controller.GetPressUp(gripButton))
        {
            onGripRelease.Invoke();
        }





		// TOUCHPAD
		// down
		if (controller.GetPressDown (touchpad)) // touch
		{
			axis = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);
			onTouchpadPress.Invoke (axis);
		} 
		else if (controller.GetTouchDown(touchpad)) // touchpad
		{
			axis = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);
			onTouchPress.Invoke (axis);
            
        }

		// press
		if (controller.GetPress(touchpad)) // touch
		{
			axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
			onTouchpad.Invoke(axis);
            rig.position += new Vector3(axis.x, 0, axis.y) * accelmultipler * Time.deltaTime;


        }
		else if (controller.GetTouch(touchpad)) // touchpad
		{
			axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
			onTouch.Invoke(axis);
            Debug.Log(axis);
            rig.position += new Vector3(axis.x, 0, axis.y) * accelmultipler * Time.deltaTime;
           

		}


		// up
		if (controller.GetPressUp(touchpad)) // touch
		{
			axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
			onTouchpadRelease.Invoke(axis);
            //rig.position -= new Vector3(axis.x, 0, axis.y) * deceleration * Time.deltaTime;
            
        }
		else if (controller.GetTouchUp(touchpad)) // touchpad
		{
			axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
			onTouchRelease.Invoke(axis);
           // rig.position -= new Vector3(axis.x, 0, axis.y) * deceleration * Time.deltaTime;
		}

       
	}
}
