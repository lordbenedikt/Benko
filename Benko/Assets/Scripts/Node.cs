using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int[] iii = new int[5];
    public float x;
    public float z;
    public bool selected = false;
    public GameObject[] adjacents;

    public GameObject nodeObject;
    public AStar aStar;
    public bool isObstacle = false;

    public Node(AStar aStar, GameObject nodeObject, float x, float z)

    {
        this.x = x;
        this.z = z;
        this.nodeObject = nodeObject;
        this.aStar = aStar;

        gameObject.GetComponent<ObjectVariables>().node = this;
        adjacents = new GameObject[4];
        adjacents[1] = gameObject;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = isObstacle ? aStar.colorSelected : aStar.colorOriginal;
    }
    void OnMouseDown() {

        // print("This: "+"\n");
        isObstacle = !isObstacle;
        print(adjacents.Length);
    }
    public GameObject getGameObject() {
        return gameObject;
    }
}
