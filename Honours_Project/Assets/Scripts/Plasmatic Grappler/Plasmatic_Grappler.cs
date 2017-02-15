using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasmatic_Grappler : MonoBehaviour
{
    // public Transform cam;
    // private RaycastHit hit;
    //// private Rigidbody rb;
    // private bool attached = false;
    // private float momentum;
    // public float speed;
    // private float step;
    // private GameObject player;

    // public float maxDistance;
    // public LayerMask layerMask;

    // private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    // //private bool gripButtonDown = false;
    // //private bool gripButtonUp = false;
    // //private bool gripButtonPressed = false;
    // private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    // //private bool triggerButtonDown = false;
    // //private bool triggerButtonUp = false;
    // //private bool triggerButtonPressed = false;

    // private Valve.VR.EVRButtonId dpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    // public bool dpadDown = false;
    // public bool dpadUp = false;
    // public bool dpadLeft = false;
    // public bool dpadRight = false;

    // private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    // private SteamVR_TrackedObject trackedObj;

    // // Use this for initialization
    // void Start()
    // {
    //     player = GameObject.Find("[CameraRig]");
    //     trackedObj = GetComponent<SteamVR_TrackedObject>();

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (controller == null)
    //     {
    //         Debug.Log("Controller no initialized");
    //         return;
    //     }

    //    // gripButtonDown = controller.GetPressDown(gripButton);
    //   //  gripButtonUp = controller.GetPressUp(gripButton);
    //  //   gripButtonPressed = controller.GetPress(gripButton);

    //     dpadDown = controller.GetPressDown(dpad);

    //   //  triggerButtonDown = controller.GetPressDown(triggerButton);
    //     //triggerButtonUp = controller.GetPressUp(triggerButton);
    //     //triggerButtonPressed = controller.GetPress(triggerButton);

    //     if (triggerButtonUp)
    //     {
    //         if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, layerMask))
    //         {
    //             attached = true;

    //             player.GetComponent<Rigidbody>().isKinematic = true;
    //         }
    //     }
    //     if (triggerButtonPressed)
    //     {
    //         attached = false;
    //        player.GetComponent<Rigidbody>().isKinematic = false;
    //         player.GetComponent<Rigidbody>().velocity = cam.forward * momentum;
    //     }

    //     if (attached)
    //     {
    //         momentum += Time.deltaTime * speed;
    //         step = momentum * Time.deltaTime;
    //         transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
    //     }

    //     if (!attached && momentum >= 0)
    //     {
    //         momentum -= Time.deltaTime * 5;
    //         step = 0;
    //     }
    // }
    
    // Plasmatic Grappler Variables //
  //  [HideInInspector]
    public LayerMask pg_LayerMask;
    //[HideInInspector]
    public bool isFlying;
    //[HideInInspector]
    public Vector3 position; //Use the rig?
    //[HideInInspector]
    public float speed = 10;
    //[HideInInspector]
    public Transform pg_ReferencePoint;
    //[HideInInspector]
    public int maxDistance;
    //[HideInInspector]
    public LineRenderer pg_LineRender;

    //[HideInInspector]
    public AudioSource pg_Fire;
    //[HideInInspector]
    public AudioSource pg_Swing;
    //[HideInInspector]
    public AudioSource pg_Release;

    void Update()
    {
        if (isFlying)
        {
            Flying();
        }
    }
    public void Fire_Grapple()
    {
        RaycastHit hit;
        Debug.Log("Grappler Reaches here");
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, pg_LayerMask))
        {
            Debug.Log("Grappler also reaches here");
            isFlying = true;
            position = hit.point;
            pg_LineRender.enabled = true;
            pg_LineRender.SetPosition(1, position);
        }
    }

    public void Release_Grapple()
    {
        if (isFlying)
        {
            isFlying = false;
            pg_LineRender.enabled = false;
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

}
