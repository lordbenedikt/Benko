using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold_Controller : MonoBehaviour
{
    public float rotation_speed;
    public float range;
    public float speed;
    public GameObject target;

    void Update()
    {
        transform.Rotate(0,rotation_speed * Time.deltaTime,0);


        GameObject[] Archers = GameObject.FindGameObjectsWithTag("Unit");
        float ShortestDistance = Mathf.Infinity;
        GameObject nearestArcher = null;
        foreach(GameObject Archer in Archers)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, Archer.transform.position + new Vector3(0,1.5f,0));
            if(DistanceToEnemy < ShortestDistance)
            {
                ShortestDistance = DistanceToEnemy;
                nearestArcher = Archer;
            }
        }
        if(nearestArcher != null && ShortestDistance <= range)
        {
            target = nearestArcher;
            //print(target.name);
            //transform.position()
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0,1.5f,0), step);
            if(nearestArcher != null && ShortestDistance <= 0.5f){
                //print("close to pickup");
                GameObject.Find("Canvas").GetComponent<UI_Manager>().AddGold(10);
                Destroy(gameObject);
            }
            //transform.Translate(target.transform.position * Time.deltaTime);
        }
        else
        {
            target = null;
        }        
        
        // if distance vom spieler ist geringer als 3 dann move towards it

        
    
    }

    void OnDrawGizmosSelected(){
        //nur zur Übersicht/Darstellung
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    
    }
}
