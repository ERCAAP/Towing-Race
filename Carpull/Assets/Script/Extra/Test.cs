using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
public class Test : MonoBehaviour
{
	public ObiRope rope;
	public Camera cam;
	public Vector3 firspos;
	public Vector3 secpos;
	public bool cut;
	void FindRobe()
    {
		// first particle in the rope is the first particle of the first element:
		int firstParticle = rope.elements[0].particle1;

		// last particle in the rope is the second particle of the last element:
		int lastParticle = rope.elements[rope.elements.Count - 1].particle2;

		// now get their positions (expressed in solver space):
		firspos = rope.solver.positions[firstParticle];
		secpos = rope.solver.positions[lastParticle];
	}
    private void FixedUpdate()
    {
		FindRobe();
		ScreenSpaceCut(firspos,secpos);
    }
    public void ScreenSpaceCut(Vector2 lineStart, Vector2 lineEnd)
	{
		// keep track of whether the rope was cut or not.
		cut = false;

		// iterate over all elements and test them for intersection with the line:
		for (int i = 0; i < rope.elements.Count; ++i)
		{
			// project the both ends of the element to screen space.
			Vector3 screenPos1 = cam.WorldToScreenPoint(rope.solver.positions[rope.elements[i].particle1]);
			Vector3 screenPos2 = cam.WorldToScreenPoint(rope.solver.positions[rope.elements[i].particle2]);

			// test if there's an intersection:
			if (SegmentSegmentIntersection(screenPos1, screenPos2, lineStart, lineEnd, out float r, out float s))
			{
				cut = true;
				rope.Tear(rope.elements[i]);
			}
		}

		// If the rope was cut at any point, rebuild constraints:
		if (cut) rope.RebuildConstraintsFromElements();
	}

	/**
	* line segment 1 is AB = A+r(B-A)
	* line segment 2 is CD = C+s(D-C)
	* if they intesect, then A+r(B-A) = C+s(D-C), solving for r and s gives the formula below.
	* If both r and s are in the 0,1 range, it meant the segments intersect.
*/
	private bool SegmentSegmentIntersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D, out float r, out float s)
	{
		float denom = (B.x - A.x) * (D.y - C.y) - (B.y - A.y) * (D.x - C.x);
		float rNum = (A.y - C.y) * (D.x - C.x) - (A.x - C.x) * (D.y - C.y);
		float sNum = (A.y - C.y) * (B.x - A.x) - (A.x - C.x) * (B.y - A.y);

		if (Mathf.Approximately(rNum, 0) || Mathf.Approximately(denom, 0))
		{ r = -1; s = -1; return false; }

		r = rNum / denom;
		s = sNum / denom;

		return (r >= 0 && r <= 1 && s >= 0 && s <= 1);
	}
}
