using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public bool inTrigger = false;
    private TriggerManager aud;  
    public AudioClip haptic20;

    void Start()
    {
        aud = FindObjectOfType<TriggerManager>();      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {
            aud.source.clip = haptic20;
            aud.source.PlayDelayed(1.25f);
            inTrigger = true;           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Blind Stick")
        {     
            aud.source.Stop();
            inTrigger = false;
        }
	}
}
