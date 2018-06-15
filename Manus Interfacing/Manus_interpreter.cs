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
//  Data phraser for the manus_hand and finger objects. Sorts and compiles the data adquired from the Manus device. Refactored for C#
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using ManusVR;

//######################################################################################################################################
//Class to phrase the data on a manus device hand.
namespace manus_interface
{
    public class Manus_interpreter : MonoBehaviour
    {
        //Adjust update speed of manus;
        [SerializeField]
        int updateSpeed;


        //Output fields
        [SerializeField]
        private byte bat_value;
        [SerializeField]
        private double index;
        [SerializeField]
        private double middle;
        [SerializeField]
        private double ring;
        [SerializeField]
        private double thumb;

        //Connected 
        bool isL, isR;

        //Private session stamp for the manus device. 
        IntPtr session;

        //hand pointers
        manus_hand_t lefth, righth;

        //Raw pointers
        manus_hand_raw_t rightraw, leftraw;

        //Inverse Kinemaic pointers
        ik_profile_t myProfileL, myProfileR;

        //Arm pointers 
        ik_body_t left_arm, right_arm;

        //Create a Hand array for each hand. Each hand is an data structure.
        public Manus_hand_obj[] hands = new Manus_hand_obj[2];

        ///public Manus_interpreter(ManusVR.Manus.manus_session_t robot_session) {
        ///

        //////!Converters for data types
        ////private Quaternion convert_to_unity_quaternion(quarternion quart_in)
        ////{
        ////    Quaternion quar = new Quaternion();
        ////    quar.x = (float)quart_in.x;
        ////    quar.y = (float)quart_in.y;
        ////    quar.z = (float)quart_in.z;
        ////    quar.w = (float)quart_in.w;
        ////    return quar;
        ////}


        ////private Vector3 convert_to_unity_vector(vector_manus in_vector)
        ////{
        ////    Vector3 vec = new Vector3();
        ////    vec.x = (float)in_vector.x;
        ////    vec.y = (float)in_vector.y;
        ////    vec.z = (float)in_vector.z;
        ////    return vec;
        ////}


        //!Start funct.

        void print_quat(Quaternion quart)
        {
            Debug.Log( "Printing Quarternion: " +
        "X: " + quart.x +
        "Y: " + quart.y +
        "Z: " + quart.z +
        "W: " + quart.w 
        );
        }

        void print_vector(Vector3 vec)
        {
            Debug.Log("Printing Vector: " +
        "X: " + vec.x +
        "Y: " + vec.y +
        "Z: " + vec.z 
        );
        }


        void Start()
        {
            Debug.Log("I tried.");
            session = new IntPtr();
            lefth = new manus_hand_t();
            righth = new manus_hand_t();
            leftraw = new manus_hand_raw_t();
            rightraw = new manus_hand_raw_t();
            myProfileL = new ik_profile_t();
            myProfileR = new ik_profile_t();
            left_arm = new ik_body_t();
            right_arm = new ik_body_t();
            isR = false;
            isL = false;

            Manus.ManusInit(out session);
            Debug.Log("I is done.");

        }


        //Data cleaner for data recieved for double values from the Manus.
        void merge_value<T>(T output_value,T input_value)
        {
            output_value = input_value;
        }

        //Cycles one period of update for each of the manus hands. 
        void Update()
        {
            //Manus.ManusInit(out session);
            isL = Manus.ManusIsConnected(session, device_type_t.GLOVE_LEFT);
            isR = Manus.ManusIsConnected(session, device_type_t.GLOVE_RIGHT);
            add_manus_hand(ref lefth, ref leftraw, device_type_t.GLOVE_LEFT, left_arm, myProfileL);
            add_manus_hand(ref righth, ref rightraw, device_type_t.GLOVE_RIGHT, right_arm, myProfileR);

            //Finger[] f = this.hands[0].get_hand_profile_manus().ToArray();
            double[] ls = this.hands[1].get_raw_hand().ToArray();
            bat_value = this.hands[1].get_bat();
            index = ls[0];
            middle = ls[1];
            ring = ls[2];
            thumb = ls[3];

            Quaternion q = hands[0].get_wrist();

            Debug.Log("Wrist data= X: " + q.x + "Y: " + q.y + "Z: " + q.z + "W: " + q.w);

            //Debug.Log(carpal_inx);
    }

