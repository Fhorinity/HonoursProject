using UnityEngine;
using UnityEngine.Events;

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
    public Controls ctrl;

    void Start()
    {
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
            onApplicationMenuPress.Invoke();
        }
        // press
        if (controller.GetPress(applicationMenu))
        {
            onApplicationMenu.Invoke();
        }
        // up
        // GRIP BUTTON
        // down
        if (controller.GetPressDown(gripButton))
        {
            onGripPress.Invoke();             
        }
        // press
        if (controller.GetPress(touchpad)) // touch
        {
            ctrl.axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouchpad.Invoke(ctrl.axis);           
        }
        else if (controller.GetTouch(touchpad)) // touchpad
        {
            ctrl.axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            onTouch.Invoke(ctrl.axis); 
        }
    }   
}