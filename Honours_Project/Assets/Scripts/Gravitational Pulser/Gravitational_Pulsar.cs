using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitational_Pulsar : MonoBehaviour
{
    // Gravitiational Pulsar Variables
    private bool gp_Pick = false;
    private bool gp_DropLaunch = false;
  //  [HideInInspector]
    public GameObject rigArea;
  //  [HideInInspector]
    public float grabDistance = 10.0f;
   // [HideInInspector]
    public Transform gp_ReferencePoint;
    private float throwForce = 10.0f;
  //  [HideInInspector]
    public ForceMode throwForceMode;
  //  [HideInInspector]
    public float maxYDim;
   // [HideInInspector]
    public float speedMultiplier;
  //  [HideInInspector]
    public AnimationCurve forceOverDist;
    private GameObject heldObject = null;
   // [HideInInspector]
    public LayerMask gp_LayerMask = -1;

  //  [HideInInspector]
    public Animator _Animator;
  //  [HideInInspector]
    public AudioSource gp_Grab;
  //  [HideInInspector]
    public AudioSource gp_Drop;
  //  [HideInInspector]
    public AudioSource gp_Carry;
  //  [HideInInspector]
    public AudioSource gp_Fire;

    void Update()
    {
        if (heldObject == null)
        {
            gp_Pick = true;
            // gp_DropLaunch = false;
        }
        else
        {
            heldObject.transform.position = gp_ReferencePoint.position;
            heldObject.transform.rotation = gp_ReferencePoint.rotation;

            gp_DropLaunch = true;

        }
    }

    IEnumerator _GrabbedObject()
    {
        float curveTime = 0f;
        float curveAmount = forceOverDist.Evaluate(curveTime);
        while (curveAmount < 1.0f)
        {
            curveTime += Time.deltaTime * speedMultiplier;
            curveAmount = forceOverDist.Evaluate(curveTime);
            yield return null;
        }
    }

    public void Grab()
    {
        if (gp_Pick)
        {
            RaycastHit hit;
            Debug.Log("Now I'm in here"); // This works
            Debug.DrawRay(transform.position, transform.forward * grabDistance, Color.cyan);
            if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, gp_LayerMask))
            {
                //Debug.DrawRay(transform.position, transform.forward, Color.green);
                Debug.Log("Finally made it into here"); // This doesn't
                if (hit.collider.gameObject.tag == "Throwable")
                {
                    Debug.Log("Somehow made it in here");
                    //_Animator.Play("Grab");

                    heldObject = hit.collider.gameObject;
                    // Add Force to object
                    //Maybe use
                    // StartCoroutine(_GrabbedObject()); //?
                    //heldObject.GetComponent<Rigidbody>().MovePosition()
                    // If statement if positioning of the object hits a trigger then apply some force below it
                    // if (triggerOne)
                    //{
                    // Addforce on Y position
                    // Addforce increases faster the closer it gets
                    // }
                    //If (triggerTwo)
                    //{
                    //If state of positioning of object passes second trigger it will do the following below.

                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Collider>().enabled = true;
                    //Debug.DrawRay(transform.position, transform.forward, Color.red);
                    //_Animator.Play("Carry");
                    gp_Pick = false;
                    //}
                }
                else
                {
                    //_Animator.Play("Error");
                }
            }
            else
            {

                //print(Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, gp_LayerMask));
            }


        }
        
    }

    public void Drop()
    {
        if (gp_DropLaunch)
        {
            //_Animator.Play("Fire");
            Rigidbody body = heldObject.GetComponent<Rigidbody>();
            body.isKinematic = false;
            body.AddForce(throwForce * transform.forward, throwForceMode);
            heldObject = null;
           // Debug.DrawRay(transform.position, transform.forward, Color.red);
            gp_DropLaunch = false;
        }
    }

    public void Launch()
    {
        if (gp_DropLaunch)
        {
            //_Animator.Play("Drop");
            Rigidbody body = heldObject.GetComponent<Rigidbody>();
            body.isKinematic = false;
            heldObject = null;
            //Debug.DrawRay(transform.position, transform.forward, Color.red);
            gp_DropLaunch = false;
        }
    }
}
