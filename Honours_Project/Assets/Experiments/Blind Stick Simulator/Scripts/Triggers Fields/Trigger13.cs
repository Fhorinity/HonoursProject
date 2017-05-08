using UnityEngine;

public class Trigger13 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger12 trig;
    public AudioClip haptic9;
    public AudioClip haptic8;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger12>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic8;
                aud.source.PlayDelayed(0.5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic9;
            aud.source.PlayDelayed(0.5f);
            inTrigger = false;
        }
    }
}