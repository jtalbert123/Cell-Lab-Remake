using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substrate : MonoBehaviour {

    public Genome CurrentGenome;
    public GameObject PrefabReference;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            if (position.magnitude < 5)
            {
                GameObject cell = Instantiate(PrefabReference);
                Cell cellData = cell.GetComponent<Cell>();
                cellData.cellMode = cellData.genome.InitialModeIndex;
                cell.transform.position = position;
                cell.transform.SetParent(transform, false);
            }
        }
	}
}
