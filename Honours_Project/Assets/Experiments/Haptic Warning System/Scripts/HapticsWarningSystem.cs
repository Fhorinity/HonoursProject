using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HapticsWarningSystem : MonoBehaviour
{
    public bool b_TaskCompleted = false;
    public bool b_TaskSet = true;
    private float i_FailSafeCounter = 0f;
    private HapticManager aud;
    public AudioClip loop;
    public AudioClip pulse;
    public Text warning;
    public GameObject label;
    private VRControllerEvents controller;
	
    void Start()
    {
        aud = FindObjectOfType<HapticManager>();
    }

    IEnumerator Flashing()
    {
        while (true)
        {
            warning.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            warning.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        if (b_TaskSet)
        {
            i_FailSafeCounter += Time.deltaTime;
            print(i_FailSafeCounter);
            if (i_FailSafeCounter > 0f)
            {
                label.SetActive(true);
                warning.text = "Press Trigger";
                warning.color = Color.green;
            }
            if (i_FailSafeCounter > 10)
            {
                StartCoroutine("Flashing");
            }
            if (i_FailSafeCounter > 20)
            {
                aud.source.clip = pulse;
                aud.source.volume = 0.4f;
                aud.source.Play();
            }
            if (i_FailSafeCounter > 30)
            {
                aud.source.clip = loop;
                aud.source.loop = true;
                aud.source.Play();              
            }
            if (i_FailSafeCounter > 40)
            {
                i_FailSafeCounter = 40;
                aud.source.volume++;
                if (aud.source.volume >= 1)
                {
                    aud.source.volume = 1;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            b_TaskCompleted = true;
        }
        if (b_TaskCompleted)
        {
            aud.source.loop = false;
            aud.source.Stop();
            StopCoroutine("Flashing");
            warning.color = Color.green;
            label.SetActive(false);
            b_TaskCompleted = false;
            i_FailSafeCounter = 0f;
            b_TaskSet = false;
        }
    }
}
