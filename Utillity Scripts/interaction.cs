﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour {
    private FixedJoint attach_joint = null;
    private Rigidbody current_rigidBody = null;
    private List<Rigidbody> rigidbody_list = new List<Rigidbody>();

	// Use this for initialization
	void Awake () {
        attach_joint = GetComponent<FixedJoint>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        rigidbody_list.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        rigidbody_list.Remove(other.gameObject.GetComponent<Rigidbody>());

    }

    public void Pickup()
    {
        current_rigidBody = Get_Nearest_RigidBody();

        if (!current_rigidBody)
        {
            return;
        }
        //Copy position of controller
        current_rigidBody.transform.position = transform.position;

        //Set to the Controller
        attach_joint.connectedBody = current_rigidBody;

    }

    public void Drop(SteamVR_Controller.Device device)
    {
        if (!current_rigidBody)
        {
            return;
        }
        current_rigidBody.velocity = device.velocity;
        current_rigidBody.angularVelocity = device.angularVelocity;

        attach_joint.connectedBody = null;
        current_rigidBody = null;


    }


    public void Drop(Rigidbody device)
    {
        if (!current_rigidBody)
        {
            return;
        }
        current_rigidBody.velocity = device.velocity;
        current_rigidBody.angularVelocity = device.angularVelocity;

        attach_joint.connectedBody = null;
        current_rigidBody = null;


    }

    private Rigidbody Get_Nearest_RigidBody()
    {
        Rigidbody nearest_rigid = null;

        float min_dist = float.MaxValue;
        float dist = 0.0f;

        foreach(Rigidbody contact_body in rigidbody_list)
        {
            dist = (contact_body.gameObject.transform.position - transform.position).sqrMagnitude;
            if (dist < min_dist)
            {
                min_dist = dist;
                nearest_rigid = contact_body;
            }
        }
        return nearest_rigid;
    }
 
}
