using System.Collections;
using UnityEngine;

public class Gravitational_Pulsar : MonoBehaviour
{
    [HideInInspector]
    public Transform gp_ReferencePoint;
    [HideInInspector]
    public LayerMask gp_LayerMask = -1;
    [HideInInspector]
    public AnimationCurve forceOverDist;
    [HideInInspector]
    public Animator _Animator;
    [HideInInspector]
    public AudioSource gp_Grab;
    [HideInInspector]
    public AudioSource gp_Drop;
    [HideInInspector]
    public AudioSource gp_Carry;
    [HideInInspector]
    public AudioSource gp_Fire;

    private bool b_Grab = false;
    private bool b_Carrying = false;
    private float grabDistance = 10.0f;
    private float throwForce = 10.0f;
    private float maxYDim;
    private float speedMultiplier;
    private ForceMode throwForceMode = ForceMode.Force;
    private GameObject heldObject = null;
    private ReticleChanger reticleChange;
    [HideInInspector]
    public Vector2 axis = Vector2.zero;
    

    void Update()
    {
        if (heldObject == null)
        {
            b_Grab = true;
            b_Carrying = false;
        }
        else
        {
            heldObject.transform.position = gp_ReferencePoint.position;
            heldObject.transform.rotation = gp_ReferencePoint.rotation;
            b_Carrying = true;
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
        if (gp_Grab)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, gp_LayerMask))
            {
                if (hit.collider.gameObject.tag == "Throwable")
                {
                    reticleChange.inRange = true;
                    _Animator.Play("Grab");

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
                    _Animator.Play("Carry");
                    b_Grab = false;
                    //}
                }
                else
                {
                    reticleChange.inRange = false;
                    _Animator.Play("Error");
                }
            }
        }       
    }
    public void Drop()
    {
        if (axis.x == 0 || axis.y == 0)
        {
            if (b_Carrying)
            {
                _Animator.Play("Fire");
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject = null;
                b_Carrying = false;
            }
        }
    }
    public void Launch()
    {
        if (b_Carrying)
        {
            _Animator.Play("Drop");
            Rigidbody body = heldObject.GetComponent<Rigidbody>();
            body.isKinematic = false;
            body.AddForce(throwForce * transform.forward, throwForceMode);
            heldObject = null;
            b_Carrying = false;
        }
    }
}