using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform player;
    public GameObject SwordPrefab;
    public GameObject MacePrefab;
    public float SwordSpawnTime = 4f;
    public float MaceSpawnTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        InvokeRepeating("SwordSpawn", 1, SwordSpawnTime);
        InvokeRepeating("MaceSpawn", 1, MaceSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    void SwordSpawn()
    {
        Quaternion WeaponRotation = Quaternion.Euler(-90, 0, 0);
        Vector3 WeaponPosition = transform.position;


        GameObject spawnedSword = Instantiate(SwordPrefab, WeaponPosition, WeaponRotation)
        as GameObject;

        spawnedSword.transform.parent = gameObject.transform;
    }
    
    void MaceSpawn()
    {
        Quaternion WeaponRotation = player.transform.rotation;
        Vector3 WeaponPosition = new Vector3(transform.position.x + 1, transform.position.y + 3, transform.position.z);
        
        GameObject spawnedSword = Instantiate(MacePrefab, WeaponPosition, WeaponRotation)
            as GameObject;

        spawnedSword.transform.parent = gameObject.transform;
    }


}
