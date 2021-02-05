using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    GameObject gameController;
    CustomGrid customGrid;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        customGrid = gameController.GetComponent<CustomGrid>();
    }
    void Update()
    {
        // print(transform.position.x + "" + transform.position.z);
        int gridIndex = customGrid.gridIndexFromPos(transform.position.x, transform.position.z);
        // print(gridIndex);
        if (gridIndex >= 0 && gridIndex < customGrid.nodes.Length)
            customGrid.nodes[gridIndex].GetComponent<Node>().isObstacle = true;
    }
}
