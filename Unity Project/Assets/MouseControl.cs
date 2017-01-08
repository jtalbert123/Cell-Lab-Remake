using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {
    public float forceFactor = 1;
    public float maxSpeed = 1;
    Rigidbody2D body;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (body.velocity.magnitude > maxSpeed)
        {
            if ((x > 0 && body.velocity.x > 0) ||
                (x < 0 && body.velocity.x < 0))
            { // Same Direction in X
                x = 0;
            }
            if ((y > 0 && body.velocity.y > 0) ||
                (y < 0 && body.velocity.y < 0))
            { // Same Direction in Y
                y = 0;
            }
        }
        if (x != 0 || y != 0)
        {
            body.AddForce(new Vector2(x, y) * forceFactor);
        }
    }
}
