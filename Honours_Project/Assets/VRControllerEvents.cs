using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class TouchpadAxisEvent : UnityEvent<Vector2> { }
[System.Serializable]
public class TriggerAxisEvent : UnityEvent<float> { }
[RequireComponent(typeof(SteamVR_TrackedObject))]

public class VRControllerEvents : MonoBehaviour
{
    [HideInInspector]
    public bool rightController = false;
    [HideInInspector]
    public bool leftController = false;
    public enum EIndex
    {
        None = -1,
        LeftController,
        RightController
    }
    public EIndex controllerType;
    // Vive Control Variables //
    public UnityEvent onTriggerPress;
    private EventSystem events;
    public Transform rig;
    public Transform headset;
    private Vector2 axis = Vector2.zero;
    private int accelMultiplier = 5;
    [HideInInspector]
    public TriggerAxisEvent onTrigger;
    public UnityEvent onTriggerRelease;
    [HideInInspector]
    public UnityEvent onApplicationMenuPress;
    public UnityEvent onApplicationMenu;
    [HideInInspector]
    public TouchpadAxisEvent onTouchpadPress;
    private Grounding groundCheck;
    public bool menuOpen = true;
    private Rope line;
    private MenuManager menu;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId applicationMenu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private bool strafing = true;

    void Start()
    {
        menu = GetComponent<MenuManager>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        if (controllerType == EIndex.LeftController)
        {
            leftController = true;
        }
        if (controllerType == EIndex.RightController)
        {
            rightController = true;
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
            //menuOpen = !menuOpen;
            onApplicationMenuPress.Invoke();
        }
        // press
        if (controller.GetPress(applicationMenu))
        {
            if (controllerType == EIndex.RightController)
            {
                if (menu.menuState != Type.Main)
                {
                    menuOpen = !menuOpen;
                }
            }
            if (controllerType == EIndex.LeftController)
            {
                Respawn();
            }
        }
        if (controller.GetPressDown(gripButton))
        {
            if (groundCheck.isGrounding)
            {
                rig.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
            }
        }
        if (controller.GetPress(touchpad)) // touch
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            if (controllerType == EIndex.LeftController)
            {
                if (menu.menuState == Type.None)
                {
                    if (menu.b_Strafing)
                    {
                        rig.position += (headset.transform.right * axis.x + headset.transform.forward * axis.y) * accelMultiplier * Time.deltaTime; // With Strafing
                    }
                    else if (!menu.b_Strafing)
                    {
                        rig.position += (headset.transform.forward * axis.y) * accelMultiplier * Time.deltaTime; // Without Strafing
                    }
                }
                if (menu.menuState != Type.None)
                {
                    if (Input.GetButtonDown("Left Horizontal Movement"))
                    {
                        print("Left|Right. Above 0.7");
                    }
                    if (Input.GetButtonDown("Left Vertical Movement"))
                    {
                        print("Up|Down. Below -0.7");
                    }
                }
            }
            if (controllerType == EIndex.RightController) // Rope Lengthen / Shorten
            {
                if (menu.menuState == Type.None)
                {
                    if (axis.y > 0.7)
                    {
                        if (Input.GetButtonDown("Right Press"))
                        {
                            print("No Menu. Above 0.7");
                        }
                    }
                    if (axis.y < -0.7)
                    {
                        if (Input.GetButtonDown("Right Press "))
                            print("No Menu. Below -0.7");
                    }
                }
                if (menu.menuState != Type.None)
                {
                    if (axis.y > 0.7)
                    {
                        if (Input.GetButtonDown("Right Press"))
                        {
                            print("Menu is displaying. Above 0.7");
                        }
                    }
                    if (axis.y < -0.7)
                    {
                        if (Input.GetButtonDown("Right Press "))
                            print("Menu is displaying. Below -0.7");
                    }
                }
            }
        }
        else if (controller.GetTouch(touchpad)) // touchpad
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            if (controllerType == EIndex.LeftController)
            {
                if (menu.menuState == Type.None)
                {
                    if (menu.b_Strafing)
                    {
                        rig.position += (headset.transform.right * axis.x + headset.transform.forward * axis.y) * accelMultiplier * Time.deltaTime; // With Strafing
                    }
                    else if (!menu.b_Strafing)
                    {
                        rig.position += (headset.transform.forward * axis.y) * accelMultiplier * Time.deltaTime; // Without Strafing
                    }
                }
                if (menu.menuState != Type.None)
                {
                    if (Input.GetButtonDown("Left Horizontal Movement"))
                    {
                        print("Touch Left|Right. Above 0.7");
                    }
                    if (Input.GetButtonDown("Left Vertical Movement"))
                    {
                        print("Touch Up|Down. Below -0.7");
                    }
                }
            }
            if (controllerType == EIndex.RightController) // Rope Lengthen / Shorten
            {
                if (menu.menuState == Type.None)
                {
                    if (axis.y > 0.7)
                    {
                        if (Input.GetButtonDown("Right Press"))
                        {
                            print("Touch No Menu. Above 0.7");
                        }
                    }
                    if (axis.y < -0.7)
                    {
                        if (Input.GetButtonDown("Right Press "))
                            print("Touch No Menu. Below -0.7");
                    }
                }
                if (menu.menuState != Type.None)
                {
                    if (axis.y > 0.7)
                    {
                        if (Input.GetButtonDown("Right Press"))
                        {
                            print("Touch Menu is displaying. Above 0.7");
                        }
                    }
                    if (axis.y < -0.7)
                    {
                        if (Input.GetButtonDown("Right Press "))
                            print("Touch Menu is displaying. Below -0.7");
                    }
                }
            }
        }
    }
    public void Respawn()
    {
        print("Resapwning");
    }
}