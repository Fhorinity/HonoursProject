using UnityEngine;

public class Trigger4 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;
    private Trigger3 trig;
    public AudioClip haptic18;
    public AudioClip haptic17;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();
        trig = FindObjectOfType<Trigger3>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            inTrigger = true;
            if (trig.inTrigger)
            {
                aud.source.clip = haptic17;
                aud.source.PlayDelayed(0.5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic18;
            aud.source.PlayDelayed(0.5f);
            inTrigger = false;
        }
    }
}
