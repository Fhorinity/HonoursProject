using UnityEngine;

public class Trigger14 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger13 trig;
    public AudioClip haptic8;
    public AudioClip haptic7;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger13>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic7;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic8;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
