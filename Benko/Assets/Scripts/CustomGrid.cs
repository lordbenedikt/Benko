using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{

    public float xOffset;
    public float zOffset;
    public GameObject target;
    public GameObject structure;
    Vector3 truePos;
    public float gridSize;

    public int indexFromPos(float x, float z) {
        x += xOffset;
        z += zOffset;
        return -1;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        truePos.x = xOffset + Mathf.Floor((target.transform.position.x+0.5f-xOffset) / gridSize) * gridSize;
        truePos.z = zOffset + Mathf.Floor((target.transform.position.z+0.5f-zOffset) / gridSize) * gridSize;
        truePos.y = 0;

        structure.transform.position = truePos;
    }
}
