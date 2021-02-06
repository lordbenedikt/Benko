using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node : MonoBehaviour
{
    public GameObject enemy;
    [HideInInspector]
    public GameObject cornerStone;


    [HideInInspector]
    public bool selected = false;
    [HideInInspector]
    public GameObject[] adjacents = null;
    public bool isObstacle = false;
    public bool isWall = false;
    public bool inPath = false;
    public bool isOccupied = false;

    GameController gameController;
    CustomGrid customGrid;


    List<GameObject> shortestPath = new List<GameObject>();

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        customGrid = gameController.gameObject.GetComponent<CustomGrid>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Renderer>() != null)
        {
            Material objMaterial = gameObject.GetComponent<Renderer>().material;
            objMaterial.color = customGrid.colorOriginal;
            if (isObstacle && customGrid.highlightObstacle) objMaterial.color = customGrid.colorSelected;
            if (inPath) objMaterial.color = customGrid.colorPath;
        }
    }
    void OnMouseOver()
    {
        // left button
        if (Input.GetMouseButton(0))
        {
            if (!isOccupied && !isObstacle && !isWall && gameController.UI.ActivateBuildMode && GameObject.Find("Canvas").GetComponent<UI_Manager>().GoldAmount >= 10)
            {
                bool enemyTooClose = false;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < 1.5f)
                        enemyTooClose = true;
                }
                if (!enemyTooClose)
                {
                    // should be optimized
                    customGrid.pathMap.Clear();
                    // foreach (long key in customGrid.pathMap.Keys.ToArray())
                    // {
                    //     if (customGrid.pathMap[key].Contains(gameObject))
                    //     {
                    //         customGrid.pathMap.Remove(key);
                    //     }
                    // }

                    // print("siize "+customGrid.pathMap.Count);
                    GameObject.Find("Canvas").GetComponent<UI_Manager>().AddGold(-10);
                    isWall = true;
                    isObstacle = true;

                    // connect walls
                    foreach (GameObject wall in customGrid.walls)
                    {
                        Destroy(wall);
                    }
                    customGrid.buildWall();
                }
            }
        }
        // right button
        if (Input.GetMouseButton(1))
        {
            if (isWall && gameController.UI.ActivateBuildMode)
            {
                customGrid.pathMap.Clear();
                // foreach(long key in customGrid.pathMap.Keys.ToArray()) {
                //     float pathLength = 0;
                //     List<GameObject> path = customGrid.pathMap[key];
                //     for(int i = 0; i<customGrid.pathMap[key].Count-2;i++) {
                //         pathLength += Vector3.Distance(path[i].gameObject.transform.position, path[i+1].gameObject.transform.position);
                //     }
                // }
                // print("siize "+customGrid.pathMap.Count);
                GameObject.Find("Canvas").GetComponent<UI_Manager>().AddGold(5);
                isWall = false;
                isObstacle = false;
                Destroy(cornerStone);

                // connect walls
                foreach (GameObject wall in customGrid.walls)
                {
                    Destroy(wall);
                }
                customGrid.buildWall();
            }
            else
            {
                foreach (GameObject go in customGrid.nodes)
                {
                    go.GetComponent<Node>().inPath = false;
                }
                // print(customGrid.aStar(gameObject, customGrid.nodes[customGrid.nodes.Length - 1], shortestPath));
            }

        }
        // middle button
        if (Input.GetMouseButtonDown(2))
        {
            // GameObject go = Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
