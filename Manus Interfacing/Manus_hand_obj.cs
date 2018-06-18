///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Manus_hand_obj.h/cpp
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_hand_obj.cs 
//  Revision:        1.1 - 6/11/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Data structure for a manus_hand object. Refactored for C#
//
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections.Generic;
//A Manus hand object class - Assume it's a hand.  

namespace manus_interface {
    public class Manus_hand_obj 
    {
        private double arm_lenght;
        private double upper_arm_length, upper_arm_neck;
        private double lower_arm_length, lower_arm_neck;
        private byte bat_level;
        private Vector3 upperNeckOffset;

        //In practive, there are 10 bands (Resistive elastic sensors) for each finger on the manus.
        //This array contains all of those measurements starting from the left to rightmsot finger.  
        private List<double> hand_measurements_raw;

        //Specialized data relating to orientation of certain elements. IMU data not fully understood yet. 
        private Quaternion[] imu_data;
        private Quaternion wrist_data; 

        //For each finger, a specific bone and position is given by manus to interpret. 
        private List<Finger> hand_fingers;
        private List<Finger> hand_fingers_manus_profile;
        //#######################################################
        
        //Constructor 
        public Manus_hand_obj() { int e = 1; }
  
    	//Set all lenghts for arm as needed 
	    public void set_lenghts_arm(double l, double ual, double uan, double lal, double lan, Vector3 upN) {
		    arm_lenght = l;
		    upper_arm_length = ual;
		    upper_arm_neck = uan;
		    lower_arm_length = lal;
		    lower_arm_neck = lan;
		    upperNeckOffset = upN;
	    }

        public void set_imus(Quaternion[] imus)
        {
            imu_data = imus; 
        }

	    //Set the hand data arry to the array in this object with the poses. 
	    //void set_hand(finger *hand[]) {
	    //	*hand_fingers = hand;
	    //}

	    /*void Manus_hand_obj::add_finger(Finger fingr) {
		    hand_fingers.push_back(fingr);
	    }*/

        public void set_wrist(Quaternion inQ)
        {
            wrist_data = inQ;
        }

	    public void add_vector_fingers_manus_profile(List<Finger> fingr) {
            hand_fingers_manus_profile = new List<Finger>();
            Finger[] doe = fingr.ToArray();
            foreach (Finger d in doe)
            {
                hand_fingers_manus_profile.Add(d);
            }

            ///hand_fingers_manus_profile = fingr;
        }
	    public void add_vector_fingers(List<Finger> fingr) {
            //hand_fingers = new List<Finger>();
            //Finger[] doe = fingr.ToArray();
            //foreach (Finger d in doe)
            //{
            //    hand_fingers.Add(d);
            //}

            hand_fingers = fingr;
	    }

	    //Set the raw hand data array to the private array in this object. 
	    public void set_hand_raw(List<double> raw_hand) {
		    hand_measurements_raw = raw_hand;
	    }

        public void set_bat(byte bat)
        {
            bat_level = bat; 
        }

	    //Get the data for the arm lenghts sent in an array
	    public List<double> get_arm_lenghts() {
		    List<double> lengths =  new List<double>{ arm_lenght, upper_arm_length, lower_arm_length, upper_arm_neck, lower_arm_neck };
		    return lengths;
	    }

	    //Set the manus_profile hand data 
	    public void set_hand_profile_manus(List<Finger> fingers) {
		    hand_fingers_manus_profile = fingers;
	    }

        public Quaternion[] get_imus()
        {
            return imu_data;
        }

	    //Get the data for a hand
	    public List<Finger> get_hand() {
		    return hand_fingers;
	    }

        //Summary:          Get the data for the wrist on the hand
        public Quaternion get_wrist()
        {
            return wrist_data;
        }

        //Return the battery level; 
        public byte get_bat()
        {
            return bat_level;
        }

        //Slightly useless, returns a preset for the manus hand profile
	    public List<Finger> get_hand_profile_manus() {
		    return hand_fingers_manus_profile;
	    }

        //Returns compression ratios for each finger on the manus. 
	    public List<double> get_raw_hand() {
		    return hand_measurements_raw;
	    }
    }
	//######################################################################################################################################
}