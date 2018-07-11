using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using manus_interface;


public class hand_pickup : MonoBehaviour {
    //Locate interpreter
    Manus_interpreter manus_inpt;
    GameObject target_object;
    public bool is_right;
    public bool is_grab;
    float distance_to_target;

    // Use this for initialization
    void Start () {
        GameObject game_thing = GameObject.Find("Manus_VR_Driver");     //Seek global API
        manus_inpt = game_thing.GetComponent<Manus_interpreter>();
    }

    // Update is called once per frame
    //void Update () {


    void OnCollisionEnter(Collision collision)
    {
        if (is_right)
        {
            is_grab = manus_inpt.get_hand(1).get_grabbing(); //Rightmost hand
        }
        else
        {
            is_grab = manus_inpt.get_hand(0).get_grabbing();
        }
        target_object = GameObject.Find(collision.collider.name); //Locate the other object this object is in collision with. 
         = Vector3.Distance(this.transform.position, target_object.transform.position);//Distance offset
     }

    private void OnCollisionStay(Collision collision)
    {
        if (is_grab)
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }


}

