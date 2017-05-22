using UnityEngine;

public class Trigger16 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger15 trig;
    public AudioClip haptic6;
    public AudioClip haptic5;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger15>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic5;
                aud.source.PlayDelayed(0.5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic6;
            aud.source.PlayDelayed(0.5f);
            inTrigger = false;
        }
    }
}
