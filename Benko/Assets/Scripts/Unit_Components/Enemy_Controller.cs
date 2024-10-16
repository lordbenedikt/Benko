using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Controller : MonoBehaviour
{
    private GameObject player;
    [Header("Basic Setup")]
    public float speed;
    public bool showPath = false;
    public bool collideWithOtherEnemies;
    public int prefabSetIndex;
    List<GameObject> path = new List<GameObject>();
    GameController controller;
    CustomGrid customGrid;
    GameObject nextNode;
    //Animator enemy_main;
    private bool dead = false;
    //public GameObject gold_coin;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        customGrid = controller.gameObject.GetComponent<CustomGrid>();
        InvokeRepeating("findPath", 0f, 1f);
        //enemy_main = GetComponent<Animator>();
    }

    void findPath()
    {
        if (!dead)
        {
            // don't find path when inside obstacle
            if (customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>().isObstacle) return;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Unit");
            float minDistance = Mathf.Infinity;
            foreach (GameObject ply in players)
            {
                float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(ply.transform.position.x, ply.transform.position.z));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    player = ply;
                }
            }
            if (player == null) return;
            int start = controller.GridIndexFromPos(transform.position.x, transform.position.z);
            int ziel = controller.GridIndexFromPos(player.transform.position.x, player.transform.position.z);
            if (start == -1 || ziel == -1) return;
            customGrid.aStar(start, ziel, path);
            if (path.Count > 1)
            {
                nextNode = path[path.Count - 2].gameObject;
            }
            else
            {
                nextNode = null;
            }
        }
    }

    void Update()
    {
        if (!dead)
        {
            if (gameObject.GetComponent<Health>().Currenthealth <= 0)
            {
                Die();
            }
            Vector3 prevPos3d = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (showPath)
            {
                foreach (GameObject n in customGrid.nodes)
                {
                    n.GetComponent<Node>().inPath = false;
                }
                foreach (GameObject n in path)
                {
                    n.GetComponent<Node>().inPath = true;
                }
            }
            if (player == null) return;
            // if hit Player
            // is only for testing
            if (new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z).magnitude < 0.8)
            {
                player.GetComponent<Health>().Currenthealth -= 2f;
                return;
            }
            if (nextNode != null)
            {
                transform.position += (nextNode.transform.position - transform.position).normalized * speed * Time.deltaTime;
                GetComponent<UnitAnimator>().Run();
                //print("is currently walking");
                if ((nextNode.transform.position - transform.position).magnitude < 0.1)
                {
                    nextNode = null;
                }
            }
            Vector3 face = new Vector3(transform.position.x - prevPos3d.x, transform.position.y - prevPos3d.y, transform.position.z - prevPos3d.z);
            if (face.sqrMagnitude != 0)
            {
                float damping = 20f;
                face.y = 0;
                var targetRotation = Quaternion.LookRotation(face);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
            }

            // Vector3 targetDirection = Player.transform.position;
            // float singleStep = speed * Time.deltaTime;
            // Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            // Debug.DrawRay(transform.position, newDirection, Color.red);
            // // Calculate a rotation a step closer to the target and applies rotation to this object
            // transform.rotation = Quaternion.LookRotation(newDirection);
        }
        float minDist = 1;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            if(Vector3.Distance(transform.position, enemy.transform.position) < minDist) {
                Vector3 diff = enemy.transform.position-transform.position;
                Vector3 correction = -0.5f*(minDist - diff.magnitude)*diff.normalized;
                Vector3 newPos = transform.position + correction;
                if(customGrid.gridIndexFromPos(newPos.x,newPos.z)!=-1 && !customGrid.nodes[customGrid.gridIndexFromPos(newPos.x,newPos.z)].GetComponent<Node>().isObstacle)
                    transform.position = newPos;
                newPos = enemy.transform.position - correction;
                if(customGrid.gridIndexFromPos(newPos.x,newPos.z)!=-1 && !customGrid.nodes[customGrid.gridIndexFromPos(newPos.x,newPos.z)].GetComponent<Node>().isObstacle)
                    enemy.transform.position = newPos;
            }
        }
    }
    public void Die()
    {
        dead = true;
        gameObject.tag = "Untagged";

        GetComponent<UnitAnimator>().Death();

        //Spawn Coin
        //int amount_of_dropped_coins = (int)Random.Range(1,4);
        //for (var i = 0; i < amount_of_dropped_coins; i++)
        //{
        //    float RandomPosX = Random.Range(-1,1);
        //    float RandomPosZ = Random.Range(-1,1);

        //    //Instantiate(gold_coin, new Vector3(transform.position.x + RandomPosX,transform.position.y,transform.position.z + RandomPosZ) + new Vector3(0,1.2f,0), Quaternion.identity);
        //}

        Destroy(gameObject, 0.0f);
    }
}
