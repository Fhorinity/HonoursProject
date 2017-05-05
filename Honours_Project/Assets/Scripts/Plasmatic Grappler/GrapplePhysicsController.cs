using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePhysicsController : MonoBehaviour
{
    public Joint[] joints;
    public Rigidbody rootRigidbody;

    private void GrappleHookImpact()
    {
        foreach (Joint joint in this.joints)
            joint.connectedBody = this.rootRigidbody;
    }
}
