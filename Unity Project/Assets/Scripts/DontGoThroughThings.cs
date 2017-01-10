using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGoThroughThings : MonoBehaviour {

    public LayerMask layerMask; //make sure we aren't in this layer 
    public float skinWidth = 0.1f; //probably doesn't need to be changed 
    private float minimumExtent; 
    private float partialExtent; 
    private float sqrMinimumExtent; 
    private Vector2 previousPosition; 
    private Rigidbody2D myRigidbody; 

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        previousPosition = myRigidbody.position;
        minimumExtent = Mathf.Min(Mathf.Min(GetComponent<Collider2D>().bounds.extents.x, GetComponent<Collider2D>().bounds.extents.y), GetComponent<Collider2D>().bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //have we moved more than our minimum extent? 
        Vector2 movementThisStep = myRigidbody.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;
        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit2D hitInfo;
            //check for obstructions we might have missed 
            hitInfo = Physics2D.Raycast(previousPosition, movementThisStep, movementMagnitude, layerMask.value);
            if (hitInfo)
                myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
        }
        previousPosition = myRigidbody.position;
    }
}