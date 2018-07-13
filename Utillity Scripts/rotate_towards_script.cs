using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//OBSOLTE, DO NOT USE


public class rotate_towards_script : MonoBehaviour
{
    GameObject target;
    Transform tar_transform;
    float speed = 0.0f;


    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Rotate Handle");
        tar_transform = target.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 pivot_pos = tar_transform.localPosition;
        Vector3 target_rot = tar_transform.localEulerAngles;
        

        //// The step size is equal to speed times frame time.
        //float step = speed * Time.deltaTime;

        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        //Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(RotatePointAroundPivot(transform.position,pivot_pos,transform.localEulerAngles));

    }
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
}
