using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float MouseValue;
    public float speed;
    public float maxLeft;
    public float maxRight;
    void Update()
    {
        if (Input.GetKey("q") || Input.GetKey("e"))
        {
            float MouseScrollInput = Input.mouseScrollDelta.y;
            float Current_y = transform.eulerAngles.y;
            Debug.Log(Current_y);

            //this.transform.Rotate(0, MouseScrollInput * 5, 0);
            GameObject.Find("_camera").GetComponent<Camera>().orthographicSize = GameObject.Find("_camera").GetComponent<Camera>().orthographicSize - Input.mouseScrollDelta.y * 2;

            if (Input.GetKey("q"))
            {
                Debug.Log("q && y=="+Current_y);
                if (Current_y < maxLeft || Current_y > 180)
                {
                    transform.Rotate(0, speed * Time.deltaTime, 0);
                }
            }

            if (Input.GetKey("e"))
            {
                if (Current_y > maxRight || Current_y < 180)
                {
                    transform.Rotate(0, -speed * Time.deltaTime, 0);
                }
            }
        }
    }
}
