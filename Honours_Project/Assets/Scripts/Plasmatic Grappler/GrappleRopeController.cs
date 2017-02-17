using UnityEngine;

[ExecuteInEditMode()]
public class GrappleRopeController : MonoBehaviour
{
    public Transform[] r_Points;
    public LineRenderer r_LineRenderer;

	void Start ()
    {
        r_LineRenderer.numPositions = r_Points.Length;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    for (int i = 0; i < r_Points.Length; i++)
        {
            r_LineRenderer.SetPosition(i, r_Points[i].position);
        }	
	}
}
