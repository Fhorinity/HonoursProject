using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectile : MonoBehaviour
{
    public Transform origin;
    public float speed;
    public float maxRange;

    private float distanceTraveled;
	
	void Start ()
    {
	    	
	}
	
	void Update ()
    {
        float travelDistance = speed * Time.deltaTime;
        distanceTraveled += travelDistance;
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, travelDistance))
        {
            //  if (hit.collider.tag == "Grapplable Surface")
            //{
            travelDistance = hit.distance;
            this.transform.position += this.transform.forward * travelDistance;
            this.transform.parent = hit.transform;
            SendMessage("Grapple Hook Impact");

            Destroy(this);
            //}
        }
        else
        {
            this.transform.position += this.transform.forward * travelDistance;
            if (distanceTraveled > maxRange)
            {
                Destroy(this.gameObject);
            }
        }	
	}
}
