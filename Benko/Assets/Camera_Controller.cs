using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float MouseValue;
    public float scale;
    

    
    void Update()
    {
        if (!Input.GetKey("q"))
        {
            this.transform.Rotate(0, Input.mouseScrollDelta.y * scale, 0);
        }

        if (Input.GetKey("q"))
        {
            float Size = GameObject.Find("_camera").GetComponent<Camera>().orthographicSize;
            print(Size);
            GameObject.Find("_camera").GetComponent<Camera>().orthographicSize = GameObject.Find("_camera").GetComponent<Camera>().orthographicSize - Input.mouseScrollDelta.y * 2;
        }
    }
}
