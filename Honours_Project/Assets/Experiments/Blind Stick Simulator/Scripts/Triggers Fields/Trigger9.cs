using UnityEngine;

public class Trigger9 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger8 trig;
    public AudioClip haptic13;
    public AudioClip haptic12;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger8>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic12;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic13;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
