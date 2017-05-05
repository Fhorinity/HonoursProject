using UnityEngine;

public class Trigger18 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger17 trig;
    public AudioClip haptic4;
    public AudioClip haptic3;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger17>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic3;
                aud.source.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic4;
            aud.source.Play();
            inTrigger = false;
        }
    }
}
