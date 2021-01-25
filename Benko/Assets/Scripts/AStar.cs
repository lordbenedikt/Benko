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
        // SortedDictionary<float, float> hoho = new SortedDictionary<float, float>();
        // hoho[5f] = 7;
        // print(hoho[5f]);


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

    public class Tracker {
        public Tracker prevNode;
        public GameObject node;
        // public int ID;
        // public static int serialNumber = 0;
        public Tracker(Tracker prevNode, GameObject node) {
            this.prevNode = prevNode;
            this.node = node;
            // ID = serialNumber;
            // serialNumber++;
        }
    }

    public float aStar(GameObject s, GameObject z, List<Node> path)
    {
        SortedDictionary<int, float> D = new SortedDictionary<int, float>();
        D[s.GetInstanceID()] = 0;

        List<Tracker> path = new List<Tracker>();
        List<GameObject> Todo = new List<GameObject>();
        Todo.Add(s);
        // path.Add(new Tracker(null, s));
        while(Todo.Count > 0) {
            GameObject v = minimalScore(D, Todo, z);
            if(v==z) return D[z.GetInstanceID()];
            Todo.Remove(v);
            foreach(GameObject u in v.GetComponent<Node>().adjacents) {
                if (u==null || u.GetComponent<Node>().isObstacle) continue;
                Debug.Log(D.ContainsKey(u.GetInstanceID()));
                if(!D.ContainsKey(u.GetInstanceID()) || D[u.GetInstanceID()] > D[v.GetInstanceID()] + (v.gameObject.transform.position-u.transform.position).magnitude) {
                    D[u.GetInstanceID()] = D[v.GetInstanceID()] + (v.gameObject.transform.position-u.transform.position).magnitude;
                    Todo.Add(u);
                    // path.Add(new Tracker(v, u));
                }
            }
        }
    return -1;
    }

    GameObject minimalScore(SortedDictionary<int, float> D, List<GameObject> Todo, GameObject z) {
        GameObject res = Todo[0];
        
        foreach(GameObject n in Todo) {
            float minScore = nodeScore(D[n.GetInstanceID()], res, z);
            for(int i = 0; i<Todo.Count; i++) {
                float score = nodeScore(D[n.GetInstanceID()], Todo[i], z);
                if(score<minScore) {
                    res = Todo[i];
                    minScore = score;
                }
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
}
