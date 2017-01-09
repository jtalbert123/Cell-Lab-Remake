using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    public GameObject PrefabReference;
    public float Mass = 3f;
    public CellMode mode;

    private Rigidbody2D physics;
    private CircleCollider2D SubstrateCollider;
    private CircleCollider2D thisCollider;

    public float Radius { get { return Mass / 4f; } }

    // Use this for initialization
    void Start () {
        physics = GetComponent<Rigidbody2D>();
        if (mode == null)
        { // Diagnostic, for initial cell, placed from Unity editor
            mode = new CellMode();
            mode.Type = CellType.Photocyte;
            mode.SplitMass = 2.54f;
            mode.Child1 = mode;
            mode.Child2 = mode;
        }
        physics.mass = Mass;
        transform.localScale = new Vector3(1, 1, 1) * Mass / 4f;
    }
    
    // Update is called once per frame
    void Update () {
        Mass -= 0.05f;
        if (transform.position.y > 0)
        {
            // linear + raduis
            float linear = 0.006f * transform.position.y;
            float radial = 0.008f * transform.position.magnitude;
            Mass += linear + radial;
        }
        if (Mass < 1f)
        {
            Destroy(gameObject);
            return;
        }
        physics.mass = Mass;
        transform.localScale = new Vector3(1, 1, 1) * Radius;
        if (Mass >= mode.SplitMass)
        {
            if (GameObject.FindGameObjectsWithTag("Cell").Length > 1000)
            {
                return;
            }

            GameObject child1 = Instantiate(PrefabReference, transform.position, transform.rotation);
            GameObject child2 = Instantiate(PrefabReference, transform.position, transform.rotation);
            child1.name = gameObject.name;
            child2.name = gameObject.name;
            Rigidbody2D child1physics = child1.GetComponent<Rigidbody2D>();
            Rigidbody2D child2physics = child2.GetComponent<Rigidbody2D>();
            Cell child1Data = child1.GetComponent<Cell>();
            Cell child2Data = child2.GetComponent<Cell>();
            child1Data.Mass = Mass / 2f;
            child2Data.Mass = Mass / 2f;

            Vector2 splitVelocity = new Vector2(-1, 0).Rotate(transform.eulerAngles.z);

            child1physics.velocity = physics.velocity + splitVelocity;
            child2physics.velocity = physics.velocity - splitVelocity;

            Destroy(gameObject);
        }
    }
}

public static class Extensions
{
    public static Vector2 Rotate(this Vector2 original, float degrees)
    {
        return Quaternion.Euler(new Vector3(0, 0, degrees)) * original;
    }
}
