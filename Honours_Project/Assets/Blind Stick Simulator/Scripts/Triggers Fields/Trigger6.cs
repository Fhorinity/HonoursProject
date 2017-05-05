using UnityEngine;

public class Trigger6 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger5 trig;
    public AudioClip haptic16;
    public AudioClip haptic15;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger5>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic15;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic16;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
