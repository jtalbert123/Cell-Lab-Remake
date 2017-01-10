using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBoxRingCollider : MonoBehaviour {

    public float InnerRadius = 0.5f;
    public int NumBoxes = 60;
    public float Depth = 0.05f;

    public PolygonCollider2D target;

    // Use this for initialization
    void Start () {
        int index = target.pathCount;
        target.pathCount += NumBoxes;

        for (int i = 0; i < NumBoxes; i++)
        {
            float lowAngle = 2 * Mathf.PI * i / NumBoxes;
            float highAngle = 2 * Mathf.PI * (i + 1) / NumBoxes;
            Vector2 lowDirection = new Vector2(Mathf.Cos(lowAngle), Mathf.Sin(lowAngle));
            Vector2 highDirection = new Vector2(Mathf.Cos(highAngle), Mathf.Sin(highAngle));
            Vector2 lowInside = lowDirection * InnerRadius;
            Vector2 lowOutside = lowDirection * (InnerRadius + Depth);
            Vector2 highInside = highDirection * InnerRadius;
            Vector2 highOutside = highDirection * (InnerRadius + Depth);

            target.SetPath(index + i, new Vector2[] { highOutside, highInside, lowInside, lowOutside });
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
