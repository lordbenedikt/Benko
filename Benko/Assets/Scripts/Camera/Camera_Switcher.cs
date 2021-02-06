using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Switcher : MonoBehaviour
{

    public int number;

    Camera_Controller_FreeStyle Freestyle;
    Camera_Controller_Single Single;
    Camera_Controller_Multiple Multiple;

    void Start()
    {
        Freestyle = GetComponent<Camera_Controller_FreeStyle>();
        Single = GetComponent<Camera_Controller_Single>();
        Multiple = GetComponent<Camera_Controller_Multiple>();
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Freestyle.enabled = true;
            Single.enabled = false;
            Multiple.enabled = false;
        }

        if (Input.GetKeyDown("2"))
        {
            Freestyle.enabled = false;
            Single.enabled = true;
            Multiple.enabled = false;
        }
        if (Input.GetKeyDown("3"))
        {
            Freestyle.enabled = false;
            Single.enabled = false;
            Multiple.enabled = true;
        }
    }

    

    
 
 
}
