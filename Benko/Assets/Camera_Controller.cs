using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float MouseValue;
    public float speed;
    void Update()
    {
        float MouseScrollInput = Input.mouseScrollDelta.y;
        float Current_y = transform.eulerAngles.y;
        Debug.Log(Current_y);

        //this.transform.Rotate(0, MouseScrollInput * 5, 0);
GameObject.Find("_camera").GetComponent<Camera>().orthographicSize = GameObject.Find("_camera").GetComponent<Camera>().orthographicSize - Input.mouseScrollDelta.y * 2;
        
        if (Input.GetKey("q"))
        {
            if (Current_y <= 30 || Current_y >= 300)
            {
                transform.Rotate(0, speed * Time.deltaTime, 0);
            }
            else
            {
                transform.Rotate(0, -speed * Time.deltaTime, 0);
            }
            
        }

        if (Input.GetKey("e"))
        {
            if (Current_y > 310 || Current_y <= 40)
            {
                transform.Rotate(0, -speed * Time.deltaTime, 0);
            }
            else
            {
                transform.Rotate(0, speed * Time.deltaTime, 0);
            }

        } 
        
    }
}
