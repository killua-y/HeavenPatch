using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform player;
    public GameObject SwordPrefab;
    public float SwordSpawnTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        InvokeRepeating("SwordSpawn", 1, SwordSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    void SwordSpawn()
    {
        Quaternion SwordRotation = Quaternion.Euler(-90, 0, 0);
        Vector3 SwordPosition = transform.position;


        GameObject spawnedSword = Instantiate(SwordPrefab, SwordPosition, SwordRotation)
        as GameObject;

        spawnedSword.transform.parent = gameObject.transform;
    }


}
