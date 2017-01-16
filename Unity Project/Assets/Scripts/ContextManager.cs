using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextManager : MonoBehaviour {

    Microscope Microscope;

    GenomeEditor GenomeEditor;

    // Use this for initialization
    void Start () {
        Microscope = GameObject.FindGameObjectWithTag("Microscope").GetComponent<Microscope>();
        GenomeEditor = GameObject.FindGameObjectWithTag("Genome Editor").GetComponent<GenomeEditor>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G) && !GenomeEditor.Active)
        {
                Microscope.Active = false;
                GenomeEditor.Active = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && !Microscope.Active)
        {
            Microscope.Active = true;
            GenomeEditor.Active = false;
            Microscope.CurrentGenome = GenomeEditor.CurrentGenome;
        }
	}
}
