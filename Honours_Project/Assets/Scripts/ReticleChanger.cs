using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ReticleChanger : MonoBehaviour
{
    private string spr_reticle = "Reticle";
    public Camera cameraFacing;
    private SpriteRenderer sprRend;
    private Sprite[] reticles;
    private int reticleType = 0;
    [HideInInspector]
    public bool inRange;
    void Start ()
    {
        sprRend = gameObject.GetComponent<SpriteRenderer>();
        reticles = Resources.LoadAll<Sprite>(spr_reticle);	
	}
	void Update ()
    {
        transform.LookAt(cameraFacing.transform.position);
        transform.position = cameraFacing.transform.position + 
            cameraFacing.transform.rotation * Vector3.forward * 2.0f;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        RaycastHit hit;
        float distance;
        if (Physics.Raycast(cameraFacing.transform.position,
            cameraFacing.transform.rotation * Vector3.forward, out hit))
            {
            distance = hit.distance;
        }
        if (inRange)
            reticleType = 1;
        else
            reticleType = 0;
        sprRend.sprite = reticles[reticleType];
	}
}
