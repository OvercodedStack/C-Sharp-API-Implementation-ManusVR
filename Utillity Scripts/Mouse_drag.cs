using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_drag : MonoBehaviour {
    public float distance = 9;

    public Vector3 debug_vector;
    private void OnMouseDrag()
    {

        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        
        Vector3 obj_pos = Camera.main.ScreenToWorldPoint(mouse_pos);

        debug_vector = obj_pos;

        transform.position = obj_pos; 
    }
    void LateUpdate()
    {
        if (Input.GetButton("Q"))
        {
            distance++;
        }
        if (Input.GetButton("E"))
        {
            distance--;
        }
    }
}
