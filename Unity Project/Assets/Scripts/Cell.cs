using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public float Mass = 3f;
    public Genome genome;
    public int cellMode;

    public bool Alive { get; set; }

    private Rigidbody2D physics;

    public float Radius { get { return Mass / 4f; } }

    private HashSet<Adhesin> adhesins = new HashSet<Adhesin>();

    // Use this for initialization
    void Start () {
        Alive = true;
        physics = GetComponent<Rigidbody2D>();
        physics.mass = Mass;
        transform.localScale = new Vector3(1, 1, 1) * Mass / 4f;
        SpriteRenderer graphics = GetComponent<SpriteRenderer>();
        graphics.color = genome[cellMode].Color;
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
            Destroy(gameObject);
            return;
        }
        physics.mass = Mass;
        transform.localScale = new Vector3(1, 1, 1) * Radius;
        if (Mass >= genome[cellMode].SplitMass)
        {
            if (GameObject.FindGameObjectsWithTag("Cell").Length > 1000)
            {
                return;
            }


            GameObject child1 = Instantiate(PrefabSupplier.CellPrefabReference, transform.position, transform.rotation, transform.parent);
            GameObject child2 = Instantiate(PrefabSupplier.CellPrefabReference, transform.position, transform.rotation, transform.parent);
            child1.transform.rotation *= Quaternion.Euler(0, 0, genome[cellMode].SplitAngle);
            child2.transform.rotation *= Quaternion.Euler(0, 0, genome[cellMode].SplitAngle);
            Rigidbody2D child1physics = child1.GetComponent<Rigidbody2D>();
            Rigidbody2D child2physics = child2.GetComponent<Rigidbody2D>();
            Cell child1Data = child1.GetComponent<Cell>();
            Cell child2Data = child2.GetComponent<Cell>();
            child1Data.Mass = Mass / 2f;
            child2Data.Mass = Mass / 2f;
            child1Data.genome = genome.Clone();
            child2Data.genome = genome.Clone();
            child1Data.cellMode = genome[cellMode].Child1ModeIndex;
            child2Data.cellMode = genome[cellMode].Child2ModeIndex;

            Vector2 splitVelocity = new Vector2(-1, 0).Rotate(transform.eulerAngles.z + genome[cellMode].SplitAngle);

            child1physics.velocity = physics.velocity + splitVelocity;
            child2physics.velocity = physics.velocity - splitVelocity;

            //Component[] child1Components = child1.GetComponents<Component>();
            //Component[] child2Components = child2.GetComponents<Component>();

            if (genome[cellMode].Child1KeepAdhesin)
            {
                foreach (Adhesin ad in adhesins)
                {
                    Cell otherCell = (ad.Cell1 != this) ? ad.Cell1 : ad.Cell2;
                    bool alreadyexists = false;
                    foreach (Adhesin child1Adhesin in child1Data.adhesins)
                    {
                        if (child1Adhesin.Cell1 == otherCell || child1Adhesin.Cell2 == otherCell)
                        {
                            alreadyexists = true;
                            break;
                        }
                    }
                    if (!alreadyexists)
                    {
                        GameObject adhesin = Instantiate(PrefabSupplier.AdhesinPrefabReference);
                        adhesin.transform.SetParent(gameObject.transform.parent);
                        Adhesin adhesinData = adhesin.GetComponent<Adhesin>();
                        adhesinData.Cell1 = child1Data;
                        adhesinData.Cell2 = otherCell;
                        child1Data.AddAdhesin(adhesinData);
                        otherCell.AddAdhesin(adhesinData);
                    }
                }
            }

            if (genome[cellMode].Child2KeepAdhesin)
            {
                foreach (Adhesin ad in adhesins)
                {
                    Cell otherCell = (ad.Cell1 != this) ? ad.Cell1 : ad.Cell2;
                    bool alreadyexists = false;
                    foreach (Adhesin child2Adhesin in child2Data.adhesins)
                    {
                        if (child2Adhesin.Cell1 == otherCell || child2Adhesin.Cell2 == otherCell)
                        {
                            alreadyexists = true;
                            break;
                        }
                    }
                    if (!alreadyexists)
                    {
                        GameObject adhesin = Instantiate(PrefabSupplier.AdhesinPrefabReference);
                        adhesin.transform.SetParent(gameObject.transform.parent);
                        Adhesin adhesinData = adhesin.GetComponent<Adhesin>();
                        adhesinData.Cell1 = child2Data;
                        adhesinData.Cell2 = (ad.Cell1 != this) ? ad.Cell1 : ad.Cell2;
                        child2Data.AddAdhesin(adhesinData);
                        otherCell.AddAdhesin(adhesinData);
                    }
                }
            }

            if (genome[cellMode].MakeAdhesin)
            {
                GameObject adhesin = Instantiate(PrefabSupplier.AdhesinPrefabReference);
                adhesin.transform.SetParent(gameObject.transform.parent);
                Adhesin adhesinData = adhesin.GetComponent<Adhesin>();
                adhesinData.Cell1 = child1Data;
                adhesinData.Cell2 = child2Data;
                child1Data.AddAdhesin(adhesinData);
                child2Data.AddAdhesin(adhesinData);
            }

            Alive = false;
            Destroy(gameObject);
        }
    }

    internal void RemoveAdhesin(Adhesin adhesin)
    {
        adhesins.Remove(adhesin);
    }

    public void AddAdhesin(Adhesin adhesin)
    {
        adhesins.Add(adhesin);
    }
}

public static class Extensions
{
    public static Vector2 Rotate(this Vector2 original, float degrees)
    {
        return Quaternion.Euler(new Vector3(0, 0, degrees)) * original;
    }
}
