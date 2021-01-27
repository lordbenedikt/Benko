using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    GameObject gameController;
    CustomGrid customGrid;

    private void Start()
    {
        gameController = GameObject.Find("GameController");
        customGrid = gameController.GetComponent<CustomGrid>();
    }
    public void markAsObstacle() {
        print(customGrid);
        int gridIndex = customGrid.gridIndexFromPos(transform.position.x, transform.position.z);
        if(gridIndex!=-1)
            customGrid.nodes[gridIndex].GetComponent<Node>().isObstacle = true;
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
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("ObstacleMarker")) {
            if(go==gameObject) continue;
            if(Vector3.Distance(go.transform.position,this.transform.position)<0.5f) {
                DestroyImmediate(gameObject);
                return;
            }
        }
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
