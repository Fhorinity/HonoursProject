using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectile : MonoBehaviour
{
    public float speed;
    public float maxRange;
    private float distanceTraveled;

    void Update()
    {
        float maxDistance = this.speed * Time.deltaTime;
        this.distanceTraveled += maxDistance;
        RaycastHit hit;
        if (Physics.Raycast(new Ray(this.transform.position, this.transform.forward), out hit, maxDistance))
        {
            if (hit.collider.tag == "Grapplable Surface")
            {
                maxDistance = hit.distance;
                base.transform.position += base.transform.forward * maxDistance;
                base.transform.parent = hit.transform;
                base.SendMessage("Grapple Hook Impact");
                Object.Destroy((Object)this);
            }
        }
        else
        {
            this.transform.position += this.transform.forward * maxDistance;
            if ((double)this.distanceTraveled <= (double)this.maxRange)
                return;
            Object.Destroy((Object) this.gameObject);
        }
    }
}
