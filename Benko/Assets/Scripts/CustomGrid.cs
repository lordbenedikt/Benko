using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGrid : MonoBehaviour
{
    public float minX;
    public float minZ;
    public float gridSize;
    public int cols;
    public int rows;

    public GameObject nodeObject;
    public GameObject wallObject;
    public GameObject wallFillerObject;
    public Material material;
    public Color colorOriginal;
    public Color colorSelected;
    public Color colorPath;

    public List<GameObject> walls;
    public GameController controller;

    public GameObject[] nodes;
    Ray ray;
    RaycastHit hit;

    UnityEvent buildWallEvent = new UnityEvent();

    void Awake() {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // SortedDictionary<float, float> hoho = new SortedDictionary<float, float>();
        // hoho[5f] = 7;
        // print(hoho[5f]);


        buildWallEvent.AddListener(buildWall);

        walls = new List<GameObject>();
        nodes = new GameObject[cols * rows];

        for (int i = 0; i < nodes.Length; i++)
        {
            Vector3 position = new Vector3(minX + (i % cols + ((cols % 2 == 0) ? 0.5f : 0f)) * gridSize, 0, minZ + (i / cols+ ((cols % 2 == 0) ? 0.5f : 0f)) * gridSize);
            
            GameObject go = Instantiate(nodeObject, position, Quaternion.identity);
            nodes[i] = go;

            Node node = nodes[i].GetComponent<Node>();
            node.transform.parent = gameObject.transform; 
            node.customGrid = this;
            node.adjacents = new GameObject[4];
        }
        for (int i = 0; i < nodes.Length; i++)
        {
            Node node = nodes[i].GetComponent<Node>();

            if (i / cols == 0)
            {
                node.isObstacle = true;
                node.adjacents[0] = null;
            }
            else
            {
                node.adjacents[0] = nodes[i - cols];
            }
            if ((i+1) % cols == 0)
            {
                node.isObstacle = true;
                node.adjacents[1] = null;
            }
            else
            {
                node.adjacents[1] = nodes[i + 1];
            }
            if (i / cols == rows - 1)
            {
                node.isObstacle = true;
                node.adjacents[2] = null;
            }
            else
            {
                node.adjacents[2] = nodes[i + cols];
            }
            if (i % cols == 0)
            {
                node.isObstacle = true;
                node.adjacents[3] = null;
            }
            else
            {
                node.adjacents[3] = nodes[i -1];
            }

        }

        buildWall();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject wall in walls) {
            Destroy(wall);
        }
        
        buildWall();
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
        foreach (GameObject go in nodes)
            {
                Node n = go.GetComponent<Node>();
                int[] isWall = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    if (n.adjacents[i] != null && n.adjacents[i].GetComponent<Node>().isObstacle) isWall[i] = 1;
                }
                if (((isWall[0] == 1 && isWall[2] == 1) && (isWall[1] == 0 && isWall[3] == 0)) || ((isWall[0] == 0 && isWall[2] == 0) && (isWall[1] == 1 && isWall[3] == 1)))
                {
                    Destroy(n.cornerStone);
                }
                else
                {
                    if (n.isObstacle && n.cornerStone == null) n.cornerStone = MonoBehaviour.Instantiate(wallObject, new Vector3(go.transform.position.x, 0, go.transform.position.z), Quaternion.identity);
                }
            }
    }

    public float aStar(GameObject s, GameObject z, List<GameObject> shortestPath)
    {
        shortestPath.Clear();
        if(s==z) {
            shortestPath.Add(z);
            return 0;
        }
        SortedDictionary<int, float> D = new SortedDictionary<int, float>();
        D[s.GetInstanceID()] = 0;
        List<GameObject> Todo = new List<GameObject>();
        Todo.Add(s);

        while(Todo.Count > 0) {
            GameObject v = minimalScore(D, Todo, z);

            // if found path 
            if(v==z) {
                shortestPath.Add(z);
                GameObject advancedNode = z;
                GameObject bestChoice = null;
                float minScore = 10000000;

                do {
                    // print(advancedNode);
                    foreach(GameObject go in advancedNode.GetComponent<Node>().adjacents) {
                        if (go==null || !D.ContainsKey(go.GetInstanceID())) continue;
                        if(D[go.GetInstanceID()]<minScore)
                            bestChoice = go;
                        minScore = D[bestChoice.GetInstanceID()];
                    }
                    // if(bestChoice==null) break;
                    advancedNode = bestChoice;
                    shortestPath.Add(advancedNode);
                } while(advancedNode != s);

                // print(shortestPath.Count);
                foreach(GameObject n in shortestPath) {
                    // n.GetComponent<Node>().inPath = true;
                }
                return D[z.GetInstanceID()];
            }

            Todo.Remove(v);
            foreach(GameObject u in v.GetComponent<Node>().adjacents) {
                if (u==null || u.GetComponent<Node>().isObstacle) continue;
                if(!D.ContainsKey(u.GetInstanceID()) || D[u.GetInstanceID()] > D[v.GetInstanceID()] + (v.gameObject.transform.position-u.transform.position).magnitude) {
                    if(D.ContainsKey(u.GetInstanceID())) {
                        print("found shorter path");
                    }
                    D[u.GetInstanceID()] = D[v.GetInstanceID()] + (v.gameObject.transform.position-u.transform.position).magnitude;
                    Todo.Add(u);
                }
            }
        }
    return -1;
    }

    GameObject minimalScore(SortedDictionary<int, float> D, List<GameObject> Todo, GameObject z) {
        GameObject res = Todo[0];
        float minScore = nodeScore(D[Todo[0].GetInstanceID()], res, z);
        for(int i = 1; i<Todo.Count; i++) {    
            float score = nodeScore(D[Todo[i].GetInstanceID()], Todo[i], z);
            if(score<minScore) {
                res = Todo[i];
                minScore = score;
            }
        }

        return res;
    }
    float nodeScore(float currentScore, GameObject n, GameObject z) {
        if(n==null || z==null) return -1;
        float distNZ = (z.transform.position-n.transform.position).magnitude;
        float res = currentScore + distNZ;
        return res;
    }

    public int gridIndexFromPos(float x, float z) {
        if(x<minX || z<minZ) return -1;
        if(x>minX+gridSize*cols || z>minZ+gridSize*rows) return -1;
        int _col = (int)Mathf.Floor((x + cols*gridSize/2)/gridSize);
        int _row = (int)Mathf.Floor((z + rows*gridSize/2)/gridSize);
        // print("col: "+_col+" row: "+_row+"\n");
        return cols*_row + _col;
    }
}
