using UnityEngine;

public class Trigger15 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger14 trig;
    public AudioClip haptic7;
    public AudioClip haptic6;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger14>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic6;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic7;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