        //Function that phrases in a single finger 
        private void add_manus_hand(ref manus_hand_t hand, ref manus_hand_raw_t raw_hand, device_type_t which_hand_side, ik_body_t body_side, ik_profile_t my_profile)
        {
            //Manus Hand Objects
            Manus_hand_obj hand_in_use = new Manus_hand_obj();

            //Process data for the left hand, including raw data. 
            Manus.ManusGetHandRaw(session, which_hand_side, out raw_hand);
            Manus.ManusGetProfile(session, out my_profile);    ///Wrong assumption, it does not provide real-time data. 
            Manus.ManusGetHand(session, which_hand_side, out hand);
            
            Manus.ManusGetBatteryLevel(session, which_hand_side, out bat_value);

            //Set Battery level
            hand_in_use.set_bat(bat_value);

            //Set Arm calculations
            add_arm_calc(ref hand, ref body_side, ref my_profile, ref hand_in_use);

            //Set the raw_double finger data from manus 
            add_hand_fingers_raw(ref raw_hand, ref which_hand_side, ref hand_in_use);

            //Set manus_profile finger data
            add_manus_profile_hands(ref my_profile, ref hand_in_use);
            
            //Set regular Manus hand data
            add_hand_fingers(ref hand, ref which_hand_side, ref hand_in_use);

            //Add relevant data to the hands array
            if (which_hand_side == device_type_t.GLOVE_LEFT)
            {
                //hands.insert(hands.begin(),hand_in_use); //Left Glove
                hands[0] = hand_in_use;
            }
            else
            {
                //hands.assign(1, hand_in_use); //Right Glove
                hands[1] = hand_in_use;
            }
        }

        //Get a hand from the list 
        public Manus_hand_obj get_hand(device_type_t side)
        {
            if (side == device_type_t.GLOVE_LEFT)
            {
                return hands[0];
            }
            else
            {
                return hands[1];
            }
        }

        //Add the calcuations for the arm calculations on the manus API
        private void add_arm_calc(ref manus_hand_t hand, ref ik_body_t body_side, ref ik_profile_t my_profile, ref Manus_hand_obj my_hand)
        {
            //double arm_calcs[2];
            ManusVR.Manus.ManusUpdateIK(session, ref body_side); //Broken, does nothing. 
            //Hand function to set the profile characteristics of the arm. 
            my_hand.set_lenghts_arm(my_profile.shoulderLength, my_profile.upperArmLength, my_profile.upperNeckLength, my_profile.lowerArmLength,
                my_profile.lowerNeckLength, process_vector(my_profile.upperNeckOffset));
            ////print_quat(process_quat(body_side.left.lowerArm.rotation));
            ////print_vector(process_vector(body_side.left.lowerArm.translation));
        }


        //Add vector/quarternion measurements to a finger
        private void add_hand_fingers(ref manus_hand_t device, ref device_type_t side, ref Manus_hand_obj manus_hand)
        {
            List<Finger> single_hand_array = new List<Finger>();   //Finger array
            pose temp_pose;                     //Temporary pose format.
            List<pose> temp_finger = new List<pose>();           //Temporary array for bones.
            for (int i = 0; i < 5; i++)//Illiterate through the fingers
            {
                for (int j = 0; j < 5; j++)//Illiterate through the poses. 
                {
                    temp_pose.rotation = process_quat(device.fingers[i].joints[j].rotation);
                    temp_pose.translation = process_vector(device.fingers[i].joints[j].translation);
                    temp_finger.Add(temp_pose);//Add poses to a temporay finger array
                }
                Finger real_finger = new Finger(temp_finger); //Instantiate a finger class
                single_hand_array.Add(real_finger);           //Add the finger to the hand
            }
            //**NEW** Pass imu Data to device. 
            quat_t[] quarts = device.raw.imu;
            Quaternion[] quarts_to_write = new Quaternion[2];
            quarts_to_write[0] = process_quat(quarts[0]);
            quarts_to_write[1] = process_quat(quarts[1]);
            manus_hand.set_imus(quarts_to_write);        
            manus_hand.add_vector_fingers(single_hand_array); //Add in vector data from manus for each individual bone. 
            manus_hand.set_wrist(process_quat(device.wrist)); //**NEW** Add in wrist data into the manus_hand_obj

        }

