using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microscope : MonoBehaviour {

    public bool active;

    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            substrate.Active = value;
        }
    }

    public Genome CurrentGenome { set { substrate.CurrentGenome = value; } }

    private Substrate substrate;

	// Use this for initialization
	void Start () {
        substrate = GetComponentInChildren<Substrate>();
	}
}
