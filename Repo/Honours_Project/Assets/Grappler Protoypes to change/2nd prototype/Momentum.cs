using UnityEngine;
using System.Collections;

public class Momentum : MonoBehaviour
{
    public float velocityMove;
    public float velocityMoveAir;
    public float strengthBalancer;

    private bool noMomentum;
    private bool hung;

    void Start()
    {

    }

    void Update()
    {
        RaycastHit ray;
        noMomentum = Physics.Raycast(transform.position, -transform.up, out ray, 1.5f);
        hung = Grappler.ropeCollided;
        float h = Input.GetAxis("Horizontal");
        if (noMomentum)
        {
            if (h != 0)
            {
                transform.Translate(h * velocityMove * Time.deltaTime, 0, 0);
            }
        }
        else if (!noMomentum && hung)
        {
            GetComponent<Rigidbody>().AddForce(transform.right * h * strengthBalancer);
        }

        else if (noMomentum & !hung)
        {
            if (h != 0)
            {
                transform.Translate(h * velocityMoveAir * Time.deltaTime, 0, 0);
            }
        }
    }
}