using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer_Controller : MonoBehaviour
{
    private Transform target;
    public GameObject Arrow;
    public Transform ArrowStartPoint;
    private isSelected IsSelected;
    GameController gameController;
    public GameObject DiePX;
    private bool isDead;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 1f, 0.5f); //0f, 0.01f
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        isDead = false;
        IsSelected = GetComponent<isSelected>();
    }
    void Update()
    {
        if(!isDead){
            if(gameObject.GetComponent<Health>().Currenthealth <= 0){
                Die();
            }
            Vector3 prevPos3d = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            Vector3 move = new Vector3(0,0,0);
            if(IsSelected.IsSelected) { 
                if(Input.GetKey("a")) {
                    move.x -= GetComponent<UnitAttributes>().walkspeed * Time.deltaTime; 
                    GetComponent<UnitAnimator>().Run();
                }
                if(Input.GetKey("d")) {
                    move.x = GetComponent<UnitAttributes>().walkspeed* Time.deltaTime;
                    GetComponent<UnitAnimator>().Run();   
                }
                if(Input.GetKey("w")) {
                    move.z = GetComponent<UnitAttributes>().walkspeed* Time.deltaTime;   
                    GetComponent<UnitAnimator>().Run();      
                }
                if(Input.GetKey("s")) {
                    move.z = -GetComponent<UnitAttributes>().walkspeed* Time.deltaTime;     
                    GetComponent<UnitAnimator>().Run();    
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
            if(target == null && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.W) == false)
            {
                GetComponent<UnitAnimator>().Idle();   
            }
            if(target == null) return;
            if(Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.W) == false){
                GetComponent<UnitAnimator>().Attack();
                
                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                //Shoot();
                //print("shoot");
            }
            GetComponent<UnitAttributes>().firecountdwon -= Time.deltaTime;
            if (GetComponent<UnitAttributes>().firecountdwon <= 1)
            {
                GetComponent<UnitAttributes>().firecountdwon = 1 / GetComponent<UnitAttributes>().firerate;
                //print("Shoot");
                //Shoot();
            }
        }
    }
    void UpdateTarget()
    {
        if(!isDead){
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
            if(nearestEnemy != null && ShortestDistance <= GetComponent<UnitAttributes>().attackrange)
            {
                target = nearestEnemy.transform;
            }
            else target = null;    
        }
    }
    public void Shoot()
    {
        if(!isDead){
            GameObject go = Instantiate(Arrow, ArrowStartPoint.position, ArrowStartPoint.rotation);
            //print(go.transform.name);
            int damage = (int)GetComponent<UnitAttributes>().damage;
            go.GetComponent<ArrowController>().Seek(target, damage);
        }
    }
    public void Die(){
        GetComponent<UnitAnimator>().Death();
        GameObject go = Instantiate(DiePX, new Vector3(transform.position.x,transform.position.y+0.8f,transform.position.z), Quaternion.identity); //instanciate Die Particle
        Destroy(go,1.0f);
        gameObject.tag = "Untagged";
        IsSelected.IsSelected = false;
        isDead = true;
        Destroy(gameObject, 2.5f);
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetComponent<UnitAttributes>().attackrange);
    }
}
