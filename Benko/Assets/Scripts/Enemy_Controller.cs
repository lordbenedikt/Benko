using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Controller : MonoBehaviour
{
    private GameObject player;
    public float speed;
    List<GameObject> path = new List<GameObject>();
    GameController controller;
    CustomGrid customGrid;
    GameObject nextNode;

    // private Transform target;
    

    void Start() {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        customGrid = controller.gameObject.GetComponent<CustomGrid>();
        InvokeRepeating("findPath", 0f, 0.2f);
    }
    
    void findPath() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        foreach(GameObject ply in players) {
            float distance = Vector2.Distance(new Vector2(transform.position.x,transform.position.z),new Vector2(ply.transform.position.x,ply.transform.position.z));
            if(distance<minDistance) {
                minDistance = distance;
                player = ply;
            }
        }

        if(player==null) return;
        int start = controller.gridIndexFromPos(transform.position.x,transform.position.z);
        int ziel = controller.gridIndexFromPos(player.transform.position.x,player.transform.position.z);
        if(start==-1 || ziel==-1) return;
        customGrid.aStar(customGrid.nodes[start], customGrid.nodes[ziel], path);
        if(path.Count>1) {
            nextNode = path[path.Count-2].gameObject;
        }
    }
    void Update()
    {
        if (player==null) return;
        // if hit Player
        if(new Vector2(transform.position.x-player.transform.position.x,transform.position.z-player.transform.position.z).magnitude < 0.8) {
            
            
            Destroy(gameObject);
            player.GetComponent<Health>().Currenthealth -= 40;
            return;
        }
        // findPath();
        // print(nextNode);
        if(nextNode != null) {
            // print("find path\n");
            transform.position += (nextNode.transform.position-transform.position).normalized*speed;
            if((nextNode.transform.position-transform.position).magnitude<0.1) {
                nextNode = null;
            }
        }

        // Vector3 targetDirection = Player.transform.position;
        // float singleStep = speed * Time.deltaTime;
        // Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        // Debug.DrawRay(transform.position, newDirection, Color.red);
        // // Calculate a rotation a step closer to the target and applies rotation to this object
        // transform.rotation = Quaternion.LookRotation(newDirection);
    }
}

