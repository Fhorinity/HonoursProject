using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounding : MonoBehaviour
{ 
    public Collider coll;
    public bool isGrounding;
   public bool Grounded()
    {
        return Physics.Raycast(coll.bounds.center, Vector3.down, coll.bounds.extents.y + 0.1f);
    }
    void Update()
    {
        if (Grounded())
            isGrounding = true;
        else
            isGrounding = false;
    }
}
