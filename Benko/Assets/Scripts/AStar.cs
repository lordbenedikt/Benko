using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AStar : MonoBehaviour
{
    public float minX;
    public float minZ;
    public float s;
    public int width;
    public int height;

    public GameObject nodeObject;
    public GameObject wallObject;
    public GameObject wallFillerObject;
    public Material material;
    public Color colorOriginal;
    public Color colorSelected;

    public List<GameObject> walls;

    public GameObject[] nodes;
    Ray ray;
    RaycastHit hit;

    UnityEvent buildWallEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        buildWallEvent.AddListener(buildWall);

        walls = new List<GameObject>();
        nodes = new GameObject[width * height];

        for (int i = 0; i < nodes.Length; i++)
        {
            Vector3 position = new Vector3(minX + (i % width + ((width % 2 == 0) ? 0.5f : 0f)) * s, 0, minZ + (i / width+ ((width % 2 == 0) ? 0.5f : 0f)) * s);
            
            GameObject go = Instantiate(nodeObject, position, Quaternion.identity);
            nodes[i] = go;

            Node node = nodes[i].GetComponent<Node>();
            node.transform.parent = gameObject.transform; 
            node.aStar = this;
            node.nodeObject = nodeObject;
            node.adjacents = new GameObject[4];
        }
        for (int i = 0; i < nodes.Length; i++)
        {
            Node node = nodes[i].GetComponent<Node>();

            if (i / width == 0)
            {
                node.isObstacle = true;
                node.adjacents[0] = null;
            }
            else
            {
                node.adjacents[0] = nodes[i - width];
            }
            if ((i+1) % width == 0)
            {
                node.isObstacle = true;
                node.adjacents[1] = null;
            }
            else
            {
                node.adjacents[1] = nodes[i + 1];
            }
            if (i / width == height - 1)
            {
                node.isObstacle = true;
                node.adjacents[2] = null;
            }
            else
            {
                node.adjacents[2] = nodes[i + width];
            }
            if (i % width == 0)
            {
                node.isObstacle = true;
                node.adjacents[3] = null;
            }
            else
            {
                node.adjacents[3] = nodes[i -1];
            }

        }

        // buildWallEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject wall in walls) {
            Destroy(wall);
        }
        
        buildWallEvent.Invoke();
    }

    void buildWall() {
        walls.Clear();
        for(int i = 0; i<nodes.Length; i++) {
            GameObject n = nodes[i];
            Node node = n.GetComponent<Node>();
            for(int j = 1; j<=2; j++) {
                if(node.adjacents[j] != null) {
                    if(node.isObstacle && node.adjacents[j].GetComponent<Node>().isObstacle) {
                        GameObject go = Instantiate(wallFillerObject, (node.transform.position + node.adjacents[j].transform.position)/2, Quaternion.identity);
                        if(j%2 == 0) go.transform.Rotate(0, 90, 0, Space.Self);
                        walls.Add(go);
                    }
                }
            }
        }
    }

    void aStar(Node s, Node z)
    {

    }
    // algorithm A*(s, z)
    // D sei eine Map von Knoten zu Weglängen
    // D[s] ← 0
    // Todo ← { s }
    // while Todo ist nicht leer
    // v ← Knoten in Todo mit minimalem Score(v) = D[v] + Rest(v)
    // if v = z
    // return D[z] ⇦ Abkürzung
    // en'erne v aus Todo
    // for each u ∈ Neighbours(v)
    // if ( ) or ( )
    // D[u] ← D[v] + Len(v, u)
    // Todo ← Todo ∪ { u }
    // report "kann z von s aus nicht erreichen "



}
