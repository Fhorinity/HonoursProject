using UnityEngine;
using UnityEngine.Events;
using System.Collections;


[System.Serializable]
public class TouchpadAxisEvent : UnityEvent<Vector2> { }

[System.Serializable]
public class TriggerAxisEvent : UnityEvent<float> { }

[RequireComponent(typeof(SteamVR_TrackedObject))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class VRControllerEvents : MonoBehaviour
{        
    // Editor Variables
    [HideInInspector]
    public bool usePlasmaticGrappler = false;
    [HideInInspector]
    public bool useGravitationalPulsar = false;
    [HideInInspector]
    public bool useMovementControls = false;
    [HideInInspector]
    public bool useLeftMenuControls = false;
    [HideInInspector]
    public bool useRightMenuControls = false;

    // Menu Varables
    [HideInInspector]
    public bool _OpenMenu = false;

    // Movement Variables
    [HideInInspector]
    public Transform rig;
    private float accelmultipler = 5;
    private float deceleration = 4;
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


    // Gravitiational Pulsar Variables
    private bool gp_Pick = false;
    private bool gp_DropLaunch = false;
    [HideInInspector]
    public GameObject rigArea;
    [HideInInspector]
    public float grabDistance = 10.0f;
    [HideInInspector]
    public Transform gp_ReferencePoint;   
    private float throwForce = 10.0f;
    [HideInInspector]
    public ForceMode throwForceMode;
    [HideInInspector]
    public float maxYDim;
    [HideInInspector]
    public float speedMultiplier;
    [HideInInspector]
    public AnimationCurve forceOverDist;
    private GameObject heldObject = null;
    [HideInInspector]
    public LayerMask gp_LayerMask;

    [HideInInspector]
    public Animator _Animator;
    [HideInInspector]
    public AudioSource gp_Grab;
    [HideInInspector]
    public AudioSource gp_Drop;
    [HideInInspector]
    public AudioSource gp_Carry;
    [HideInInspector]
    public AudioSource gp_Fire;

    // Plasmatic Grappler Variables //
    [HideInInspector]
    public LayerMask pg_LayerMask;
    [HideInInspector]
    public bool isFlying;
    [HideInInspector]
    public Vector3 position; //Use the rig?
    [HideInInspector]
    public float speed = 10;
    [HideInInspector]
    public Transform pg_ReferencePoint;
    [HideInInspector]
    public int maxDistance;
    [HideInInspector]
    public LineRenderer pg_LineRender;

    [HideInInspector]
    public AudioSource pg_Fire;
    [HideInInspector]
    public AudioSource pg_Swing;
    [HideInInspector]
    public AudioSource pg_Release;

  

    // Vive Control Variables //
    // Trigger
    // press 
    [HideInInspector]
    public UnityEvent onTriggerPress;
    [HideInInspector]
    // down (float axis) 
    public TriggerAxisEvent onTrigger;
    [HideInInspector]
    // up
    public UnityEvent onTriggerRelease;
    [HideInInspector]
    // Application button
    // press
    public UnityEvent onApplicationMenuPress;
    [HideInInspector]
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

    IEnumerator _GrabbedObject()
    {
        float curveTime = 0f;
        float curveAmount = forceOverDist.Evaluate(curveTime);
        while (curveAmount < 1.0f)
        {
            curveTime += Time.deltaTime * speedMultiplier;
            curveAmount = forceOverDist.Evaluate(curveTime);
            yield return null;
        }
    }
    public void Flying()
    {
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime / Vector3.Distance(transform.position, position));
        pg_LineRender.SetPosition(0, pg_ReferencePoint.position);

        if (Vector3.Distance(transform.position, position) < 0.5f)
        {
            isFlying = false;
            pg_LineRender.enabled = false;
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        //// 1st Iteration of Gravity gun //
        if (heldObject == null)
        {
            gp_Pick = true;
            gp_DropLaunch = false;
        }
        else
        {
            heldObject.transform.position = gp_ReferencePoint.position;
            heldObject.transform.rotation = gp_ReferencePoint.rotation;
            
            gp_DropLaunch = true;

        }

        if (isFlying)
        {
            Flying();
        }
        // TRIGGER
        // down
        if (controller.GetPressDown(triggerButton))
        {
            onTriggerPress.Invoke();
            if (usePlasmaticGrappler)
            {
                pg_LineRender = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
                if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, pg_LayerMask))
                {
                    isFlying = true;
                    position = hit.point;
                    pg_LineRender.enabled = true;
                    pg_LineRender.SetPosition(1, position);
                }
            }
            if (useGravitationalPulsar)
            {
                if (gp_Pick)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, gp_LayerMask))
                    {
                        if (hit.collider.gameObject.tag == "Throwable")
                        {
                            //_Animator.Play("Grab");

                            heldObject = hit.collider.gameObject;
                            // Add Force to object
                            //Maybe use
                            // StartCoroutine(_GrabbedObject()); //?
                            //heldObject.GetComponent<Rigidbody>().MovePosition()
                            // If statement if positioning of the object hits a trigger then apply some force below it
                            // if (triggerOne)
                            //{
                            // Addforce on Y position
                            // Addforce increases faster the closer it gets
                            // }
                            //If (triggerTwo)
                            //{
                            //If state of positioning of object passes second trigger it will do the following below.

                            heldObject.GetComponent<Rigidbody>().isKinematic = true;
                            heldObject.GetComponent<Collider>().enabled = true;
                            Debug.DrawRay(transform.position, transform.forward, Color.red);
                            //_Animator.Play("Carry");
                            gp_Pick = false;
                            //}
                        }
                        else
                        {
                            //_Animator.Play("Error");
                        }
                    }
                }
                if (gp_DropLaunch)
                {
                    //_Animator.Play("Fire");
                    Rigidbody body = heldObject.GetComponent<Rigidbody>();
                    body.isKinematic = false;
                    body.AddForce(throwForce * transform.forward, throwForceMode);
                    heldObject = null;
                    Debug.DrawRay(transform.position, transform.forward, Color.red);
                    gp_DropLaunch = false;
                }
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
            if (usePlasmaticGrappler && isFlying)
            {
                isFlying = false;
                pg_LineRender.enabled = false;
            }
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
            if (usePlasmaticGrappler)
            {

            }
            if (useGravitationalPulsar)
            {
                onApplicationMenu.Invoke();
                if (gp_DropLaunch)
                {
                    //_Animator.Play("Drop");
                    Rigidbody body = heldObject.GetComponent<Rigidbody>();
                    body.isKinematic = false;
                    heldObject = null;
                    Debug.DrawRay(transform.position, transform.forward, Color.red);
                    gp_DropLaunch = false;
                }
            }
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
            Debug.Log(axis);
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