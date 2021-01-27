using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public GameController gameController;
    CustomGrid customGrid;

    private void Start()
    {
        customGrid = GameObject.Find("GameController").GetComponent<CustomGrid>();
        customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>().isObstacle = true;
    }
    private void Update()
    {
        print(transform.position.x + "" + transform.position.z);
        int gridIndex = customGrid.gridIndexFromPos(transform.position.x, transform.position.z);
        print(gridIndex);
        customGrid.nodes[31].GetComponent<Node>().isObstacle = true;
    }
    private void OnDrawGizmos()
    {
        SnapToGrid();
    }
    private void SnapToGrid()
    {
        var position = new Vector3(
            Mathf.RoundToInt(gameObject.transform.position.x - 0.5f) + 0.5f,
            Mathf.RoundToInt(gameObject.transform.position.y - 0.5f) + 0.5f,
            Mathf.RoundToInt(gameObject.transform.position.z - 0.5f) + 0.5f
            );

        transform.position = position;
    }
    private void SnapToGrid2()
    {
        var position = new Vector3(
            Mathf.RoundToInt(gameObject.transform.position.x / customGrid.gridSize) * customGrid.gridSize + customGrid.gridSize*0.5f,
            Mathf.RoundToInt(gameObject.transform.position.y / customGrid.gridSize) * customGrid.gridSize + customGrid.gridSize * 0.5f,
            Mathf.RoundToInt(gameObject.transform.position.z / customGrid.gridSize) * customGrid.gridSize + customGrid.gridSize * 0.5f
        );

        gameObject.transform.position = position;
    }
}
