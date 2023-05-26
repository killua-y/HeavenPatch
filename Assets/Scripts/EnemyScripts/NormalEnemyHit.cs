using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyHit : MonoBehaviour
{
    public int enemyHealth = 10;
    public static bool isDied;
    private int currentHealth;
    void Start()
    {
        currentHealth = enemyHealth;
        isDied = false;
    }
    
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //改成玩家攻击的tag
        {
            currentHealth -= 10; //改成玩家攻击力
        }
        if (currentHealth <= 0)
        {
            isDied = true;
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
}
