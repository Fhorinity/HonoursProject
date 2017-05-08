using UnityEngine;

public class Trigger11 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger10 trig;
    public AudioClip haptic11;
    public AudioClip haptic10;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger10>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic10;
                aud.source.PlayDelayed(0.5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic11;
            aud.source.PlayDelayed(0.5f);
            inTrigger = false;
        }
    }
}
