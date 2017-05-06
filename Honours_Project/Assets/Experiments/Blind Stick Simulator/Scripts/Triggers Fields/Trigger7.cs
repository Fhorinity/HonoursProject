using UnityEngine;

public class Trigger7 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger6 trig;
    public AudioClip haptic15;
    public AudioClip haptic14;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger6>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic14;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic15;
                aud.source.PlayDelayed(1.25f);
                inTrigger = false;
        }
    }
}
