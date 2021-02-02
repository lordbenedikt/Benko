using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_Controller : MonoBehaviour
{
    public float speed;

    void Update()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Translate(new Vector3(-1,0,0) * Time.deltaTime);
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime, 0.1f);
        
    }
}