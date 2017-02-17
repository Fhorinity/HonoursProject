using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePhysicsController : MonoBehaviour
{
    public Joint[] joints;
    public Rigidbody rootRigidbody;

    void GrappleHookImpact()
    {
        foreach(var joint in joints)
        {
            joint.connectedBody = rootRigidbody;
        }
    }
}
