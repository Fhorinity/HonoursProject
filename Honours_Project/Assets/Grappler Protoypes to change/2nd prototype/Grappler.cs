using UnityEngine;
using System.Collections;

public class Grappler : MonoBehaviour
{
    public float velocity;
    public float ropeSize;
    public float ropeStrength;
    public float weight;

    private GameObject player;
    private Rigidbody rigidbody;
    private SpringJoint ropeEffect;

    private float distanceToPlayer;

    private bool throwRope;
    public static bool ropeCollided;

    //use a line Renderer on when using Trigger.cs & grappler.cs


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        ropeEffect = player.GetComponent<SpringJoint>();

        throwRope = true;
        ropeCollided = false;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            throwRope = false;
        }

        if (throwRope)
        {
            ThrowHook();
        }
        else
        {
            RetrieveHook();
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag != "Player")
        {
            ropeCollided = true;
        }
    }

    public void ThrowHook()
    {
        if (distanceToPlayer <= ropeSize)
        {
            if (!ropeCollided)
            {
                transform.Translate(0, 0, velocity * Time.deltaTime);
            }
            else
            {
                ropeEffect.connectedBody = rigidbody;
                ropeEffect.spring = ropeStrength;
                ropeEffect.damper = weight;
            }
        }

        if (distanceToPlayer > ropeSize)
        {
            throwRope = false;
        }
    }

    public void RetrieveHook()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 25 * Time.deltaTime);

        ropeCollided = false;

        if (distanceToPlayer <= 2)
        {
            Destroy(gameObject);
        }
    }






}