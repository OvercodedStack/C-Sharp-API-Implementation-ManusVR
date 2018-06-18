///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Finger.h/cpp
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Finger.cs
//  Revision:        1.1 - 6/7/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Data structure for a finger. Refactored for C#.
//
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections.Generic;

//The Finger Class 
namespace manus_interface
{
    public class Finger
    {
        private double bandstrech;              //For Raw sensor data only. 
        private List<pose> bones;               //Bone Data -  pose data
        private List<double> bones_doubles;     //More Data, for use when using the profile_manus_bone option
        //##################################

        public Finger() {
            
        }

        public Finger(List<pose> array_in)
        {
            bones = new List<pose>();

            pose[] posee = array_in.ToArray();
            foreach (pose p in posee)
            {
                bones.Add(p);
            }
            
            //bones.Clear();
            //bones = new List<pose>(array_in);
        }
        public Finger(List<double> array_in)
        {
            //bones_doubles = new List<double>();
            //double[] doe = array_in.ToArray();
            //foreach (double d in doe)
            //{
            //    bones_doubles.Add(d);
            //}
            bones_doubles = array_in;
            //bones_doubles.Clear();
            //bones_doubles = new List<double>(array_in);
        }

        //Template for single double band finger (raw)
        public Finger(double bone_band)
        {
            bandstrech = bone_band;
        }

        //Getter for raw finger data
        public double get_fingers_raw()
        {
            return bandstrech;
        }

        public List<double> get_fingers_manus()
        {
            return bones_doubles;
        }

        //Could be doubles, could be pose data. 
        public List<pose> get_finger_data()
        {
            return bones;
        }
    }
}