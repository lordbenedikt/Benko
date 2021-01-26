using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float MouseValue;
    public float scale;
    

    
    void Update()
    {
        float MouseScrollInput = Input.mouseScrollDelta.y;
        float Current_y = this.transform.eulerAngles.y;

        if (!Input.GetKey("q") ) //-15 48
        {
            this.transform.Rotate(0, MouseScrollInput * scale, 0);
            if(Current_y > 48)
            {
                this.transform.Rotate(0, -2 * scale, 0);
            }
            if (Current_y < -15)
            {
                this.transform.Rotate(0, 2 * scale, 0);
            }
        }

        if (Input.GetKey("q"))
        {
            float Size = GameObject.Find("_camera").GetComponent<Camera>().orthographicSize;
            //print(Size);
            GameObject.Find("_camera").GetComponent<Camera>().orthographicSize = GameObject.Find("_camera").GetComponent<Camera>().orthographicSize - Input.mouseScrollDelta.y * 2;
        }
    }
}
