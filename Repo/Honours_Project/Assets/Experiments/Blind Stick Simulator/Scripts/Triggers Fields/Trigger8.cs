using UnityEngine;

public class Trigger8 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger7 trig;
    public AudioClip haptic14;
    public AudioClip haptic13;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger7>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic13;
                aud.source.PlayDelayed(0.5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic14;
            aud.source.PlayDelayed(0.5f);
            inTrigger = false;
        }
    }
}
