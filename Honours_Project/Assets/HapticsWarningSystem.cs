using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HapticsWarningSystem : MonoBehaviour
{
    //public Text[] text;
    public bool b_TaskCompleted = false;
    public bool b_TaskSet = true;
    public float i_FailSafeCounter = 0f;
    public AudioSource hapticLoop;
    public AudioSource hapticPulse;
	
   

	// Update is called once per frame
	void Update ()
    {
	  //  foreach(Text text in HapticsWarningSystem)
        {
            if (b_TaskSet)
            {
                i_FailSafeCounter += Time.deltaTime;
                if (i_FailSafeCounter > 10)
                {
                    //Display Visual Cue
                }
                if (i_FailSafeCounter > 20)
                {
                    hapticPulse.Play();
                    hapticPulse.Stop();
                }
                if (i_FailSafeCounter > 30)
                {
                    
                    hapticLoop.Play();
                    hapticLoop.volume = 0.2f;
                    hapticLoop.loop = true;
                    //Play constant haptic cue
                }
                if (i_FailSafeCounter > 40)
                {
                    hapticLoop.volume = 1f;
                }
            }

            if (b_TaskCompleted)
            {
                hapticLoop.loop = false;
                hapticLoop.Stop();
                b_TaskCompleted = false;
                b_TaskSet = false;
                //Stop haptic cue && visual cue
            }

         }	
	}


}
