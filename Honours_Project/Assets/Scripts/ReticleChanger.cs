using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ReticleChanger : MonoBehaviour
{
    private string spr_reticle = "Reticle";
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
	    if (inRange)
            reticleType = 1;
        else
            reticleType = 0;
        sprRend.sprite = reticles[reticleType];
	}
}
