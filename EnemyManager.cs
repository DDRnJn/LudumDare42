
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    public EnemySpawner[] spawnerPrefabs;

    [SerializeField]
    private List<EnemySpawner> enemySpawners;

    private List<Transform> mountains;

    public int spawnerNum;
    //public int spawnTime;
    public int minSpawnTime;
    public int maxSpawnTime;
    private Transform groundTransform; // For creating spawn points on the ground and placing spawners
    private List<Vector3> spawnPoints;

    public float minDistance; //minimum distance a spawner must be from the base 

    // Use this for initialization
    void Start () {
        this.groundTransform = GameObject.FindWithTag("Ground").transform;
        this.spawnPoints = this.getSpawnPoints();
        this.enemySpawners = this.createSpawners();

        InvokeRepeating("SpawnFromSpawner", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));
	}
	
    private List<Vector3> getSpawnPoints()
    {
        List<Vector3> spawnPoints = new List<Vector3>();
        Mesh groundMesh = groundTransform.gameObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = groundMesh.bounds;
        float minX = groundTransform.position.x - groundTransform.localScale.x * bounds.size.x * 0.5f;
        float minZ = groundTransform.position.z - groundTransform.localScale.z * bounds.size.z * 0.5f;
        for (int i = 0; i < this.spawnerNum; i++)
        {
            float newX = Random.Range(minX, -minX);
            float newZ = Random.Range(minZ, -minZ);
            // Making sure the spawn points aren't too close to the base - if they are, just move them to the edge
            if (Mathf.Abs(newX) < minDistance)
            {
                newX = minX;
            }
            if (Mathf.Abs(newZ) < minDistance)
            {
                newZ = minZ;
            }
            Vector3 newSpawnPoint = new Vector3(newX,
                                                0f,
                                                newZ);
            spawnPoints.Add(newSpawnPoint);
        }

        return spawnPoints;
    }

    public void destroySpawners()
    {
        for(int i = 0; i < this.enemySpawners.Count; i++)
        {
            Destroy(this.enemySpawners[i].gameObject);
        }
        this.enemySpawners = null;
    }

    public void resetSpawners()
    {
        this.destroySpawners();
        this.minDistance = this.minDistance * 0.95f;
        this.spawnPoints = this.getSpawnPoints();
        this.enemySpawners = this.createSpawners();
        
    }

    private List<EnemySpawner> createSpawners()
    {
        List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
        for(int i=0; i < this.spawnPoints.Count; i++)
        {
            Vector3 spawnPoint = this.spawnPoints[i];
            EnemySpawner currentSpawner = this.spawnerPrefabs[Random.Range(0, this.spawnerPrefabs.Length)];
            EnemySpawner newSpawner = Instantiate(currentSpawner, spawnPoint, currentSpawner.transform.rotation);
            enemySpawners.Add(newSpawner);
        }
        return enemySpawners;
    }

    void SpawnFromSpawner()
    {
        EnemySpawner currentSpawner = this.enemySpawners[Random.Range(0, this.enemySpawners.Count)];
        currentSpawner.SpawnEnemy();
    }

    public void gameOver()
    {
        this.destroySpawners();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
