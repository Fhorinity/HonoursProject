using UnityEngine;

public class Trigger5 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger4 trig;
    public AudioClip haptic17;
    public AudioClip haptic16;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger4>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic16;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic17;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
