using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adhesin : MonoBehaviour {

    public Cell Cell1;
    public Cell Cell2;

    private Rigidbody2D Cell1Physics;
    private Rigidbody2D Cell2Physics;

    public SpringJoint2D spring;

    private bool HasRetracted = false;

    // Use this for initialization
    void Start () {
        if (Cell1 == null || Cell2 == null)
        {
            Destroy(gameObject);
            return;
        }
        Cell1Physics = Cell1.GetComponent<Rigidbody2D>();
        Cell2Physics = Cell2.GetComponent<Rigidbody2D>();

        transform.localScale = Vector3.one * (Cell1.Radius + Cell2.Radius);

        transform.position = (Cell1.transform.position + Cell2.transform.position) / 2;
        Vector2 displacement = Cell2.transform.position - Cell1.transform.position;
        if (displacement == Vector2.zero)
        {
            displacement = Cell2Physics.velocity - Cell1Physics.velocity;
        }
        float angle = -Mathf.Atan2(displacement.x, displacement.y)/Mathf.PI * 180f + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        spring = Cell1.gameObject.AddComponent<SpringJoint2D>();
        spring.connectedBody = Cell2Physics;
        Vector2 Cell1_WorldAngledAnchorPoint = displacement.normalized * 0.4f;
        Vector2 Cell1_AnchorPoint = Cell1_WorldAngledAnchorPoint.Rotate(-Cell1.transform.eulerAngles.z);
        spring.anchor = Cell1_AnchorPoint;

        Vector2 Cell2_WorldAngledAnchorPoint = -displacement.normalized * 0.4f;
        Vector2 Cell2_AnchorPoint = Cell2_WorldAngledAnchorPoint.Rotate(-Cell2.transform.eulerAngles.z);
        spring.connectedAnchor = Cell2_AnchorPoint;

        spring.enableCollision = true;
        spring.distance = 0;
        spring.frequency = 1;
        spring.dampingRatio = 1;
        spring.autoConfigureDistance = false;
        spring.autoConfigureConnectedAnchor = false;

        HasRetracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cell1 == null || Cell2 == null)
        {
            Destroy(spring);
            Destroy(gameObject);
            return;
        }
        spring.anchor = spring.anchor.normalized * Cell1.Radius * 3 / 8f;
        spring.connectedAnchor = spring.connectedAnchor.normalized * Cell2.Radius * 3 / 8f;

        transform.position = (Cell1.transform.position + Cell2.transform.position) / 2;
        Vector2 displacement = Cell2.transform.position - Cell1.transform.position;
        float angle = -Mathf.Atan2(displacement.x, displacement.y) / Mathf.PI * 180f + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.localScale = Vector3.one * (Cell1.Radius + Cell2.Radius);

        if (displacement.magnitude < (Cell1.Radius + Cell2.Radius) * 0.6f)
        {
            HasRetracted = true;
        }
        else if (HasRetracted)
        { // too far away (0.2f is the ideal position), the cells got pushed apart

            Cell1.RemoveAdhesin(this);
            Cell2.RemoveAdhesin(this);
            Destroy(spring);
            Destroy(gameObject);
            return;
        }
        else
        {
            Vector2 dv = Cell1Physics.velocity - Cell2Physics.velocity;
            float displacementAngle = Mathf.Atan2(displacement.y, displacement.x);
            float velocityAngle = Mathf.Atan2(dv.y, dv.x);
            if (Mathf.Abs(velocityAngle - displacementAngle) > Mathf.PI/2f)
            {
                Cell1.RemoveAdhesin(this);
                Cell2.RemoveAdhesin(this);
                Destroy(spring);
                Destroy(gameObject);
                return;
            }
        }

        float transferAmount = 0.3f * Time.deltaTime;
        if (Cell1.Mass > Cell2.Mass)
        {
            Cell1.Mass -= transferAmount;
            Cell2.Mass += transferAmount;
        }
        else if (Cell2.Mass > Cell1.Mass)
        {
            Cell2.Mass -= transferAmount;
            Cell1.Mass += transferAmount;
        }
    }
}
