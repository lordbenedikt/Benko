using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy_prefab;
    public float MaxRange = 2.5f;
    private float MinX;
    private float MaxX;
    private float MinZ;
    private float MaxZ;
    private Vector3 RandomVector;
    
    private void Start()
    {
        MinX = transform.position.x - MaxRange;
        MaxX = transform.position.x + MaxRange;
        MinZ = transform.position.z - MaxRange;
        MaxZ = transform.position.z + MaxRange;
        InvokeRepeating("OutputTime", 2f, 3f);  //1s delay, repeat every 1s
    }
    public void GetRandomPos()
    {
        float Random_x = Random.Range(MinX, MaxX);
        float Random_z = Random.Range(MinZ, MaxZ);
        RandomVector = new Vector3(Random_x, 0, Random_z);
    }

    public void SpawnEnemy()
    {
        GetRandomPos();
        if (MaxRange <= RandomVector.magnitude)
        {
            //Debug.Log("Gespawnt");
            //print("Gespawnt");
        }
        //print("eig nicht gespawnt");
        //Debug.Log("eig nicht gespawnt");
        GameObject go =Instantiate(enemy_prefab, RandomVector, Quaternion.identity);


    }

    
    void OutputTime()
    {
        SpawnEnemy();
    }
}
