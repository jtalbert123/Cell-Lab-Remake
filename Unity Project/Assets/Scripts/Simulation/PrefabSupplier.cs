using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSupplier : MonoBehaviour {

    public GameObject CellPrefab;
    public static GameObject CellPrefabReference { get; set; }

    public GameObject AdhesinPrefab;
    public static GameObject AdhesinPrefabReference { get; set; }

    // Use this for initialization
    void Start () {
        CellPrefabReference = CellPrefab;
        AdhesinPrefabReference = AdhesinPrefab;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
