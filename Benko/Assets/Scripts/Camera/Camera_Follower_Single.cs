using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follower_Single : MonoBehaviour
{
    Transform PlayerTransform;
    public Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public GameObject follower;
    private GameObject selectedUnit;


    void Start()
    {
        PlayerTransform = follower.GetComponent<Transform>();
        _cameraOffset = transform.position - PlayerTransform.position;
        
    }

    void Update()
    {
        GetSelectedUnit();

        if (selectedUnit != null)
        {
            Vector3 newPos = selectedUnit.transform.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        }


        //if (PlayerTransform != null)
        //{
        //    Vector3 newPos = PlayerTransform.position + _cameraOffset;

        //    transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        //}
    }

    public void GetSelectedUnit()
    {
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject ply in Units)
        {
            if (ply.GetComponent<isSelected>().IsSelected == true)
            {
                selectedUnit = ply;
                return;
            }
            if (selectedUnit == null)
            {
                selectedUnit = null;
            }
        }
    }
}