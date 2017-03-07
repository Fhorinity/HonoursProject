using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Type
{
    None = -1,
    Main,
    Pause
}
public class MenuManager : MonoBehaviour
{
    private VRControllerEvents vrEvents;
    public GameObject m_One;
    public GameObject m_Two;
    public GameObject m_Three;
    public GameObject m_Four;
    public GameObject m_Options;
    public GameObject m_Credits;
    public GameObject m_Pause;
    public GameObject menuLeft;
    public GameObject menuRight;
    public GameObject gameLeft;
    public GameObject gameRight;
    public GameObject cage;
    public GameObject transCage;
    public List<GameObject> menuArray = new List<GameObject>(); 

    private EventSystem eventSystem;
    public Text t_Play;
    public Text t_Options;
    public Text t_Credits;
    public Text t_Quit;
    public Text t_StrafeOn;
    public Text t_StafeOff;
    public Text t_Cage;
    public Text t_TransparentCage;
    public Text t_NoCage;
    public Text t_Latch;
    public Text t_SpecificLatch;
    public Text t_SoundBar;
    public Text t_FXBar;
    public GameObject menu;
    [HideInInspector]
    public bool b_Strafing;
    private bool b_Cage = false;
    private bool b_TransparentCage = false;
    private bool b_NoCage = false;
    [HideInInspector]
    public bool b_SpecificLatch = false;
    private bool b_MenuCredits = false;
    private bool b_MenuOne = true;
    private bool b_MenuTwo = false;
    private bool b_MenuThree = false;
    private bool b_MenuFour = false;
    private bool b_MenuOptions = false;
    private bool b_MenuPause = true;
    private bool m_Open;
    private bool p_Open;
    public Type menuState; 

