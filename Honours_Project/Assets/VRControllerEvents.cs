using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class TouchpadAxisEvent : UnityEvent<Vector2> { }

[System.Serializable]
public class TriggerAxisEvent : UnityEvent<float> { }

[RequireComponent(typeof(SteamVR_TrackedObject))]
//[RequireComponent(typeof(LineRenderer))]
//[RequireComponent(typeof(AudioSource))]
//[RequireComponent(typeof(Animator))]

public class VRControllerEvents : MonoBehaviour
{        
    // Editor Variables
    [HideInInspector]
    public bool usePlasmaticGrappler = false;
   // [HideInInspector]
   // public bool useGravitationalPulsar = false;
    [HideInInspector]
    public bool useMovementControls = false;
    //[HideInInspector]
    //public bool useLeftMenuControls = false;
    //[HideInInspector]
    //public bool useRightMenuControls = false;
    [HideInInspector]
    public bool useGrappleRightControls = false;
    [HideInInspector]
    public bool rightController = false;
    [HideInInspector]
    public bool leftController = false;
    // Menu Varables
    [HideInInspector]
    public bool _OpenMenu = false;
    [HideInInspector]
    public Transform grappleHookOrigin;
    [HideInInspector]
    public bool grappleHook = false;
    [HideInInspector]
    private bool menuOpen = false;
    // Movement Variables
    [SerializeField]
    private Transform rig;
    public Transform headset; 
    private float accelmultipler = 5;
    [HideInInspector]
    public AudioSource walking;
    [HideInInspector]
    public AudioSource running;
    [HideInInspector]
    public AudioSource jumping;
    [HideInInspector]
    public Grounding groundCheck;

    // Vive Control Variables //
    // Trigger
    // press 
 
    public UnityEvent onTriggerPress;
    [HideInInspector]
    // down (float axis) 
    public TriggerAxisEvent onTrigger;
    // up
    public UnityEvent onTriggerRelease;
    [HideInInspector]
    // Application button
    // press
    public UnityEvent onApplicationMenuPress;

    // down
    public UnityEvent onApplicationMenu;
    [HideInInspector]
    // up
    public UnityEvent onApplicationMenuRelease;
    [HideInInspector]
    // grip button
    // press
    public UnityEvent onGripPress;
    [HideInInspector]
    // down
    public UnityEvent onGrip;
    [HideInInspector]
    // up
    public UnityEvent onGripRelease;
    [HideInInspector]
    // touchpad touch
    // press (vector2 axis)
    public TouchpadAxisEvent onTouchPress;
    [HideInInspector]
    // down (vector2 axis)
    public TouchpadAxisEvent onTouch;
    [HideInInspector]
    // up (vector2 axis)
    public TouchpadAxisEvent onTouchRelease;
    [HideInInspector]
    // touchpad press
    // press (vector2 axis)
    public TouchpadAxisEvent onTouchpadPress;
    [HideInInspector]
    // down (vector2 axis)
    public TouchpadAxisEvent onTouchpad;
    [HideInInspector]
    // up (vector2 axis)
    public TouchpadAxisEvent onTouchpadRelease;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId applicationMenu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = Vector2.zero;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
   
    }

    void Update()
    {
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
        }

        // press
        if (controller.GetPress(triggerButton))
        {
            //float delta = controller.hairTriggerDelta;
            float delta = controller.GetAxis(triggerButton).x;
            onTrigger.Invoke(delta);
            grappleHook = true;
        }
        // up
        if (controller.GetPressUp(triggerButton))
        {
            onTriggerRelease.Invoke();
            grappleHook = false;
        }
        // APPLICATION BUTTON
        // down
        if (controller.GetPressDown(applicationMenu))
        {
            onApplicationMenuPress.Invoke();
            if (rightController)
            {
                menuOpen = true;
            }
            if (leftController)
            {
               
            }
        }
        // press
        if (controller.GetPress(applicationMenu))
        {
            grappleHook = true;
                onApplicationMenu.Invoke();
        }
        // up
        // GRIP BUTTON
        // down
        if (controller.GetPressDown(gripButton))
        {
            onGripPress.Invoke();

            if (groundCheck.isGrounding)
            {
                rig.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
            }                 
        }
        // press
        if (controller.GetPress(touchpad)) // touch
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouchpad.Invoke(axis);
            if (!menuOpen)
            {
                if (useMovementControls)
                {

                    rig.position += (headset.transform.right * axis.x + headset.transform.forward * axis.y) * accelmultipler * Time.deltaTime;
                }
                if (useGrappleRightControls)
                {

                }
            }
            if (menuOpen)
            {
                if (leftController)
                {

                }
                if (rightController)
                {

                }
            }
           
        }
        else if (controller.GetTouch(touchpad)) // touchpad
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouch.Invoke(axis);
            if (useMovementControls)
            {
                if (!menuOpen)
                rig.position += (headset.transform.right * axis.x + headset.transform.forward * axis.y) * accelmultipler * Time.deltaTime;
            }  
        }
    }   
}