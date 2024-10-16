using UnityEngine;

public class InfiniteEnemySpawner : MonoBehaviour
{
    public GameObject enemy_prefab;
    public float MaxRange = 2.5f;
    public float SpawnInterval = 5f;

    private Vector3 RandomVector;
    private float nextspawntime;

    private void Update() {
        if (Time.time >= nextspawntime){
            SpawnEnemy();
            nextspawntime = Time.time + SpawnInterval;
        }
    }
    public void GetRandomPos()
    {
        Vector2 displacement = Random.insideUnitCircle * MaxRange;
        RandomVector = new Vector3(displacement.x, 0, displacement.y);
    }
    public void SpawnEnemy()
    {
        GetRandomPos();
        if (MaxRange <= RandomVector.magnitude)
        {
            GameObject go = Instantiate(enemy_prefab, RandomVector, Quaternion.identity);
        }else{
            Debug.Log("eig nicht gespawnt");
        }
    }


    
}
