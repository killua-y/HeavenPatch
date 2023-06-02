using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform player;
    public GameObject SwordPrefab;
    public GameObject MacePrefab;
    public GameObject ArrowPrefab;
    public float SwordSpawnTime = 4f;
    public float MaceSpawnTime = 4f;
    public float ArrowSpawnTime = 2f;

    private Vector3 weaponPosition;

    private Quaternion weaponRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        weaponPosition = player.transform.position;
        weaponRotation = player.transform.rotation;
        
        InvokeRepeating("SwordSpawn", 1, SwordSpawnTime);
        InvokeRepeating("MaceSpawn", 1, MaceSpawnTime);
        InvokeRepeating("ArrowSpawn", 1, ArrowSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        weaponPosition = player.transform.position;
        weaponRotation = player.transform.rotation;
        transform.position = player.transform.position;
    }

    void SwordSpawn()
    {
        Quaternion WeaponRotation = Quaternion.Euler(-90, 0, 0);
        Vector3 WeaponPosition = weaponPosition;


        GameObject spawnedSword = Instantiate(SwordPrefab, WeaponPosition, WeaponRotation)
        as GameObject;

        spawnedSword.transform.parent = gameObject.transform;
    }
    
    void MaceSpawn()
    {
        Quaternion WeaponRotation = weaponRotation; 
        WeaponRotation *= Quaternion.Euler(0, -90, 0);
        Vector3 WeaponPosition = new Vector3(player.transform.position.x + 2f, 
            player.transform.position.y, player.transform.position.z);
        
        GameObject spawnedMace = Instantiate(MacePrefab, WeaponPosition, WeaponRotation)
            as GameObject;

        spawnedMace.transform.parent = gameObject.transform;
    }

    void ArrowSpawn()
    {
        Quaternion WeaponRotation = weaponRotation;
        WeaponRotation *= Quaternion.Euler(-90, 0, 0);
        Vector3 WeaponPosition = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
        
        GameObject spawnedArrow = Instantiate(ArrowPrefab, WeaponPosition, WeaponRotation)
            as GameObject;

        Rigidbody rb = spawnedArrow.GetComponent<Rigidbody>();
        
        rb.AddForce(player.transform.forward * 2000, ForceMode.Acceleration);

        spawnedArrow.transform.parent = gameObject.transform;
    }
}
