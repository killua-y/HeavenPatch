using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 3;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
        // first time is start time, second time is repeate time
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemies(){
        Vector3 enemyPosition;

        enemyPosition.x = Random.Range(transform.position.x - 10, transform.position.x + 10);
        enemyPosition.y = transform.position.y;
        enemyPosition.z = Random.Range(transform.position.z - 10, transform.position.z + 10);

        GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;

        spawnedEnemy.transform.parent = gameObject.transform;
    }
}