using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Controller : MonoBehaviour
{
    private Transform target;
    public GameObject energy_ball;
    public Transform energy_start_pos;
    GameController gameController;
    public GameObject DiePX;
    private bool isDead;
    private isSelected IsSelected;
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
            if(target == null && Input.GetKey("a") == false && Input.GetKey("s") == false && Input.GetKey("d") == false && Input.GetKey("w") == false)
            {
                GetComponent<UnitAnimator>().Idle();
            }
            if(target == null)
            {
                return;
            }
            if(Input.GetKey("a") == false && Input.GetKey("s") == false && Input.GetKey("d") == false && Input.GetKey("w") == false){
                GetComponent<UnitAnimator>().Attack();
                //Shoot();
                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }

            GetComponent<UnitAttributes>().firecountdwon -= Time.deltaTime;
            if (GetComponent<UnitAttributes>().firecountdwon <= 1)
            {
                GetComponent<UnitAttributes>().firecountdwon = 1 / GetComponent<UnitAttributes>().firerate;
                Shoot();
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
            GetComponent<UnitAnimator>().Attack();
            GameObject go = Instantiate(energy_ball, energy_start_pos.position, energy_start_pos.rotation);
            int damage = (int)GetComponent<UnitAttributes>().damage;
            go.GetComponent<EnergyBallController>().Seek(target, damage);
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
