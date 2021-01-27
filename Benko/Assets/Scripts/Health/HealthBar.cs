using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthDisplay;
    GameObject _camera;

    private void Start()
    {
        _camera = GameObject.Find("_camera");
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
