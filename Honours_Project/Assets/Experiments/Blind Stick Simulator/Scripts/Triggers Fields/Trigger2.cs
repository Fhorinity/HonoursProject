using UnityEngine;

public class Trigger2 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger1 trig;
    public AudioClip haptic19;
    public AudioClip haptic20;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger1>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic19;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic20;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
	}
}
