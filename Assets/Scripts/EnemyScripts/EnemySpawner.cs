using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;

    // 当前场上最大的怪物数量
    public static int MaxEnemyOnBoard = 40;
    public static int CurrentEnemyOnBoard = 0;

    public GameObject EnemyPrefab;
    public static float GroundSize = 46;
    public float spawnTime = 3;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        Spawn();
    }

    void Spawn()
    {
        StartCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnEnemy()
    {
        Vector3 enemyPosition;

        enemyPosition.x = Random.Range(Mathf.Clamp(player.position.x - 20, -(GroundSize / 2), GroundSize / 2),
            Mathf.Clamp(player.position.x + 20, -(GroundSize / 2), GroundSize/2));
        enemyPosition.y = player.position.y;
        enemyPosition.z = Random.Range(Mathf.Clamp(player.position.z - 20, -(GroundSize / 2), GroundSize / 2),
            Mathf.Clamp(player.position.z + 20, -(GroundSize / 2), GroundSize / 2));

        // 如果当前版面上的怪太多就停止生成
        if (CurrentEnemyOnBoard < MaxEnemyOnBoard)
        {
            GameObject spawnedEnemy = Instantiate(EnemyPrefab, enemyPosition, transform.rotation) as GameObject;
            CurrentEnemyOnBoard++;

            spawnedEnemy.transform.parent = gameObject.transform.parent;
        }

        yield return new WaitForSeconds(spawnTime);

        Spawn();
    }

    public void UpdateSpwanTime(float newSpwanTime)
    {
        spawnTime = newSpwanTime;
    }

}