using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //public float MouseValue;
    //public float speed;
    //public float maxLeft;
    //public float maxRight;
    Vector2 prevMousePos;
    float xRotation = 0;
    float yRotation = 0;

    public GameObject _camera;

    void Update()
    {
        float Current_y = transform.eulerAngles.y;
        float MouseScrollInput = Input.mouseScrollDelta.y;
            _camera.GetComponent<Camera>().fieldOfView = _camera.GetComponent<Camera>().fieldOfView - Input.mouseScrollDelta.y * 1;
       

        if(Input.GetMouseButton(1)) {
            xRotation += prevMousePos.y-Input.mousePosition.y;
            yRotation += Input.mousePosition.x-prevMousePos.x;
            transform.rotation = Quaternion.Euler(new Vector3(xRotation *0.5f ,yRotation*0.5f,0));
        }
        if(Input.GetMouseButton(2)) {
            transform.position = new Vector3(transform.position.x,0,transform.position.z) + Quaternion.Euler(0,yRotation,0) * new Vector3(0.03f*(prevMousePos.x-Input.mousePosition.x),0,0.03f*(prevMousePos.y-Input.mousePosition.y));
        }

        // if (Input.GetKey("q") || Input.GetKey("e"))
        // {
        //     if (Input.GetKey("q"))
        //     {
        //         //Debug.Log("q && y=="+Current_y);
        //         if (Current_y < maxLeft || Current_y > 180) transform.Rotate(0, speed * Time.deltaTime, 0);
        //     }

        //     if (Input.GetKey("e"))
        //     {
        //         if (Current_y > maxRight || Current_y < 180)transform.Rotate(0, -speed * Time.deltaTime, 0);
        //     }
        // }
        prevMousePos = Input.mousePosition;
    }
}
