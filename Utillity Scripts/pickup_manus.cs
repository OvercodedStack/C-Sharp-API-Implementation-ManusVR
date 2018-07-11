using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using manus_interface;

/// <summary>
///  OBSOLETE DO NOT USE.
/// </summary>
public class pickup_manus : MonoBehaviour {
    Collider collider_this;
    Collision collisions_on_this;

	// Use this for initialization
	void Start () {
        collider_this = this.GetComponent<Collider>();
        collisions_on_this = this.GetComponent<Collision>();
    }
	
	// Update is called once per frame
	void Update () {
        ContactPoint[] points = collisions_on_this.contacts;
        int contactPts = points.Length;

        if (contactPts > 2)
        {

        }


	}
}
