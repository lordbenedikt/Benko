using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{

    public float minX;
    public float minZ;
    public float gridSize;
    public int cols;
    public int rows;


    public GameObject target;
    public GameObject structure;
    GameObject gameController;

    Vector3 truePos;


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

    public int gridIndexFromPos(float x, float z) {
        if(x<minX || z<minZ) return -1;
        if(x>minX+gridSize*cols || z>minZ+gridSize*rows) return -1;
        int _col = (int)Mathf.Floor((x + cols*gridSize/2)/gridSize);
        int _row = (int)Mathf.Floor((z + rows*gridSize/2)/gridSize);
        // print("col: "+_col+" row: "+_row+"\n");
        return cols*_row + _col;
    }

    public Vector3 snapToGrid(GameObject o, GameObject target) {
        Vector3 truePos = new Vector3(0,0,0);
        print("Grid: " + minX%gridSize + " " + minX + " " + minZ);
        float xOffset = minX%gridSize;
        float zOffset = minZ%gridSize;
        if (xOffset<0) xOffset += gridSize;
        if (zOffset<0) zOffset += gridSize;
        truePos.x = Mathf.Floor((target.transform.position.x) / gridSize) * gridSize + gridSize/2;
        truePos.z = Mathf.Floor((target.transform.position.z) / gridSize) * gridSize + gridSize/2;
        truePos.y = 0;

        return truePos;
    }
}
