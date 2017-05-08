using UnityEngine;

public class Trigger12 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger11 trig;
    public AudioClip haptic10;
    public AudioClip haptic9;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger11>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic9;
                aud.source.PlayDelayed(0.5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic10;
            aud.source.PlayDelayed(0.5f);
            inTrigger = false;
        }
    }
}
