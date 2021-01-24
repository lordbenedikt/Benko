using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public float minX;
    public float minZ;
    public float s;
    public int width;
    public int height;

    public GameObject nodeObject;
    public Material material;
    public Color colorOriginal;
    public Color colorSelected;

    Node[] nodes;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        nodes = new Node[width * height];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new Node(this, nodeObject, minX + (i % width + ((width % 2 == 0) ? 0.5f : 0f)) * s, minZ + (i / width) * s);
        }
        for (int i = 0; i < nodes.Length; i++)
        {

            if (i / width == 0)
            {
                nodes[i].selected = true;
                nodes[i].adjacents[0] = null;
            }
            else
            {
                nodes[i].adjacents[0] = nodes[i - width];
            }
            if (i % width == 0)
            {
                nodes[i].selected = true;
                nodes[i].adjacents[1] = null;
            }
            else
            {
                nodes[i].adjacents[1] = nodes[i - 1];
            }
            if (i / width == height - 1
            )
            {
                nodes[i].selected = true;
                nodes[i].adjacents[2] = null;
            }
            else
            {
                nodes[i].adjacents[2] = nodes[i + width];
            }
            if (i % width == width - 1)
            {
                nodes[i].selected = true;
                nodes[i].adjacents[3] = null;
            }
            else
            {
                nodes[i].adjacents[3] = nodes[i + 1];
            }

            Vector3 position = new Vector3(nodes[i].x, 0, nodes[i].z);

        }
    }

    // Update is called once per frame
    void Update()
    {
        // ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out hit))
        // {
        //     // if (Input.GetMouseButtonUp(0))
        //     // {
        //     //     print(1);
        //     //     ObjectVariables objVars = hit.collider.gameObject.GetComponent<ObjectVariables>();
        //     //     if (objVars.node.selected)
        //     //         objVars.node.selected = false;
        //     //     else
        //     //         objVars.node.selected = true;
        //     // }
        // }
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].gameObject.GetComponent<Renderer>().material.color = nodes[i].selected ? colorSelected : colorOriginal;
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
