using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substrate : MonoBehaviour {

    public Genome CurrentGenome;

    public bool Active
    {
        get;
        set;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Active && Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            if (position.magnitude < 5)
            {
                GameObject cell = Instantiate(PrefabSupplier.CellPrefabReference);
                Cell cellData = cell.GetComponent<Cell>();
                cellData.genome = CurrentGenome;
                cellData.CellModeIndex = cellData.genome.InitialModeIndex;
                cell.transform.position = position;
                cell.transform.SetParent(transform.parent, false);
            }
        }
	}
}
