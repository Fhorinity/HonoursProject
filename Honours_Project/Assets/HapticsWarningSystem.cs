using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HapticsWarningSystem : MonoBehaviour
{
    public Text[] text;
    private bool b_TaskCompleted;
    private bool b_TaskSet;
    private int i_FailSafeCounter;
	
	// Update is called once per frame
	void Update ()
    {
	  //  foreach(Text text in HapticsWarningSystem)
        {
            if (b_TaskSet)
            {
                i_FailSafeCounter++;
                if (i_FailSafeCounter >= 20)
                {
                    //Display Visual Cue
                }
                if (i_FailSafeCounter >= 40)
                {
                    //Haptic Cue
                }
                if (i_FailSafeCounter > 60)
                {
                    i_FailSafeCounter = 60;
                    //Play constant haptic cue
                }
            }

            if (b_TaskCompleted)
            {
                b_TaskCompleted = false;
                b_TaskSet = false;
                //Stop haptic cue && visual cue
            }

         }	
	}
}
