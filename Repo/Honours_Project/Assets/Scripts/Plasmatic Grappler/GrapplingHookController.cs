using UnityEngine;

public class GrapplingHookController : MonoBehaviour
{
    public GameObject hookProjectile;
    public Rigidbody rootRigidbody;
    public VRControllerEvents vrControllerEvents;
    private bool grappleFired;
    private GameObject lastGrapple;
	
	void Update ()
    {
        if (!grappleFired)
        {
            if (vrControllerEvents.grappleHook)
            {
                GameObject grapple = Object.Instantiate<GameObject>(this.hookProjectile, this.vrControllerEvents.grappleHookOrigin.position,
                    this.vrControllerEvents.grappleHookOrigin.rotation);
                this.lastGrapple = grapple;
                GrapplePhysicsController physics = grapple.GetComponent<GrapplePhysicsController>();
                GrappleRopeController rope = grapple.GetComponent<GrappleRopeController>();

                if ((Object)physics != (Object) null)
                {
                    physics.rootRigidbody = rootRigidbody;
                }
                else
                {
                    Debug.LogWarning("No Hook on Grapple");
                }
                if ((Object)rope != (Object) null)
                {
                    rope.r_Points = new Transform[2];
                    rope.r_Points[0] = grapple.transform;
                    rope.r_Points[1] = vrControllerEvents.grappleHookOrigin;
                }
                else
                {
                    Debug.Log("No Rope on Grapple");
                }
            }
            else
            {
                if ((Object) this.lastGrapple != (Object) null)
                {
                    Object.Destroy((Object) this.lastGrapple);
                    this.lastGrapple = (GameObject) null;
                }
            }
        }
               this.grappleFired = vrControllerEvents.grappleHook;
    }
}
