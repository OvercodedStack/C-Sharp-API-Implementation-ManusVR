﻿///////////////////////////////////////////////////////////////////////////////
//
//  Original System: hand_pickup.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        standalone
//  Revision:        1.0 - 7/12/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Script for moving objects using another gameobject.
//
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using manus_interface;

public class hand_pickup : MonoBehaviour {
    //Locate interpreter
    Manus_interpreter manus_inpt;
    GameObject target_object;
    public bool is_right;
    public bool is_grab;
    public Vector3 distance_to_target;
    public string name;
    private interaction interact;

    // Use this for initialization
    void Start () {
        GameObject game_thing = GameObject.Find("Manus_VR_Driver");     //Seek global API
        manus_inpt = game_thing.GetComponent<Manus_interpreter>();
        interact = this.GetComponent<interaction>();
    }

    // Update is called once per frame
    //void Update () {


    //void OnCollisionEnter(Collision collision)
    //{

    //    target_object = GameObject.Find(collision.collider.name); //Locate the other object this object is in collision with. 
    //    name = collision.collider.name;
    //     distance_to_target = target_object.transform.position - this.transform.position ;//Distance offset
    //    Debug.Log("Clicked");
    // }

    private void Update()
    {
        if (is_right)
        {
            is_grab = manus_inpt.get_hand(1).get_grabbing(); //Rightmost hand
        }
        else
        {
            is_grab = manus_inpt.get_hand(0).get_grabbing();
        }

        if (is_grab)
        {
            //target_object.transform.position = distance_to_target + this.transform.position;
            interact.Pickup();
        }
        else
        {
            interact.Drop(this.GetComponent<Rigidbody>());
        }
    }




    //private void OnCollisionStay(Collision collision)
    //{
    //    if (is_right)
    //    {
    //        is_grab = manus_inpt.get_hand(1).get_grabbing(); //Rightmost hand
    //    }
    //    else
    //    {
    //        is_grab = manus_inpt.get_hand(0).get_grabbing();
    //    }
    //    //is_grab = true;
    //    if (is_grab)
    //    {
    //        target_object.transform.position = distance_to_target + this.transform.position;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("Un-Clicked"); 
    //}
}

