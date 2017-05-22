using UnityEngine;

public class Trigger10 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger9 trig;
    public AudioClip haptic12;
    public AudioClip haptic11;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger9>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic11;
                aud.source.PlayDelayed(0.5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic12;
            aud.source.PlayDelayed(0.5f);
            inTrigger = false;
        }
    }
}