        //Converter for unity type
        private Vector3 process_vector(vector_t vect)
        {
            Vector3 vec_out = new Vector3();
            vec_out.x = (float)vect.x;
            vec_out.y = (float)vect.y;
            vec_out.z = (float)vect.z;
            return vec_out;
        }

        //Converter for unity type
        private Quaternion process_quat(quat_t quat)
        {
            Quaternion quat_out = new Quaternion();
            quat_out.x = (float)quat.x;
            quat_out.y = (float)quat.y;
            quat_out.z = (float)quat.z;
            quat_out.w = (float)quat.w;
            return quat_out;
        }

        ///***Wrong assumption, it does not provide real-time data***
        private void add_manus_profile_hands(ref ik_profile_t my_profile, ref Manus_hand_obj my_hand)
        {
            List<double> temporary_finger_array = new List<double>(); //Temporary array for doubles to insert into finger.
            List<Finger> fingers = new List<Finger>();

            //Horrible method of phrasing, but what happens is that a List array is phrased over from the manus array
            //Index Finger
            for (int c = 0; c < 4; c++)
            {
                ///    temporary_finger_array.Add(my_profile.handProfile.index.bones[c]);
                temporary_finger_array.Add(my_profile.handProfile.fingers[0].bones[c]);
                // Debug.Log(my_profile.handProfile.fingers[0].bones[c]);
                // Debug.Log("Quack");
            }
            Finger index_bone = new Finger(temporary_finger_array);    // Array is inserted into the finger object. 
            temporary_finger_array.Clear();
            fingers.Add(index_bone);//Finger is added into hand. 

            //Middle Finger
            for (int c = 0; c < 4; c++)
            {
                temporary_finger_array.Add(my_profile.handProfile.fingers[1].bones[c]);
            }
            Finger middle_bone = new Finger(temporary_finger_array);    // Array is inserted into the finger object. 
            temporary_finger_array.Clear();
            fingers.Add(middle_bone);//Finger is added into hand. 

            //Ring Finger
            for (int c = 0; c < 4; c++)
            {
                temporary_finger_array.Add(my_profile.handProfile.fingers[2].bones[c]);
            }
            Finger ring_bone = new Finger(temporary_finger_array);    // Array is inserted into the finger object. 
            temporary_finger_array.Clear();
            fingers.Add(ring_bone);//Finger is added into hand. 

            //Pinky Finger
            for (int c = 0; c < 4; c++)
            {
                temporary_finger_array.Add(my_profile.handProfile.fingers[3].bones[c]);
            }

            Finger pinky_bone = new Finger(temporary_finger_array);    // Array is inserted into the finger object. 
            temporary_finger_array.Clear();
            fingers.Add(pinky_bone);//Finger is added into hand. 

            //Thumb Finger
            for (int c = 0; c < 4; c++)
            {
                temporary_finger_array.Add(my_profile.handProfile.fingers[4].bones[c]);
            }
            Finger thumb_bone = new Finger(temporary_finger_array);    // Array is inserted into the finger object. 
            temporary_finger_array.Clear();
            fingers.Add(thumb_bone); //Finger is added into hand. 

            my_hand.add_vector_fingers_manus_profile(fingers);
        }

        //Add finger calculations that are stated to be raw decimal calculations from the Manus SDK
        //Phrase the array of raw finger calculations from the Manus SDK.
        private void add_hand_fingers_raw(ref manus_hand_raw_t hand, ref device_type_t side, ref Manus_hand_obj manus_hand)
        {
            List<double> single_hand_array_raw = new List<double>();
            if (side == device_type_t.GLOVE_LEFT)
            {
                for (int h = 0; h < 5; h++)
                {
                    single_hand_array_raw.Add(hand.finger_sensor[h]);
                }
            }
            else
            {
                for (int h = 5; h < 10; h++)
                {
                    single_hand_array_raw.Add(hand.finger_sensor[h]);
                }
            }
            manus_hand.set_hand_raw(single_hand_array_raw);
        }
    }
}
//######################################################################################################################################
 
