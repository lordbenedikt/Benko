using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //public float MouseValue;
    public float speed;
    public float maxLeft;
    public float maxRight;

    public GameObject _camera;

    void Update()
    {
        float Current_y = transform.eulerAngles.y;
        float MouseScrollInput = Input.mouseScrollDelta.y;
            _camera.GetComponent<Camera>().orthographicSize = _camera.GetComponent<Camera>().orthographicSize - Input.mouseScrollDelta.y * 1;
       


        if (Input.GetKey("q") || Input.GetKey("e"))
        {
            if (Input.GetKey("q"))
            {
                //Debug.Log("q && y=="+Current_y);
                if (Current_y < maxLeft || Current_y > 180) transform.Rotate(0, speed * Time.deltaTime, 0);
            }

            if (Input.GetKey("e"))
            {
                if (Current_y > maxRight || Current_y < 180)transform.Rotate(0, -speed * Time.deltaTime, 0);
            }
        }
    }
}
