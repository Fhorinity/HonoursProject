using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour
{
    public Transform _player;
    public Transform _camera;
    public float throwForce = 10;
    public bool hasPlayer = false; //Checks if player isi n range to pick up object
    public bool beingCarried = false;
    public AudioClip[] soundToPlay;
    private AudioSource audio;
    private bool touched = false;
   // public Camera cam;
    public float distance = 10f;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit;
      //  Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, .5f));

        float dist = Vector3.Distance(gameObject.transform.position, _player.position);
        if (dist <= 2.5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }

      //  if (Physics.Raycast(ray, out hit, distance))
        //{

        //}

        if (hasPlayer && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = _camera;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(_camera.forward * throwForce);
                RandomAudio();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }
    void RandomAudio()
    {
        if (audio.isPlaying)
        {
            return;
        }
        audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        audio.Play();
    }

    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }

    
}
