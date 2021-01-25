using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_test_Controller : MonoBehaviour
{
    private Animator ShootAnim;
    [Header ("Atribudes")]
    
    public float range;
    public Transform target;

    [Header("Attack")]
    public float FireRate;
    public float FireCountdwon = 0.0f;
    public GameObject Arrow;
    public Transform ArrowStartPoint;

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
        Debug.Log("shoot");
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
