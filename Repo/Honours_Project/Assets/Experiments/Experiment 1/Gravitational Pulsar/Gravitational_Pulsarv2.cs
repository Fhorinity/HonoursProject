using System.Collections;
using UnityEngine;

public class Gravitational_Pulsarv2 : MonoBehaviour
{
    public Transform gp_ReferencePoint;
    public LayerMask gp_LayerMask = -1;
    public bool b_Grab;
    public bool b_Fire;
    public bool b_Carrying;
    public bool b_Drop;
    private float grabDistance = 10.0f;
    private float throwForce = 10.0f;
    private ForceMode throwForceMode = ForceMode.Force;
    private GameObject heldObject = null;
    
    void Update()
    {
        if (b_Grab || Input.GetKeyDown(KeyCode.G))
        {
            if (heldObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, gp_LayerMask))
                {
                    heldObject = hit.collider.gameObject;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Collider>().enabled = true;
                    b_Carrying = true;
                }
            }
        }
        else
        {
            heldObject.transform.position = gp_ReferencePoint.position;
            heldObject.transform.rotation = gp_ReferencePoint.rotation;

            //if (b_Carrying)
            //{
                //if (b_Drop || Input.GetKeyDown(KeyCode.T))
                //{
                //    Rigidbody body = heldObject.GetComponent<Rigidbody>();
                //    body.isKinematic = false;
                //    heldObject = null;
                //    b_Carrying = false;
                //}
                if (b_Fire || Input.GetKeyDown(KeyCode.U))
                {
                    Rigidbody body = heldObject.GetComponent<Rigidbody>();
                    body.isKinematic = false;
                    body.AddForce(throwForce * transform.forward, throwForceMode);
                    heldObject = null;
                    b_Carrying = false;
                }
                
            //}
        }
    }
}