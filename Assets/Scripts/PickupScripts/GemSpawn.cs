using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawn : MonoBehaviour
{
    public GameObject GemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnGem", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnGem()
    {
        Vector3 GemPosition;

        GemPosition.x = Random.Range(transform.position.x - 10, transform.position.x + 10);
        GemPosition.y = transform.position.y;
        GemPosition.z = Random.Range(transform.position.z - 10, transform.position.z + 10);

        GameObject spawnedEnemy = Instantiate(GemPrefab, GemPosition, transform.rotation) as GameObject;

        spawnedEnemy.transform.parent = gameObject.transform;
    }
}
