///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Manus_interpreter.h.cpp
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_interpreter.cs
//  Revision:        1.1 - 6/11/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Manus_VR Hand data phraser - intended to wrap around a gameobject and provide nessesary values to a mesh in order to simulate compression and orientation.
//
///////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManusVR;
using manus_interface;

public class Manus_hand_model_inputter : MonoBehaviour {
    [SerializeField]
    private string hand_side;                   //Which hand side to use 
    public Vector3 hand_position;               //Adjust the postion of the hand object
    public Quaternion hand_orientation;         //Adjust the orientation of the hand object 
    private Quaternion[] finger_orientations;   //Adjust the orientation of the finger objects 
    private double[] finger_compression;        //Finger compression
    GameObject hand_mesh;                       //Setup GameObjects
    GameObject my_wrist;
    Manus_interpreter manus_interpret;          //The Manus API interpreter
    device_type_t type_of_device;               //Which side is this hand on - on Manus terms


    void Start () {
        string wrist_string; 
        if (hand_side == "right_hand")                  //Check if my hand is left or right and setup accordingly 
        {
            type_of_device = device_type_t.GLOVE_RIGHT;
            wrist_string = "RightHand/hands:r_hand_world";
        }
        else
        {
            type_of_device = device_type_t.GLOVE_LEFT;
            wrist_string = "LeftHand/hands:l_hand_world";

        }
        hand_mesh = GameObject.Find(hand_side);               //Find that object
        my_wrist = GameObject.Find(wrist_string); //and that one too. 
        manus_interpret = this.GetComponent<Manus_interpreter>();      //Find the API 
    }
	
	// Update is called once per frame
	void Update () {
        Manus_hand_obj hand = manus_interpret.get_hand(type_of_device);   //Select glove
        my_wrist.transform.rotation = hand.get_wrist();                   //Get the rotation of the quaternion and set equal 
    }
}
