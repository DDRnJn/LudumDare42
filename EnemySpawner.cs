using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Transform[] enemyPrefabs;

    public int minSpawnTime;
    public int maxSpawnTime;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));

    }
	
	public void SpawnEnemy()
    {
        Transform enemyToInstantiate = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyToInstantiate, this.transform.position, this.transform.rotation);
    }
}
