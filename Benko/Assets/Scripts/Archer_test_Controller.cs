using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer_test_Controller : MonoBehaviour
{
    private Animator ShootAnim;
    [Header ("Basic Atribudes")]
    public float range;
    public Transform target;
    public float walkSpeed = 10;
    public CharacterController controller;

    [Header("Attack")]
    public float FireRate;
    public float FireCountdwon = 0.0f;
    public float Damage;
    public GameObject Arrow;
    public Transform ArrowStartPoint;
    Animator animator;

    

    private void Awake()
    {
        ShootAnim = GetComponent<Animator>();
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void Update()
    {
        // Vector2 prevPos = new Vector2(transform.position.x,transform.position.z);
        Vector3 prevPos3d = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime*walkSpeed);

        // if(Input.GetKey("a")) {
        //     transform.position = new Vector3(transform.position.x-walkSpeed,transform.position.y,transform.position.z);
        // }
        // if(Input.GetKey("d")) {
        //     transform.position = new Vector3(transform.position.x+walkSpeed,transform.position.y,transform.position.z);
        // }
        // if(Input.GetKey("w")) {
        //     transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+walkSpeed);
        // }
        // if(Input.GetKey("s")) {
        //     transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z-walkSpeed);
        // }

        Vector3 face = new Vector3(transform.position.x-prevPos3d.x,transform.position.y-prevPos3d.y,transform.position.z-prevPos3d.z);
        
        if(face.sqrMagnitude != 0) {
            ShootAnim.SetBool("Running", true);
            float damping = 20f;
    
            face.y = 0;
            var targetRotation = Quaternion.LookRotation(face);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping); 
        } else {
            ShootAnim.SetBool("Running", false);
        }

        if(target == null)
        {
            ShootAnim.SetBool("Shoot", false);  
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (FireCountdwon <= 1)
        {
            Shoot();
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
        ShootAnim.SetBool("Shoot", true);
        // Debug.Log("shoot");
        GameObject go = (GameObject)Instantiate(Arrow, ArrowStartPoint.position, ArrowStartPoint.rotation);
        ArrowController ArrowScript = go.GetComponent<ArrowController>();
        //Debug.Log(ArrowScript.speed);

        if(ArrowScript != null)
        {
            ArrowScript.Seek(target);
        }
    }

    void OnDrawGizmosSelected(){
        //nur zur Übersicht/Darstellung
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    
    }

    
}
