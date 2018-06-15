using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manus_hand_model_inputter : MonoBehaviour {
    [SerializeField]
    private string hand_side;                   //Which hand side to use 
    public Vector3 hand_position;               //Adjust the postion of the hand object
    public Quaternion hand_orientation;         //Adjust the orientation of the hand object 
    private Quaternion[] finger_orientations;   //Adjust the orientation of the finger objects 
    private double[] finger_compression;        //Finger compression

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
