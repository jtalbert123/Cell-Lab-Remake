using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGizmos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, transform.localScale);
        Gizmos.DrawRay(transform.position, transform.rotation * Vector3.right);
    }
}
