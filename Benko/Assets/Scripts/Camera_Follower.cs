using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follower : MonoBehaviour
{
    Transform PlayerTransform;
    private Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public GameObject follower;


	void Start()
    {
        PlayerTransform = follower.GetComponent<Transform>();
        _cameraOffset = transform.position - PlayerTransform.position;
        
    }

    void Update()
    {


		if (PlayerTransform != null)
        {
            Vector3 newPos = PlayerTransform.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        }
    }
}