using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        print(source.clip);
    }
}
