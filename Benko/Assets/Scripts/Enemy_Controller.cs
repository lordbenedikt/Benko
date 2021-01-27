using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Controller : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public bool showPath = false;
    List<GameObject> path = new List<GameObject>();
    GameController controller;
    CustomGrid customGrid;
    GameObject nextNode;

   
    public Animator archer_anim;

    // private Transform target;
    

    void Start() {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        customGrid = controller.gameObject.GetComponent<CustomGrid>();
        InvokeRepeating("findPath", 0f, 0.1f);
         
        archer_anim = GetComponent<Animator>();
        
        archer_anim.SetInteger("Skeleton_Anim", 2);   //Death
    }
    
    void findPath() {
        // don't find path when inside obstacle
        if(customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x,transform.position.z)].GetComponent<Node>().isObstacle) return;

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
        customGrid.aStar(start, ziel, path);

        if(path.Count>1) {
            nextNode = path[path.Count-2].gameObject;
        }
    }

    void Update()
    {
        Vector3 prevPos3d = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        
        if(showPath) {
            foreach(GameObject n in customGrid.nodes) {
                n.GetComponent<Node>().inPath = false;
            }
            foreach(GameObject n in path) {
                n.GetComponent<Node>().inPath = true;
            }
        }

        if (player==null) return;
        // if hit Player
        if(new Vector2(transform.position.x-player.transform.position.x,transform.position.z-player.transform.position.z).magnitude < 0.8) {
            
            archer_anim.SetInteger("Skeleton_Anim", 2);   //Death  
            print("Death");  
            Destroy(gameObject,5.0f);
            gameObject.tag = "Untagged";
            player.GetComponent<Health>().Currenthealth -= 40;
            return;
        }
        // findPath();
        // print(nextNode);
        if(nextNode != null) {
            // print("find path\n");
            transform.position += (nextNode.transform.position-transform.position).normalized*speed;
            archer_anim.SetInteger("Skeleton_Anim", 0);   //Walking
            print("is currently walking");  
            if((nextNode.transform.position-transform.position).magnitude<0.1) {
                nextNode = null;
            }
        }

        Vector3 face = new Vector3(transform.position.x-prevPos3d.x,transform.position.y-prevPos3d.y,transform.position.z-prevPos3d.z);
        if(face.sqrMagnitude != 0) {
            //archer_anim.SetBool("Running", true); //Running auf true
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
    public void Die(){
        print("Die Method");  
        Destroy(gameObject,5.0f);
    }
}

