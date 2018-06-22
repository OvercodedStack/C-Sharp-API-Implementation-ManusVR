///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Camera_controls.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_Open_VR V2
//  Revision:        1.0 - 6/22/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Camera control wrapper for object in control. 
//
///////////////////////////////////////////////////////////////////////////////
//Some Code inherited from https://forum.unity.com/threads/moving-main-camera-with-mouse.119525/
using UnityEngine;

public class Camera_controls : MonoBehaviour {
    //Limits for camera 
    private float x_cam_rot_up      = 90;
    private float y_cam_rot_up      = 90;
    private float z_cam_rot_up      = 90;
    private float x_cam_rot_down    = -90;
    private float y_cam_rot_down    = -90;
    private float z_cam_rot_down    = -90;

    //Speed Limits
    public float horizontal_speed   = 40;
    public float vertical_speed     = 40;
    public float foward_speed       = 40;
    public float back_speed         = 40; 

	// Update is called once per frame
	void Update () {
        Vector3 shift; 
        while (Input.GetMouseButtonDown(1))
        {
            float hor = horizontal_speed * Input.GetAxis("Mouse Y");
            float ver = vertical_speed * Input.GetAxis("Mouse X");
            transform.Translate(ver, hor, 0);
        }

        if( Input.GetKeyDown("Foward"))
        {
            shift = new Vector3(foward_speed, 0, 0);
            Debug.Log("FOWARD YO");
            this.transform.Translate(shift);
        }

        if (Input.GetKeyDown("Back"))
        {
            shift = new Vector3(back_speed, 0, 0);
            this.transform.Translate(shift);
        }

        if (Input.GetKeyDown("Left"))
        {
            shift = new Vector3(0, horizontal_speed, 0);
            this.transform.Translate( shift);
        }

        if (Input.GetKeyDown("Right"))
        {
            shift = new Vector3(0, -horizontal_speed, 0);
            this.transform.Translate(shift);
        }

    }
}
