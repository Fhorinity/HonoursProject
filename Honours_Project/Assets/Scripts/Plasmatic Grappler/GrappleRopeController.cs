using UnityEngine;

[ExecuteInEditMode()]
public class GrappleRopeController : MonoBehaviour
{
    public Transform[] r_Points;
    public LineRenderer r_LineRenderer;

	void Start ()
    {
        this.r_LineRenderer.numPositions = this.r_Points.Length;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    for (int i = 0; i < this.r_Points.Length; i++)
        {
            this.r_LineRenderer.SetPosition(i, this.r_Points[i].position);
        }	
	}
}
