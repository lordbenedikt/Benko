using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject enemy;
    [HideInInspector]
    public GameObject cornerStone;

    [HideInInspector]
    public AStar aStar;
    [HideInInspector]
    public bool selected = false;
    [HideInInspector]
    public GameObject[] adjacents;
    [HideInInspector]
    public bool isObstacle = false;
    [HideInInspector]
    public bool inPath = false;


    List<GameObject> shortestPath = new List<GameObject>();


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Renderer>() != null)
            gameObject.GetComponent<Renderer>().material.color = isObstacle ? aStar.colorSelected : aStar.colorOriginal;
        if (inPath) gameObject.GetComponent<Renderer>().material.color = aStar.colorPath;
    }
    void OnMouseOver()
    {
        // left button
        if (Input.GetMouseButtonDown(0))
        {
            if (isObstacle)
            {
                isObstacle = false;
                Destroy(cornerStone);
            }
            else
            {
                isObstacle = true;
            }
        }
        // right button
        if (Input.GetMouseButtonDown(1))
        {
            foreach (GameObject go in aStar.nodes)
            {
                go.GetComponent<Node>().inPath = false;
            }
            print(aStar.aStar(gameObject, aStar.nodes[aStar.nodes.Length - 1], shortestPath));
        }
        // middle button
        if (Input.GetMouseButtonDown(2))
        {
            GameObject go = Instantiate(enemy, transform.position,Quaternion.identity);
        }
    }
}
