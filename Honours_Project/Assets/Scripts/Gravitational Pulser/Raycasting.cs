using UnityEngine;
using System.Collections;

public class Raycasting : MonoBehaviour {

    private float range = 50f;
    public Camera _camera;
    public bool canSee = false;
    public bool canCarry = false;
    public bool isCarrying = false;
    public GameObject _CarriedObject;


    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ViewportPointToRay(new Vector3(.5f, .5f, .5f));

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            if (hit.collider.tag == "Throwable")
            {
                canSee = true;
                Debug.Log("Hi I can be thrown");
                CarryCheck();


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
