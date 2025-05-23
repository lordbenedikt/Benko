using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGrid : MonoBehaviour
{
    float minX;
    float minZ;
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
    public bool highlightObstacle;

    public List<GameObject> walls;
    public GameController controller;

    public GameObject[] nodes;
    public SortedDictionary<long, List<GameObject>> pathMap = new SortedDictionary<long, List<GameObject>>();
    GameObject[] Units;

    UnityEvent buildWallEvent = new UnityEvent();
    public GameObject Enemy;

    void Awake() {
        controller = gameObject.GetComponent<GameController>();
        // print(controller);
        minX = transform.position.x - (gridSize*cols/2);
        minZ = transform.position.z - (gridSize*rows/2);
    }
    // Start is called before the first frame update
    void Start()
    {
        // SortedDictionary<float, float> hoho = new SortedDictionary<float, float>();
        // hoho[5f] = 7;
        // print(hoho[5f]);

        buildWallEvent.AddListener(BuildWall);

        walls = new List<GameObject>();
        nodes = new GameObject[cols * rows];

        for (int i = 0; i < nodes.Length; i++)
        {
            Vector3 position = new Vector3(minX + (i % cols + ((cols % 2 == 0) ? 0.5f : 0f)) * gridSize, 0, minZ + (i / cols+ ((cols % 2 == 0) ? 0.5f : 0f)) * gridSize);
            
            GameObject go = Instantiate(nodeObject, position, Quaternion.identity);
            nodes[i] = go;

            Node node = nodes[i].GetComponent<Node>();
            node.transform.parent = gameObject.transform; 
            node.adjacents = new GameObject[4];
        }
        for (int i = 0; i < nodes.Length; i++)
        {
            Node node = nodes[i].GetComponent<Node>();

            if (i / cols == rows - 1)
            {
                // node.isObstacle = true;
                node.adjacents[0] = null;
            }
            else
            {
                node.adjacents[0] = nodes[i + cols];
            }
            if ((i+1) % cols == 0)
            {
                // node.isObstacle = true;
                node.adjacents[1] = null;
            }
            else
            {
                node.adjacents[1] = nodes[i + 1];
            }
            if (i / cols == 0)
            {
                // node.isObstacle = true;
                node.adjacents[2] = null;
            }
            else
            {
                node.adjacents[2] = nodes[i - cols];
            }
            if (i % cols == 0)
            {
                // node.isObstacle = true;
                node.adjacents[3] = null;
            }
            else
            {
                node.adjacents[3] = nodes[i -1];
            }

        }
        BuildWall();
        foreach(Snap snap in FindObjectsOfType<Snap>()) {
            snap.MarkAsObstacle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
            getMousePosOnGround();
        }

        if (Input.GetMouseButtonDown(2))
        {
            // Spawn enemy at mouse position
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            // if (Physics.Raycast(ray, out hit))
            // {
            //     //Vector2 clickPos = new Vector2(hit.point.x, hit.point.z);
            //     Instantiate(Enemy, new Vector3(hit.point.x, 0, hit.point.z), Quaternion.identity);
            // }
        }
    }

    public void BuildWall() {
        walls.Clear();
        for(int i = 0; i<nodes.Length; i++) {
            GameObject n = nodes[i];
            Node node = n.GetComponent<Node>();
            for(int j = 1; j<=2; j++) {
                if(node.adjacents[j] != null) {
                    if(node.isWall && node.adjacents[j].GetComponent<Node>().isWall) {
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
                    if (n.adjacents[i] != null && n.adjacents[i].GetComponent<Node>().isWall) isWall[i] = 1;
                }
                if (((isWall[0] == 1 && isWall[2] == 1) && (isWall[1] == 0 && isWall[3] == 0)) || ((isWall[0] == 0 && isWall[2] == 0) && (isWall[1] == 1 && isWall[3] == 1)))
                {
                    Destroy(n.cornerStone);
                }
                else
                {
                    if (n.isWall && n.cornerStone == null) n.cornerStone = MonoBehaviour.Instantiate(wallObject, new Vector3(go.transform.position.x, 0, go.transform.position.z), Quaternion.identity);
                }
            }
    }

    public float aStar(int s_index, int z_index, List<GameObject> shortestPath)
    {
        shortestPath.Clear();

        GameObject s = nodes[s_index];
        GameObject z = nodes[z_index];
        long a = s_index + (z_index<<10);
        if(pathMap.ContainsKey(a)) {
            shortestPath.AddRange(pathMap[a]);
            return -2;
        }
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
                float minScore = Mathf.Infinity;

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

                shortenPath(shortestPath);
                pathMap[a] = new List<GameObject>(shortestPath);
                // print("KeyMapSize: " + pathMap.Count);

                return D[z.GetInstanceID()];
            }

            Todo.Remove(v);
            foreach(GameObject u in v.GetComponent<Node>().adjacents) {
                if (u==null || u.GetComponent<Node>().isObstacle) continue;
                if(!D.ContainsKey(u.GetInstanceID()) || D[u.GetInstanceID()] > D[v.GetInstanceID()] + (v.gameObject.transform.position-u.transform.position).magnitude) {
                    D[u.GetInstanceID()] = D[v.GetInstanceID()] + (v.gameObject.transform.position-u.transform.position).magnitude;
                    Todo.Add(u);
                }
            }
        }
    shortestPath.Add(s);
    pathMap[a] = new List<GameObject>(shortestPath);
    return -1;
    }

    void shortenPath(List<GameObject> path) {
        int a = 0;
        int b = 2;
        while(b<path.Count) {
            if(!shorten(path[a], path[b], a+1, path)) { 
                a++;
                b = a+2;
            }
        }
    }
    bool shorten(GameObject origin, GameObject target, int removeAt, List<GameObject> path) {
        if(visible(new Vector2(origin.transform.position.x,origin.transform.position.z),new Vector2(target.transform.position.x,target.transform.position.z),0.25f)) {
            path.RemoveAt(removeAt);
            return true;
        }
        return false;
    }
    bool visible(Vector2 ownPos, Vector2 target, float halfThickness) {
        Vector2 v = (target-ownPos).normalized*0.2f;
        Vector2 curPos = ownPos;
        while(Vector2.Distance(curPos,target) > 0.5f) {
            if (controller.GridIndexFromPos(curPos.x,curPos.y) == -1) break;
            if(nodes[controller.GridIndexFromPos(curPos.x,curPos.y)].GetComponent<Node>().isObstacle)
                return false;
            if(halfThickness!=0) {
                if(nodes[controller.GridIndexFromPos(curPos.x+halfThickness,curPos.y)].GetComponent<Node>().isObstacle)
                return false;
                if(nodes[controller.GridIndexFromPos(curPos.x-halfThickness,curPos.y)].GetComponent<Node>().isObstacle)
                return false;
                if(nodes[controller.GridIndexFromPos(curPos.x,curPos.y+halfThickness)].GetComponent<Node>().isObstacle)
                return false;
                if(nodes[controller.GridIndexFromPos(curPos.x,curPos.y-halfThickness)].GetComponent<Node>().isObstacle)
                return false;
            }
            curPos += v;
        }
        return true;
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

    public void getMousePosOnGround() {
        // int layerMask = 1 << 8;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Units = GameObject.FindGameObjectsWithTag("Unit");

        if (Physics.Raycast(ray, out hit))
        {
            // Debug.Log(hit.point);
            
            Vector2 clickPos = new Vector2(hit.point.x,hit.point.z);
            float maxDist = 1;
            GameObject selectedUnit = null;
            foreach(GameObject unit in Units) {
                Vector2 playerPos = new Vector2(unit.transform.position.x, unit.transform.position.z);
                float distance = Vector2.Distance(playerPos, clickPos);
                // print("distance: " + distance);
                if(distance<maxDist) {
                    maxDist = distance;
                    selectedUnit = unit;
                }
            }
            // if click on player unit
            if(selectedUnit!=null) {
                foreach(GameObject p in Units) {

                    p.GetComponent<isSelected>().IsSelected = false;
                    p.GetComponent<Outline>().enabled = false;
                }
                    selectedUnit.GetComponent<isSelected>().IsSelected = true;
                    selectedUnit.GetComponent<Outline>().enabled = true;

                selectedUnit.GetComponent<Outline>().enabled = true;
                controller.SelectionIndicator.transform.position = selectedUnit.transform.position;
                controller.SelectionIndicator.SetActive(true);
                controller.SelectionIndicator.GetComponent<Selection>().player = selectedUnit;
            }
        }

        // if (Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition),layerMask)) {
        //     agent.destination = hit.point;
        // }

        // // Does the ray intersect any objects excluding the player layer
        // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //     Debug.Log("Did Hit");
        // }
        // else
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //     Debug.Log("Did not Hit");
        // }
    }
}
