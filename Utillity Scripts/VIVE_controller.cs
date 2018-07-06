///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Vive_controller.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_interpreter.cs
//  Revision:        1.0 - 7/5/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Vive controller wrapper
//
///////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIVE_controller : MonoBehaviour {

    private SteamVR_TrackedObject track_obj;
    private SteamVR_Controller.Device device_obj;
    public bool the_triggering;

	// Use this for initialization
	void Start () {
        track_obj = GetComponent<SteamVR_TrackedObject>();	
	}
	
	// Update is called once per frame
	void Update () {
        device_obj = SteamVR_Controller.Input((int)track_obj.index);
        if (device_obj.GetPressDown
            (SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("My tingle");
            the_triggering = true;
            device_obj.TriggerHapticPulse(3900);
        }
        else
        {
            the_triggering = false;
        }
	}
}
