using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public float MinX;
    public float MaxX;

    public float MinZ;
    public float MaxZ;

    public Vector3 RandomVector;
    float MaxRange = 2.5f;

    public void GetRandomPos()
    {
        float Random_x = Random.Range(MinX, MaxX);
        float Random_y = Random.Range(MinZ, MaxZ);

        RandomVector = new Vector3(Random_x, 0, Random_y);
    }

    public void SetRandomPos()
    {
        GetRandomPos();
        print(RandomVector);
        while(MaxRange <= RandomVector.magnitude)
        {
            GetRandomPos();
            
        }
        GameObject go =Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), RandomVector, Quaternion.identity);
    }
}
