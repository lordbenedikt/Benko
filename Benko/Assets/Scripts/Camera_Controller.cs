using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //public float MouseValue;
    public float speed;
    public float maxLeft;
    public float maxRight;
    public float xRotation = 0;
    public float yRotation = 20;
    public float scale = 7;
    public GameObject _camera;

    Vector2 prevMousePos;

    void Update()
    {
        float Current_y = transform.eulerAngles.y;
        scale = Mathf.Clamp(scale - Input.mouseScrollDelta.y,2,20);
        _camera.GetComponent<Camera>().orthographicSize = scale;
        print("scale: " + scale);


        if(Input.GetMouseButton(1)) {
            xRotation += 0.5f*(prevMousePos.y-Input.mousePosition.y);
            yRotation += 0.5f*(Input.mousePosition.x-prevMousePos.x);
            transform.rotation = Quaternion.Euler(new Vector3(xRotation,yRotation,0));
        }
        if(Input.GetMouseButton(2)) {
            transform.position = new Vector3(transform.position.x,0,transform.position.z) + Quaternion.Euler(0,yRotation,0) * new Vector3(0.01f*scale*(prevMousePos.x-Input.mousePosition.x),0,0.01f*scale*(prevMousePos.y-Input.mousePosition.y));
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
