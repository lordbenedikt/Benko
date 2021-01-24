using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public float x;
    public float z;
    public bool selected = false;
    public Node[] adjacents = new Node[4];

    public GameObject nodeObject;
    public GameObject gameObject;
    AStar aStar;

    public Node(AStar aStar, GameObject nodeObject, float x, float z)

    {
        this.x = x;
        this.z = z;
        this.nodeObject = nodeObject;
        this.aStar = aStar;

        gameObject = MonoBehaviour.Instantiate(nodeObject, new Vector3(x,0,z), Quaternion.identity);
        gameObject.GetComponent<ObjectVariables>().node = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown() {
        selected = !selected;
    }
    public GameObject getGameObject() {
        return gameObject;
    }
}
