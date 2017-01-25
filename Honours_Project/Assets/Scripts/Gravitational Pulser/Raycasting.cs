using UnityEngine;
using System.Collections;

public class Raycasting : MonoBehaviour {

    private float range = 50f;
    public Camera _camera;
    public bool canSee = false;
    public bool canCarry = false;
    public bool isCarrying = false;
    public GameObject _CarriedObject;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;


    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        RaycastHit hit;
        GameObject gameObject = null;
       // Ray ray = _camera.ViewportPointToRay(new Vector3(.5f, .5f, .5f));

        if (controller == null)
        {
            Debug.Log("Controller no initialized");
            return;
        }

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);


        if(triggerButtonDown)
                {
            canSee = true;
            Debug.Log("Hi I can be thrown");


            CarryCheck();
        }

        if (triggerButtonUp)
        {
            Debug.Log("Unpressed");
        }

        if (triggerButtonPressed)
        {
            Debug.Log("Held Down");
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            gameObject = hit.collider.gameObject;

            
            if (hit.collider.gameObject.tag == "Throwable")
            {
                Debug.Log("Blah");

            }
            else
            {
                canSee = false;
            }
            
            


        }
    }

    void CarryCheck()
    {
        if (Input.GetMouseButtonDown(1) && canSee == true)
        {
            Debug.Log("Pull to me");
            isCarrying = true;
            // Puesdo Intention
            // add force to object to pull towards player
            // Always make sure the force is directed at the player
            //Once the position of the object is within a certain range slighlty rise the Y position value
            // Once at height that is level with reference point
            // Set cancarry bool to true;
            // Attach object to camera as child to camera.
            //Set up if statement to check it is carrying and whilst being carried
            //if hits a object / wall it its orientation will change
            // but as soon as no collision left it will snap back to the orientation it was picked up in.

            if (isCarrying)
            {
                DropCheck();
                LaunchCheck();
            }
            

        }
    }
    void DropCheck()
    {
        if (Input.GetMouseButtonDown(1) && isCarrying == true)
        {
            Debug.Log("Dropped object");
            isCarrying = false;
            // Set up code for dropping object where it is.
        }
    }
    void LaunchCheck()
    {
        if (Input.GetMouseButtonDown(1) && isCarrying == true)
        {
            Debug.Log("Launched Object");
            isCarrying = false;
            //Set up code for launching object.
            //Use add force etc and fire in a forward direction to where the mouse is facing.
        }
    }
        
}
