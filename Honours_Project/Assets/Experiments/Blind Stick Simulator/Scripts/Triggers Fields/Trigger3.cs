using UnityEngine;

public class Trigger3 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger2 trig;
    public AudioClip haptic19;
    public AudioClip haptic18;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic18;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic19;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
