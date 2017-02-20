using UnityEngine;

public class GrapplingHookController : MonoBehaviour
{
    public GameObject hookProjectile;
    public Rigidbody rootRigidbody;

    public VRControllerEvents vrControllerEvents;

    private bool grappleFired;
    private GameObject lastGrapple;
	
	// Update is called once per frame
	void Update ()
    {
	    //if (!grappleFired)
     //   {
     //       if (vrControllerEvents.grappleHook)
     //       {
     //   ////        GameObject grapple = Object.Instantiate<GameObject>(this.hookProjectile, this.vrControllerEvents.grappleHookOrigin.position,
        ////            this.vrControllerEvents.grappleHookOrigin.rotation);
        //       // this.lastGrapple = grapple;
        //        GrapplePhysicsController physics = grapple.GetComponent<GrapplePhysicsController>();
        //        GrappleRopeController rope = grapple.GetComponent<GrappleRopeController>();
                
        //        if (physics != null)
        //        {
        //            physics.rootRigidbody = rootRigidbody;
        //        }
        //        else
        //        {
        //            Debug.LogWarning("No Hook on Grapple");
        //        }
        //        if (rope != null)
        //        {
        //            rope.r_Points = new Transform[2];
        //            rope.r_Points[0] = grapple.transform;
        //            rope.r_Points[1] = vrControllerEvents.grappleHookOrigin;
        //        }
        //        else
        //        {
        //            Debug.Log("No Rope on Grapple");
        //        }
        //    }
        //    else
        //    {
        //        if (this.lastGrapple != null)
        //        {
        //            Object.Destroy(this.lastGrapple);
        //            this.lastGrapple = null;
        //        }
        //    }
        //}
 //       this.grappleFired = vrControllerEvents.grappleHook; // This is referring to bool to activate application menu.
	}
}
