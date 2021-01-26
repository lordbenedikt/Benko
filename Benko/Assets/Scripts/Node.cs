using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject enemy;
    [HideInInspector]
    public GameObject cornerStone;


    [HideInInspector]
    public bool selected = false;
    [HideInInspector]
    public GameObject[] adjacents;
    [HideInInspector]
    public bool isObstacle = false;
    [HideInInspector]
    public bool inPath = false;

    GameController gameController;
    CustomGrid customGrid;


    List<GameObject> shortestPath = new List<GameObject>();

    void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        customGrid = gameController.gameObject.GetComponent<CustomGrid>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Renderer>() != null) {
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
            if (!isObstacle && gameController.UI.ActivateBuildMode && GameObject.Find("Canvas").GetComponent<UI_Manager>().GoldAmount >= 10)
            {
                GameObject.Find("Canvas").GetComponent<UI_Manager>().AddGold(-10);
                isObstacle = true;
            }
        }
        // right button
        if (Input.GetMouseButton(1))
        {
            if (isObstacle && gameController.UI.ActivateBuildMode)
            {
                GameObject.Find("Canvas").GetComponent<UI_Manager>().AddGold(1);
                isObstacle = false;
                Destroy(cornerStone);
            } else {
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
            GameObject go = Instantiate(enemy, transform.position,Quaternion.identity);
        }
    }
}
