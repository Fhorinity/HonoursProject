using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePhysicsController : MonoBehaviour
{
    public Joint[] joints;
    public Rigidbody rootRigidbody;

    void GrappleHookImpact()
    {
        Joint[] array = this.joints;
        for (int i = 0; i< array.Length; i++)
        {
            Joint joint = array[i];
            joint.connectedBody = this.rootRigidbody;
        }
    }
}
