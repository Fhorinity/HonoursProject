using UnityEngine;

public class Controls : MonoBehaviour
{
    [HideInInspector]
    public Transform rig;
    [HideInInspector]
    public Transform headset;
    [HideInInspector]
    public GameObject menuLeft;
    [HideInInspector]
    public GameObject menuRight;
    [HideInInspector]
    public GameObject gameLeft;
    [HideInInspector]
    public GameObject gameRight;
    [HideInInspector]
    public AudioSource walking;
    [HideInInspector]
    public AudioSource running;
    [HideInInspector]
    public AudioSource jumping;

    private Grounding groundCheck;
    private VRControllerEvents vrEvents;
    private bool menuOpen = true;
    private float accelMultiplier = 5;
	
	// Update is called once per frame
	void Update ()
    {
        if (menuOpen)
        {
            menuRight.SetActive(true);
            menuLeft.SetActive(true);
            gameLeft.SetActive(false);
            gameRight.SetActive(false);
        }
        else if (!menuOpen)
        {
            menuRight.SetActive(false);
            menuLeft.SetActive(false);
            gameLeft.SetActive(true);
            gameRight.SetActive(true);
        }
    }

    public void Jump()
    {
        if (!menuOpen)
        {
            if (groundCheck.isGrounding)
            rig.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
        }
    }
    public void Movement()
    {
        if (!menuOpen)
        {
            rig.position += (headset.transform.right * vrEvents.axis.x + headset.transform.forward * vrEvents.axis.y) * accelMultiplier * Time.deltaTime;
        }
    }
    public void MenuNavigation()
    {
        if (menuOpen)
        {
            if (vrEvents.leftController)
            {

            }
            if (vrEvents.rightController)
            {

            }
        }
    }
    public void DisplayMenu()
    {
        menuOpen = !menuOpen;
    }
    public void Respawn()
    {

    }
}
