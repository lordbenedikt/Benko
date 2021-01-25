using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int[] iii = new int[5];
    public bool selected = false;
    public GameObject[] adjacents;

    public GameObject nodeObject;
    GameObject cornerStone;
    public AStar aStar;
    public bool isObstacle = false;


    List<Node> shortestPath = new List<Node>();


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Renderer>()!=null)
            gameObject.GetComponent<Renderer>().material.color = isObstacle ? aStar.colorSelected : aStar.colorOriginal;
    }
    void OnMouseDown() {
        if(isObstacle) {
            isObstacle = false;
            Destroy(cornerStone);
        } else {
            isObstacle = true;
        }

        foreach(GameObject go in aStar.nodes) {
            Node n = go.GetComponent<Node>();
            int[] isWall = new int[4];
            for(int i = 0; i<4; i++) {
                if(n.adjacents[i]!=null && n.adjacents[i].GetComponent<Node>().isObstacle) isWall[i] = 1;
            }
            if(((isWall[0]==1 && isWall[2]==1) && (isWall[1]==0 && isWall[3]==0)) || ((isWall[0]==0 && isWall[2]==0) && (isWall[1]==1 && isWall[3]==1))) {
                Destroy(n.cornerStone);
            } else {
                if (n.isObstacle && n.cornerStone==null) n.cornerStone = MonoBehaviour.Instantiate(aStar.wallObject, new Vector3(go.transform.position.x,0,go.transform.position.z), Quaternion.identity);
            }
        }
        print(aStar.aStar(gameObject, aStar.nodes[aStar.nodes.Length-1], shortestPath));
    }
    public GameObject getGameObject() {
        return gameObject;
    }
}
