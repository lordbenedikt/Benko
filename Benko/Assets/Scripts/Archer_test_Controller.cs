using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer_test_Controller : MonoBehaviour
{
    private Transform target;

    [Header ("Basic Setup")]
    public float range;
    public float walkSpeed;
    public float FireRate;
    public float FireCountdwon = 0.0f;
    public float Damage;

    [Header("Unresponsable")]
    public GameObject Arrow;
    public Transform ArrowStartPoint;
    public bool selected;
    Animator archer_anim;
    GameController gameController;
    public GameObject DiePX;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 1f, 0.5f); //0f, 0.01f
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        archer_anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(gameObject.GetComponent<Health>().Currenthealth <= 0){
            Die();
        }
        Vector3 prevPos3d = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        Vector3 move = new Vector3(0,0,0);
        if(selected) { 
            if(Input.GetKey("a")) {
                move.x -= walkSpeed * Time.deltaTime; 
                archer_anim.SetInteger("current_pos", 1);   //Run    
            }
            if(Input.GetKey("d")) {
                move.x = walkSpeed* Time.deltaTime;
                archer_anim.SetInteger("current_pos", 1);   //Run    
            }
            if(Input.GetKey("w")) {
                move.z = walkSpeed* Time.deltaTime;   
                archer_anim.SetInteger("current_pos", 1);   //Run       
            }
            if(Input.GetKey("s")) {
                move.z = -walkSpeed* Time.deltaTime;     
                archer_anim.SetInteger("current_pos", 1);   //Run      
            }
        }
        Vector3 nextPos = transform.position;
        int posIndex = gameController.gridIndexFromPos(nextPos.x+move.x, nextPos.z);
        if(posIndex != -1 && posIndex<gameController.gameObject.GetComponent<CustomGrid>().nodes.Length) {
            GameObject currentNode = gameController.gameObject.GetComponent<CustomGrid>().nodes[posIndex];
            if (!currentNode.GetComponent<Node>().isObstacle) {
                nextPos.x += move.x;
            }
        }
        posIndex = gameController.gridIndexFromPos(nextPos.x, nextPos.z+move.z);
        if(posIndex != -1 && posIndex<gameController.gameObject.GetComponent<CustomGrid>().nodes.Length) {
            GameObject currentNode = gameController.gameObject.GetComponent<CustomGrid>().nodes[posIndex];
            if (!currentNode.GetComponent<Node>().isObstacle) {
                nextPos.z += move.z;
            }
        }
        transform.position = nextPos;
        Vector3 face = new Vector3(transform.position.x-prevPos3d.x,transform.position.y-prevPos3d.y,transform.position.z-prevPos3d.z);
        if(face.sqrMagnitude != 0) {
            float damping = 20f;
            face.y = 0;
            var targetRotation = Quaternion.LookRotation(face);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping); 
        } else {
        }
        if(target == null && Input.GetKey("a") == false && Input.GetKey("s") == false && Input.GetKey("d") == false && Input.GetKey("w") == false)
        {
            archer_anim.SetInteger("current_pos", 0); //Idle Anim
        }
        if(target == null)
        {
            return;
        }
        if(Input.GetKey("a") == false && Input.GetKey("s") == false && Input.GetKey("d") == false && Input.GetKey("w") == false){
            archer_anim.SetInteger("current_pos", 2); //Shoot Anim
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        if (FireCountdwon <= 1)
        {
            FireCountdwon = 1 / FireRate;
        }
        FireCountdwon -= Time.deltaTime;
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float ShortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(DistanceToEnemy < ShortestDistance)
            {
                ShortestDistance = DistanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && ShortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    public void Shoot()
    {
        archer_anim.SetInteger("current_pos", 2); //Shoot Anim
        GameObject go = Instantiate(Arrow, ArrowStartPoint.position, ArrowStartPoint.rotation);
        ArrowController ArrowScript = go.GetComponent<ArrowController>();
        if(ArrowScript != null)
        {
            ArrowScript.Seek(target);
        }
    }

    public void Die(){
        archer_anim.SetInteger("current_pos", 3); //Dead Anim
        GameObject go = Instantiate(DiePX, new Vector3(transform.position.x,transform.position.y+0.8f,transform.position.z), Quaternion.identity); //instanciate Die Particle
        Destroy(go,1.0f);
        //print("Archer has died");
        gameObject.tag = "Untagged";
        selected = false;
        Destroy(gameObject, 5.0f);
    }

    

        

    void OnDrawGizmosSelected(){
        //nur zur Übersicht/Darstellung
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    
    }

    
}
