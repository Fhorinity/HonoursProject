using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Plasmatic : MonoBehaviour
{
    public Camera cam; // Place on camera.
    public RaycastHit hit;

    public LayerMask cullingmask;

    public bool isFlying;
    public Vector3 position;
    public float speed = 10;
    public Transform referencePoint; //Create Empty gameobject for hand (Use vive controller reference point)

    public int maxDistance;

   // public FirstPersonController fpc; // Placed on 1st person controller prefab
    public LineRenderer lr; //This is placed on gameobject that has linerenderer on it

    void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            FindSpot();
        }

        if (isFlying)
        {
            Flying();
        }

        if (Input.GetKey(KeyCode.Space) && isFlying)
        {
            isFlying = false;
       //     fpc.CanMove = true;
            lr.enabled = false;
        }

    }

    public void FindSpot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance, cullingmask))
        {
            isFlying = true;
            position = hit.point;
       //     fpc.CanMove = false;
            lr.enabled = true;
            lr.SetPosition(1, position);
        }
    }

    public void Flying()
    {
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime / Vector3.Distance(transform.position, position));
        lr.SetPosition(0, referencePoint.position);

        if (Vector3.Distance(transform.position, position) < 0.5f)
        {
            isFlying = false;
          //  fpc.CanMove = true;
            lr.enabled = false;
        }
    }
}