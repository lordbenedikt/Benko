using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int[] iii = new int[5];
    public bool selected = false;
    public GameObject[] adjacents;

    public GameObject nodeObject;
    public AStar aStar;
    public bool isObstacle = false;

    // public Node(AStar aStar, GameObject nodeObject, float x, float z)

    // {
    //     this.x = x;
    //     this.z = z;
    //     this.nodeObject = nodeObject;
    //     this.aStar = aStar;

    //     gameObject.GetComponent<ObjectVariables>().node = this;
    //     adjacents = new GameObject[4];
    //     adjacents[1] = gameObject;
    // }

    void Start()
    {
        // adjacents[0] = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = isObstacle ? aStar.colorSelected : aStar.colorOriginal;
    }
    void OnMouseDown() {

        GameObject newWall = MonoBehaviour.Instantiate(aStar.wallObject, new Vector3(gameObject.transform.position.x,0,gameObject.transform.position.z), Quaternion.identity);
        aStar.walls.Add(newWall);

        // print("This: "+"\n");
        isObstacle = !isObstacle;
        foreach (GameObject go in adjacents) 
        {
            if (go != null)
                go.GetComponent<Node>().isObstacle = !go.GetComponent<Node>().isObstacle;
        }
    }
    public GameObject getGameObject() {
        return gameObject;
    }
}
