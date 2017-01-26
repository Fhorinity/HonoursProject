using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasmatic_Grappler : MonoBehaviour
{
    public Transform cam;
    private RaycastHit hit;
    private Rigidbody rb;
    private bool attached = false;
    private float momentum;
    public float speed;
    private float step;

    public float maxDistance;
    public LayerMask layerMask;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller no initialized");
            return;
        }

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        if (triggerButtonPressed)
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, layerMask))
            {
                attached = true;
                rb.isKinematic = true;
            }
        }
        if (triggerButtonUp)
        {
            attached = false;
            rb.isKinematic = false;
            rb.velocity = cam.forward * momentum;
        }

        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }

        if (!attached && momentum >= 0)
        {
            momentum -= Time.deltaTime * 5;
            step = 0;
        }
    }
}
