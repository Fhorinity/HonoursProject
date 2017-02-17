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
	    if (!grappleFired)
        {
            if (vrControllerEvents.grappleHook)
            {
                var grapple = Instantiate(hookProjectile, vrControllerEvents.grappleHookOrigin.position,
                    vrControllerEvents.grappleHookOrigin.rotation);
                lastGrapple = grapple;
                var proj = grapple.GetComponent<GrapplePhysicsController>();
                var rope = grapple.GetComponent<GrappleRopeController>();
                
                if (proj != null)
                {
                    proj.rootRigidbody = rootRigidbody;
                }
                else
                {
                    Debug.LogWarning("No Hook on Grapple");
                }
                if (rope != null)
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
                if (lastGrapple != null)
                {
                    Destroy(lastGrapple);
                    lastGrapple = null;
                }
            }
        }
        grappleFired = vrControllerEvents.grappleHook; // This is referring to bool to activate application menu.
	}
}
