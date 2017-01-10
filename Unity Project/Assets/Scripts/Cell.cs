using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public float Mass = 3f;
    public CellMode mode;

    public bool Alive { get; set; }

    private Rigidbody2D physics;

    public float Radius { get { return Mass / 4f; } }

    private HashSet<Adhesin> adhesins = new HashSet<Adhesin>();

    // Use this for initialization
    void Start () {
        if (mode == null)
        { // Diagnostic, for initial cell, placed from Unity editor
            mode = new CellMode();
            mode.Type = CellType.Photocyte;
            mode.SplitMass = 2.54f;
            mode.Child1 = mode;
            mode.Child2 = mode;
            mode.Color = new Color(0f, 1f, 0f);
            mode.MakeAdhesin = true;
            mode.Child2KeepAdhesin = true;

            CellMode mode1 = new CellMode();
            mode1.Type = CellType.Photocyte;
            mode1.SplitMass = float.PositiveInfinity;
            mode1.Child1 = mode1;
            mode1.Child2 = mode1;
            mode1.Color = new Color(1f, 0f, 0f);

            CellMode mode2 = new CellMode();
            mode2.Type = CellType.Photocyte;
            mode2.SplitMass = 2.6f;
            mode2.Child1 = mode2;
            mode2.Child2 = mode2;
            mode2.Color = new Color(0f, 0f, 1f);
            mode2.MakeAdhesin = true;
            mode2.Child2KeepAdhesin = true;

            mode.Child2 = mode1;
            mode1.Child2 = mode2;
            mode2.Child2 = mode;
        }
        Alive = true;
        physics = GetComponent<Rigidbody2D>();
        physics.mass = Mass;
        transform.localScale = new Vector3(1, 1, 1) * Mass / 4f;
        SpriteRenderer graphics = GetComponent<SpriteRenderer>();
        graphics.color = mode.Color;
    }

    void OnDestroy()
    {
        BreakAllAdhesin();
    }

    private void BreakAllAdhesin()
    {
        foreach (Adhesin ad in adhesins)
        {
            ad.Break();
        }
        adhesins.Clear();
    }

    // Update is called once per frame
    void Update () {
        Mass -= 0.7f * Time.deltaTime;

        if (Mass < 5)
        {
            // linear + radius
            float linear = 0.08f * transform.position.y * Time.deltaTime;
            float radial = 0.15f * transform.position.magnitude * Time.deltaTime;
            Mass += linear + radial;
        }
        if (Mass < 1f)
        {
            Alive = false;
            BreakAllAdhesin();
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


            GameObject child1 = Instantiate(PrefabSupplier.CellPrefabReference, transform.position, transform.rotation);
            GameObject child2 = Instantiate(PrefabSupplier.CellPrefabReference, transform.position, transform.rotation);
            child1.name = gameObject.name;
            child2.name = gameObject.name;
            child1.transform.SetParent(transform.parent, false);
            child2.transform.SetParent(transform.parent, false);
            Rigidbody2D child1physics = child1.GetComponent<Rigidbody2D>();
            Rigidbody2D child2physics = child2.GetComponent<Rigidbody2D>();
            Cell child1Data = child1.GetComponent<Cell>();
            Cell child2Data = child2.GetComponent<Cell>();
            child1Data.Mass = Mass / 2f;
            child2Data.Mass = Mass / 2f;
            child1Data.mode = mode.Child1;
            child2Data.mode = mode.Child2;

            Vector2 splitVelocity = new Vector2(-1, 0).Rotate(transform.eulerAngles.z);

            child1physics.velocity = physics.velocity + splitVelocity;
            child2physics.velocity = physics.velocity - splitVelocity;

            //Component[] child1Components = child1.GetComponents<Component>();
            //Component[] child2Components = child2.GetComponents<Component>();

            if (mode.Child1KeepAdhesin)
            {
                foreach (Adhesin ad in adhesins)
                {
                    GameObject adhesin = Instantiate(PrefabSupplier.AdhesinPrefabReference);
                    adhesin.transform.SetParent(gameObject.transform.parent);
                    Adhesin adhesinData = adhesin.GetComponent<Adhesin>();
                    adhesinData.Cell1 = child1Data;
                    adhesinData.Cell2 = (ad.Cell1 != this) ? ad.Cell1 : ad.Cell2;
                }
            }

            if (mode.Child2KeepAdhesin)
            {
                foreach (Adhesin ad in adhesins)
                {
                    GameObject adhesin = Instantiate(PrefabSupplier.AdhesinPrefabReference);
                    adhesin.transform.SetParent(gameObject.transform.parent);
                    Adhesin adhesinData = adhesin.GetComponent<Adhesin>();
                    adhesinData.Cell1 = child2Data;
                    adhesinData.Cell2 = (ad.Cell1 != this) ? ad.Cell1 : ad.Cell2;
                }
            }
            BreakAllAdhesin();

            if (mode.MakeAdhesin)
            {
                GameObject adhesin = Instantiate(PrefabSupplier.AdhesinPrefabReference);
                adhesin.transform.SetParent(gameObject.transform.parent);
                Adhesin adhesinData = adhesin.GetComponent<Adhesin>();
                adhesinData.Cell1 = child1Data;
                adhesinData.Cell2 = child2Data;
            }

            Alive = false;
            Destroy(gameObject);
        }
    }

    public void AddAdhesin(Adhesin that)
    {
        adhesins.Add(that);
    }
}

public static class Extensions
{
    public static Vector2 Rotate(this Vector2 original, float degrees)
    {
        return Quaternion.Euler(new Vector3(0, 0, degrees)) * original;
    }
}
