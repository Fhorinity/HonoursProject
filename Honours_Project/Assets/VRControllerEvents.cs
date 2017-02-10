using UnityEngine;
using UnityEngine.Events;
using System.Collections;


[System.Serializable]
public class TouchpadAxisEvent : UnityEvent<Vector2> { }

[System.Serializable]
public class TriggerAxisEvent : UnityEvent<float> { }

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class VRControllerEvents : MonoBehaviour
{

    public Transform rig;
    private float accelmultipler = 5;
   // public GameObject headset;
    private float deceleration = 4;
    public Transform headset;
    public bool usePlasmaticGrappler = false;
    public bool useGravitationalPulsar = false;
    public bool useMovementControls = false;
    private bool gp_Pick = false;
    private bool gp_DropLaunch = false;
    public GameObject rigArea;

    public float grabDistance = 10.0f;
    public Transform holdPosition;
    private float throwForce = 10.0f;
    public ForceMode throwForceMode;
    public float maxYDim;
    public float speedMultiplier;

    public AnimationCurve forceOverDist;

    private GameObject heldObject = null;
    public LayerMask layerMask = -1;
   // [HideInInspector]
   // public bool isGrounded = true;

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
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;
            
            gp_DropLaunch = true;

        }
        // TRIGGER
        // down
        if (controller.GetPressDown(triggerButton))
        {
            onTriggerPress.Invoke();
            if (usePlasmaticGrappler)
            {

            }
            if (useGravitationalPulsar)
            {
                if (gp_Pick)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, layerMask))
                    {
                        if (hit.collider.gameObject.tag == "Throwable")
                        {
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
                            //If statem if postioning of object passes second trigger it will do the following below.

                            heldObject.GetComponent<Rigidbody>().isKinematic = true;
                            heldObject.GetComponent<Collider>().enabled = true;
                            Debug.DrawRay(transform.position, transform.forward, Color.red);
                            gp_Pick = false;
                            //}
                        }
                    }
                }
                if (gp_DropLaunch)
                {
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
            if (usePlasmaticGrappler)
            {

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

         //   if (isGrounded)
       //     {
                rig.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
        //    }
                   
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

