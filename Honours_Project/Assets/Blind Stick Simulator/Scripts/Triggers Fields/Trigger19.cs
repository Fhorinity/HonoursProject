using UnityEngine;

public class Trigger19 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger18 trig;
    public AudioClip haptic3;
    public AudioClip haptic2;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger18>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic2;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic3;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
