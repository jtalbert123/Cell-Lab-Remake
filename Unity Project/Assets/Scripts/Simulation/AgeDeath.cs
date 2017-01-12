using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeDeath : MonoBehaviour {

    public float MaxAge = 1;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, MaxAge);
    }
}