    void Start()
    {
        menuState = Type.Main;
        eventSystem = GameObject.FindObjectOfType<EventSystem>().GetComponent<EventSystem>();
    }
    void Update()
    {
        if (menuState == Type.Main) // If Menu State is Main and then you switch to pause it will still display 
        {
            m_Open = true;
            p_Open = false;
            menu.SetActive(true);
            MenuControls();
        }
        if (menuState == Type.Pause)
        {
            p_Open = true;
            m_Open = false;
            menu.SetActive(true);
            MenuControls();
        }
        if (menuState == Type.None)
        {
            p_Open = false;
            m_Open = false;
            menu.SetActive(false);
            GameControls();
        }
        if (p_Open && b_MenuPause)
        {
            m_Pause.SetActive(true);
            eventSystem.SetSelectedGameObject(menuArray[6]);
        }
        else if (p_Open && !b_MenuPause)
        {
            m_Pause.SetActive(false);
        }
        else if (!p_Open && !b_MenuPause)
        {
            m_Pause.SetActive(false);
        }
        if (m_Open && b_MenuOne)
        {
            m_One.SetActive(true);
            eventSystem.SetSelectedGameObject(menuArray[0]);
            
        }
        else if (m_Open && !b_MenuOne)
        {
            m_One.SetActive(false);
        }
        else if (!m_Open && !b_MenuOne)
        {
            m_One.SetActive(false);
        }
        if (b_MenuTwo)
        {
            m_Two.SetActive(true);
            eventSystem.SetSelectedGameObject(menuArray[1]);
       
        }
        else
        {
            m_Two.SetActive(false);
        }
        if (b_MenuThree)
        {
            eventSystem.SetSelectedGameObject(menuArray[2]);
            m_Three.SetActive(true);
        }
        else
        {
            m_Three.SetActive(false);
        }
        if (b_MenuFour)
        {
            m_Four.SetActive(true);
            eventSystem.SetSelectedGameObject(menuArray[3]);
            Debug.Log("Set to true");
        }
        else
        {
            m_Four.SetActive(false);

        }
        if (b_MenuOptions)
        {
            m_Options.SetActive(true);
            eventSystem.SetSelectedGameObject(menuArray[4]);
        }
        else
        {
            m_Options.SetActive(false);
        }
        if (b_MenuCredits)
        {
            m_Credits.SetActive(true);
            eventSystem.SetSelectedGameObject(menuArray[5]);
        }
        else
        {
            m_Credits.SetActive(false);
        }      
    }
    public void MenuControls()
    {
        menuRight.SetActive(true);
        menuLeft.SetActive(true);
        gameLeft.SetActive(false);
        gameRight.SetActive(false);
    }
    public void GameControls()
    {
        menuLeft.SetActive(false);
        menuRight.SetActive(false);
        gameLeft.SetActive(true);
        gameRight.SetActive(true);
    }
    public void Play()
    {
        b_MenuOne = false;
        b_MenuTwo = true;
        Debug.Log("Clicking working");
    }
    public void Options()
    {
        if (p_Open)
        {
            b_MenuPause = false;
            b_MenuOptions = true;
        }
        if (m_Open)
        {
            b_MenuOne = false;
            b_MenuOptions = true;
        }
    }
    public void Credits()
    {
        if (p_Open)
        {
            b_MenuPause = false;
            b_MenuCredits = true;
        }
        if (m_Open)
        {
            b_MenuOne = false;
            b_MenuCredits = true;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Y_Strafing()
    {
        b_Strafing = true;
        if (p_Open)
        {
            b_MenuPause = true;
            b_MenuTwo = false;
        }
        if (m_Open)
        {
            b_MenuTwo = false;
            b_MenuThree = true;
        }
    }
    public void N_Stafing()
    {
        b_Strafing = false;
        if (p_Open)
        {
            b_MenuPause = true;
            b_MenuTwo = false;
        }
        if (m_Open)
        {
            b_MenuTwo = false;
            b_MenuThree = true;
        }
    }
    public void Latch()
    {
        b_SpecificLatch = false;
        if (p_Open)
        {
            b_MenuPause = true;
            b_MenuFour = false;
        }
        if (m_Open)
        {
            b_MenuFour = false;
            menuState = Type.None;
        }
    }
    public void SpecficLatch()
    {
        b_SpecificLatch = true;
        if (p_Open)
        {
            b_MenuPause = true;
            b_MenuFour = false;
        }
       if (m_Open)
        {
            b_MenuFour = false;
            menuState = Type.None;
        }
    }
    public void Cage()
    {
        b_Cage = true;
        cage.SetActive(true);
        if (p_Open)
        {
            b_MenuPause = true;
            b_MenuThree = false;
        }
        if (m_Open)
        {
            b_MenuThree = false;
            b_MenuFour = true;
        }
    }
    public void TransCage()
    {
        b_TransparentCage = true;
        transCage.SetActive(true);
        if (p_Open)
        {
            b_MenuPause = true;
            b_MenuThree = false;
        }
        if (m_Open)
        {
            b_MenuThree = false;
            b_MenuFour = true;
        }
    }
    public void NullCage()
    {
        b_NoCage = true;
        cage.SetActive(false);
        transCage.SetActive(false);
        if (p_Open)
        {
            b_MenuPause = true;
            b_MenuThree = false;
        }
        if (m_Open)
        {
            b_MenuThree = false;
            b_MenuFour = true;
        }
    }
    public void Strafing()
    {
        b_MenuPause = false;
        b_MenuTwo = true;
    }
    public void Latching()
    {
        b_MenuPause = false;
        b_MenuFour = true;
    }
    public void Cages()
    {
        b_MenuPause = false;
        b_MenuThree = true;
    }
    public void Back()
    {
        if (b_MenuTwo == true)
        {
            if (p_Open)
            {
                b_MenuPause = true;
                b_MenuTwo = false;
            }
            if (m_Open)
            {
                b_MenuOne = true;
                b_MenuTwo = false;
            }
        }
        if (b_MenuThree == true)
        {
            if (p_Open)
            {
                b_MenuPause = true;
                b_MenuThree = false;
            }
            if (m_Open)
            {
                b_MenuTwo = true;
                b_MenuThree = false;
            }
        }
        if (b_MenuFour == true)
        {
            if (p_Open)
            {
                b_MenuPause = true;
                b_MenuFour = false;
            }
            if (m_Open)
            {
                b_MenuThree = true;
                b_MenuFour = false;
            }
        }
        if (b_MenuOptions == true)
        {
            if (p_Open)
            {
                b_MenuPause = true;
                b_MenuOptions = false;
            }
            if (m_Open)
            {
                b_MenuOne = true;
                b_MenuOptions = false;
            }
        }
        if (b_MenuCredits == true)
        {
            if (p_Open)
            {
                b_MenuPause = true;
                b_MenuCredits = false;
            }
            if (m_Open)
            {
                b_MenuOne = true;
                b_MenuCredits = false;
            }
        }
    }
}
