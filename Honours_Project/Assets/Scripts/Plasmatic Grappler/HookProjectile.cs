using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectile : MonoBehaviour
{
    //public Transform origin;
    public float speed;
    public float maxRange;
 //   public VRControllerEvents vrControllerEvents;
    private float distanceTraveled;
	
	void Start ()
    {
	    	
	}
	
	void Update ()
    {
        float travelDistance = this.speed * Time.deltaTime;
        this.distanceTraveled += travelDistance;
        Ray ray = new Ray(base.transform.position, base.transform.forward);
        RaycastHit hit;
      //  if (vrControllerEvents.grappleHook)
      //  {
            if (Physics.Raycast(ray, out hit, travelDistance))
            {
                //  if (hit.collider.tag == "Grapplable Surface")
                //{
                travelDistance = hit.distance;
                base.transform.position += base.transform.forward * travelDistance;
                base.transform.parent = hit.transform;
                base.SendMessage("Grapple Hook Impact");
                Object.Destroy(this);
                //}
            }

            else
            {
                base.transform.position += base.transform.forward * travelDistance;
                if (this.distanceTraveled > this.maxRange)
                {
                    Object.Destroy(base.gameObject);
                }
            }
       // }	
	}
}
