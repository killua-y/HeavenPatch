using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyHit : MonoBehaviour
{
    public int enemyHealth = 20;
    public bool isDied;
    private int currentHealth;
    void Start()
    {
        currentHealth = enemyHealth;
        isDied = false;
    }
    
    void Update()
    {

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
        gameObject.GetComponent<Animator>().SetTrigger("Die");
        Destroy(gameObject, 2f);
    }

}
