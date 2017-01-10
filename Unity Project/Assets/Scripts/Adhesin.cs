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

    // Use this for initialization
    void Start () {
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

        Cell1.AddAdhesin(this);
        Cell2.AddAdhesin(this);
    }

    private bool destroyed = false;
    public void Break()
    {
        if (!destroyed)
        {
            Destroy(spring);
            Destroy(gameObject);
            destroyed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spring.anchor = spring.anchor.normalized * Cell1.Radius * 3 / 8f;
        spring.connectedAnchor = spring.connectedAnchor.normalized * Cell2.Radius * 3 / 8f;

        transform.position = (Cell1.transform.position + Cell2.transform.position) / 2;
        Vector2 displacement = Cell2.transform.position - Cell1.transform.position;
        float angle = -Mathf.Atan2(displacement.x, displacement.y) / Mathf.PI * 180f + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.localScale = Vector3.one * (Cell1.Radius + Cell2.Radius);

        float transferAmount = 0.3f * Time.deltaTime;
        if (Cell1.Mass > Cell2.Mass + transferAmount)
        {
            Cell1.Mass -= transferAmount;
            Cell2.Mass += transferAmount;
        }
        else if (Cell2.Mass > Cell1.Mass + transferAmount)
        {
            Cell2.Mass -= transferAmount;
            Cell1.Mass += transferAmount;
        }
    }
}
