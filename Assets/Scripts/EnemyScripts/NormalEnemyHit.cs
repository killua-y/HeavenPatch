using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyHit : MonoBehaviour
{
    public int enemyHealth = 20;
    public bool isDied;
    public GameObject gem;
    private int currentHealth;
    void Start()
    {
        currentHealth = enemyHealth;
        isDied = false;
    }
    
    void Update()
    {
        if (isDied)
        {
            DestroyEnemy();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }

        if (currentHealth <= 0)
        {
            isDied = true;
            DestroyEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    void DestroyEnemy()
    {
        //Debug.Log("AAA");
        gameObject.GetComponent<Animator>().SetTrigger("Die");
        Instantiate(gem, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
