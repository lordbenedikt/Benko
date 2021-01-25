using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{

    public float minX;
    public float minZ;
    public float s;
    public int cols;
    public int rows;


    public GameObject target;
    public GameObject structure;
    GameObject gameController;

    Vector3 truePos;
    public float gridSize;

    public int indexFromPos(float x, float z) {
        return -1;
    }
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // print("Grid: " + minX%gridSize + " " + minX + " " + minZ);
        // float xOffset = minX%gridSize;
        // float zOffset = minZ%gridSize;
        // if (xOffset<0) xOffset += gridSize;
        // if (zOffset<0) zOffset += gridSize;
        // truePos.x = Mathf.Floor((target.transform.position.x) / gridSize) * gridSize + xOffset;
        // truePos.z = Mathf.Floor((target.transform.position.z) / gridSize) * gridSize + zOffset;
        // truePos.y = 0;

        // structure.transform.position = truePos;
    }

    public Vector3 snapToGrid(GameObject o, GameObject target) {
        Vector3 truePos = new Vector3(0,0,0);
        // print("Grid: " + minX%gridSize + " " + minX + " " + minZ);
        // float xOffset = minX%gridSize;
        // float zOffset = minZ%gridSize;
        // if (xOffset<0) xOffset += gridSize;
        // if (zOffset<0) zOffset += gridSize;
        // truePos.x = Mathf.Floor((target.transform.position.x) / gridSize) * gridSize + xOffset;
        // truePos.z = Mathf.Floor((target.transform.position.z) / gridSize) * gridSize + zOffset;
        // truePos.y = 0;

        return truePos;
    }
}
