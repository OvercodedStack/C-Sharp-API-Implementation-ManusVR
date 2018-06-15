using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_me_coninously_dynamic : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Rigidbody[] rig;
        rig = this.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rig.Length; i++)
        {
            rig[i].collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }
}
