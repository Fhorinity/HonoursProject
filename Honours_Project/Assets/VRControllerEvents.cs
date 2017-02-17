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
    [HideInInspector]
    public bool useLeftMenuControls = false;
    [HideInInspector]
    public bool useRightMenuControls = false;

    // Menu Varables
    [HideInInspector]
    public bool _OpenMenu = false;

    public Transform grappleHookOrigin;
    public bool grappleHook = false;

    // Movement Variables
    [HideInInspector]
    public Transform rig;
    private float accelmultipler = 5;
  //  private float deceleration = 4;
    [HideInInspector]
    public AudioSource walking;
    [HideInInspector]
    public AudioSource running;
    [HideInInspector]
    public AudioSource jumping;
    //   [HideInInspector]
    //  public Transform headset;
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
       // RaycastHit hit;

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
        }
        // up
        if (controller.GetPressUp(triggerButton))
        {
            onTriggerRelease.Invoke();
            //if (usePlasmaticGrappler && isFlying)
            //{
            //    isFlying = false;
            //    pg_LineRender.enabled = false;
            //}
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
            grappleHook = true;
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

            if (groundCheck.isGrounding)
            {
                rig.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
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
        if (controller.GetPressDown(touchpad)) // touch
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouchpadPress.Invoke(axis);
        }
        else if (controller.GetTouchDown(touchpad)) // touchpad
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouchPress.Invoke(axis);
        }
        // press
        if (controller.GetPress(touchpad)) // touch
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouchpad.Invoke(axis);
            if (useMovementControls)
            {
                rig.position += new Vector3(axis.x, 0, axis.y) * accelmultipler * Time.deltaTime;
               // rig.rotation = headset.rotation;
            }
        }
        else if (controller.GetTouch(touchpad)) // touchpad
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouch.Invoke(axis);
           // Debug.Log(axis);
            if (useMovementControls)
            {
                rig.position += new Vector3(axis.x, 0, axis.y) * accelmultipler * Time.deltaTime;
             //   rig.rotation = headset.rotation; 
            }  
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