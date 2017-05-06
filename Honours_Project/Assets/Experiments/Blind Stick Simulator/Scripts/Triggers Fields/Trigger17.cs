using UnityEngine;

public class Trigger17 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger16 trig;
    public AudioClip haptic5;
    public AudioClip haptic4;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger16>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic4;
                aud.source.PlayDelayed(1.25f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic5;
            aud.source.PlayDelayed(1.25f);
            inTrigger = false;
        }
    }
}
