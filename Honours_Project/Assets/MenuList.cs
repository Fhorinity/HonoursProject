using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MenuList
{
    public SubMenu[] menus;
    public SubMenu activeMenu;
    public SubMenu lastActive;
    public Text lastSelectedTextField;
    public Text t_Play;
    public Text t_Options;
    public Text t_Credits;
    public Text t_Quit;
    public Text t_StrafeOn;
    public Text t_StrafeOff;
    public Text t_Cage;
    public Text t_TransparentCage;
    public Text t_NoCage;
    public Text t_Latch;
    public Text t_SpecificLatch;
    public Text t_SoundBar;
    public Text t_FXBar;

    public void Playb()
    {
        if (On the player button)
            {
            activeMenu
            activeMenu.setActive(false);

            activeMenu = menus[0];

            lastSelectedTextField = platyButton;

            activeMenu.Setactive(true);
        }

        //other menus

        if (Go back to other menu)
            {
            activeMenu.firstActive.SetActive(false);

            EventSystem.SetCurrentObject(lastSelectedTextField);
        }

    }

    public void Play()
    {
        activeMenu.firstActive.SetActive(false);
        activeMenu = menus[1];
        lastSelectedTextField = t_Play;
        activeMenu.firstActive.SetActive(true);
    }
    public void Options()
    {
        if (p_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[2];
            lastSelectedTextField = t_Options;
            activeMenu.firstActive.SetActive(true);
        }
        if (m_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[2];
            lastSelectedTextField = t_Options;
            activeMenu.firstActive.SetActive(true);
        }
    }
    public void Credits()
    {
        if (p_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[3];
            lastSelectedTextField = t_Credits;
            activeMenu.firstActive.SetActive(true);
        }
        if (m_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[3];
            lastSelectedTextField = t_Credits;
            activeMenu.firstActive.SetActive(true);
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
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[4];
            lastSelectedTextField = t_StrafeOn;
            activeMenu.firstActive.SetActive(true);
        }
        if (m_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[4];
            lastSelectedTextField = t_StrafeOn;
            activeMenu.firstActive.SetActive(true);
        }
    }
    public void N_Stafing()
    {
        b_Strafing = false;
        if (p_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[4];
            lastSelectedTextField = t_StrafeOff;
            activeMenu.firstActive.SetActive(true);
        }
        if (m_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[4];
            lastSelectedTextField = t_StrafeOff;
            activeMenu.firstActive.SetActive(true);
        }
    }
    public void Latch()
    {
        b_SpecificLatch = false;
        if (p_Open)
        {
            activeMenu.firstActive.SetActive(false);
            activeMenu = menus[2];
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
