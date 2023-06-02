using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceBehavior : MonoBehaviour
{
    public static int ATKDamage = 20;
    public float rotationSpeed = 300;
    public Quaternion rotation = Quaternion.Euler(0, 0, 0);
    private float currentRotation = 0f;

    void Start()
    {

    }

    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        currentRotation += rotationSpeed * Time.deltaTime;

        // Check if the object has completed a full rotation
        if (currentRotation >= 90f)
        {
            // Perform any additional actions before destroying the object
            // ...

            // Destroy the object
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit enemy");
        if(other.CompareTag("Enemy"))
        {
            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();
            NormalEnemyHitScript.TakeDamage(ATKDamage);
        }

    }
}
