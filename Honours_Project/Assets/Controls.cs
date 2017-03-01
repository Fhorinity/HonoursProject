using UnityEngine;
using UnityEngine.UI;

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

    [HideInInspector]
    public Vector2 axis = Vector2.zero;

    private Grounding groundCheck;
    private VRControllerEvents vrEvents;
    [HideInInspector]
    public bool menuOpen;
    private float accelMultiplier = 5;

    private int index;
    private bool m_selected = false;
    private bool m_highlighted = false;
    private bool strafing;
    private bool b_Cage = false;
    private bool b_TransparentCage = false;
    private bool b_NoCage = false;
    private bool b_Latch = false;
    private bool b_SpecifcLatch = false;
    private bool b_MenuOne = true;
    private bool b_MenuTwo = false;
    private bool b_MenuThree = false;
    private bool b_MenuFour = false;
    private bool b_MenuOptions = false;

    // 1st Menu
    public Text t_Start;
    public Text t_Options;
    public Text t_Credits;
    public Text t_Quit;

    // 2nd Menu
    public Text t_StrafeOn;
    public Text t_StafeOff;

    // 3rd Menu
    public Text t_Cage;
    public Text t_TransparentCage;
    public Text t_NoCage;

    // 4th Menu
    public Text t_Latch;
    public Text t_SpecificLatch;

    // Options Menu
    public Text t_SoundBar;
    public Text t_FXBar;

    private int maxIndex;

    void Start()
    {
        menuOpen = true;
        b_MenuOne = true;
        b_MenuTwo = false;
        b_MenuThree = false;
        b_MenuFour = false;
        b_MenuOptions = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (index > maxIndex)
        {
            index = 0;
        }
        if (menuOpen)
        {
            if (b_MenuOne)
            {
                maxIndex = 4;
                t_Start.enabled = true;
                t_Options.enabled = true;
                t_Credits.enabled = true;
                t_Quit.enabled = true;
            }
            else
            {
                t_Start.enabled = false;
                t_Options.enabled = false;
                t_Credits.enabled = false;
                t_Quit.enabled = false;
            }
            if (b_MenuTwo)
            {
                maxIndex = 2;
                t_StrafeOn.enabled = true;
                t_StafeOff.enabled = true;
            }
            else
            {
                t_StrafeOn.enabled = false;
                t_StafeOff.enabled = false;
            }
            if (b_MenuThree)
            {
                maxIndex = 3;
                t_Cage.enabled = true;
                t_TransparentCage.enabled = true;
                t_NoCage.enabled = true;
            }
            else
            {
                t_Cage.enabled = false;
                t_TransparentCage.enabled = false;
                t_NoCage.enabled = false;
            }
            if (b_MenuFour)
            {
                maxIndex = 2;
                t_Latch.enabled = true;
                t_SpecificLatch.enabled = true;
            }
            else
            {
                t_Latch.enabled = false;
                t_SpecificLatch.enabled = false;
            }
            if (b_MenuOptions)
            {
                //maxIndex = 2;
                t_SoundBar.enabled = true;
                t_FXBar.enabled = true;
            }
            else
            {
                t_SoundBar.enabled = false;
                t_FXBar.enabled = false;
            }
            menuRight.SetActive(true);
            menuLeft.SetActive(true);
            gameLeft.SetActive(false);
            gameRight.SetActive(false);
        }
        else if (!menuOpen)
        {
            b_MenuOne = false;
            b_MenuTwo = false;
            b_MenuThree = false;
            b_MenuFour = false;
            b_MenuOptions = false;

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
            if (strafing)
            {
                rig.position += (headset.transform.right * axis.x + headset.transform.forward * axis.y) * accelMultiplier * Time.deltaTime;
            }
            else
            {
                rig.position += (headset.transform.right + headset.transform.forward * axis.y) * accelMultiplier * Time.deltaTime;
            }
        }
    }

    public void MenuNavigation()
    {       
        if (menuOpen)
        {
            if (vrEvents.leftController)
            {
                if (axis.x > 0.1)
                {
                    index++;
                }
                if (axis.x < -0.1)
                {
                    index--;
                }
                if (axis.y < -0.1)
                {
                    index--;
                }
                if (axis.y > 0.1)
                {
                    index++;
                }
            }
            if (vrEvents.rightController)
            {
                if (axis.y < -0.1)
                {

                }
                if (axis.y > 0.1)
                {

                }
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
