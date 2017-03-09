using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    public GameObject firstActive;
    private EventSystem events;

    private void OnEnable()
    { 
        EventSystem.current.SetSelectedGameObject(firstActive);
    }
}