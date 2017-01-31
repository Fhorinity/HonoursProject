﻿using UnityEngine;
using System.Collections;

public class Gravitation_Pulser : MonoBehaviour {

    // public bool canSee = false;
    private bool canCarry = true;

    public float grabDistance = 10.0f;
    public Transform holdPosition;
    private float throwForce = 10.0f;
    public ForceMode throwForceMode;
    public float maxYDim;
    public float speedMultiplier;

    public AnimationCurve forceOverDist;

    private GameObject heldObject = null;
    public LayerMask layerMask = -1;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
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

    void Update()
    {
        RaycastHit hit;
        if (controller == null)
        {
            Debug.Log("Controller no initialized");
            return;
        }

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);
        
        // 1st Iteration of Gravity gun //
        if (heldObject == null)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, layerMask))
            {
                // Pick Up Objects // - // ANimation Curve with force
                if (gripButtonDown)
                {
                    if (hit.collider.gameObject.tag == "Throwable")
                    {
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
                        //If statem if postioning of object passes second trigger it will do the following below.

                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        heldObject.GetComponent<Collider>().enabled = true;
                        Debug.DrawRay(transform.position, transform.forward, Color.red);
                        //}
                    }
                }
            }
        }
        else
        {
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;
            
            // Drop Held Objects //
            if (gripButtonDown)
            {
                Rigidbody body = heldObject.GetComponent<Rigidbody>();
                body.isKinematic = false;
                heldObject = null;
                Debug.DrawRay(transform.position, transform.forward, Color.red);
            }

            // Launches Held Objects //
            if (triggerButtonDown)
            {
                Rigidbody body = heldObject.GetComponent<Rigidbody>();
                body.isKinematic = false;
                body.AddForce(throwForce * transform.forward, throwForceMode);
                heldObject = null;
                Debug.DrawRay(transform.position, transform.forward, Color.red);
            }
        }

        // 2nd Iteration of Gravity Gun // 


    }       
}
