using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData
{
    public int frameCount;
    public int enemyPrefab;
    public Vector3 position;
    public EnemySpawnData(int frameCount, int enemyPrefab, Vector3 position) {
        this.frameCount = frameCount;
        this.enemyPrefab = enemyPrefab;
        this.position = position;
    }
}
