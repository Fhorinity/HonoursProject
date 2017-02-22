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

    private bool menuOpen = true;
    // Movement Variables
    public Transform rig;
    [HideInInspector]
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

    [HideInInspector]
    public GameObject menuLeft;
    [HideInInspector]
    public GameObject menuRight;
    [HideInInspector]
    public GameObject gameLeft;
    [HideInInspector]
    public GameObject gameRight;

    // Vive Control Variables //
    public UnityEvent onTriggerPress;
    [HideInInspector] 
    public TriggerAxisEvent onTrigger;
    public UnityEvent onTriggerRelease;
    [HideInInspector]
    public UnityEvent onApplicationMenuPress;
    public UnityEvent onApplicationMenu;
    public UnityEvent onGripPress;
    public TouchpadAxisEvent onTouch;
    [HideInInspector]
    public TouchpadAxisEvent onTouchpadPress;
    public TouchpadAxisEvent onTouchpad;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId applicationMenu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    [HideInInspector]
    public Vector2 axis = Vector2.zero;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
           
    }

    void Update()
    {
        if (menuOpen)
        {
            menuRight.SetActive(true);
            menuLeft.SetActive(true);
            gameLeft.SetActive(false);
            gameRight.SetActive(false);
        }
        else if (!menuOpen)
        {
            menuRight.SetActive(false);
            menuLeft.SetActive(false);
            gameLeft.SetActive(true);
            gameRight.SetActive(true);
        }
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
            menuOpen = !menuOpen;
            onApplicationMenuPress.Invoke();
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
                rig.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));                
        }
        // press
        if (controller.GetPress(touchpad)) // touch
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouchpad.Invoke(axis);
            if (!menuOpen)
                if (useMovementControls)
                    rig.position += (headset.transform.right * axis.x + headset.transform.forward * axis.y) * accelmultipler * Time.deltaTime;
           
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