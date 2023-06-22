using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyHit : MonoBehaviour
{
    public int enemyHealth = 20;
    public bool isDied;
    public GameObject gem;
    public GameObject HealthCanvas;
    private int currentHealth;

    Transform player;
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        currentHealth = (int)(enemyHealth * EnemySpawnerController.EnemyHealthMultipler * EnemySpawnerController.LevelDifficulty);

        // 设置helth slide的最大数值
        if(HealthCanvas != null)
        {
            HealthCanvas.GetComponent<EnemyHealthCanvasBehavior>().SetMax(currentHealth);
        }

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
        // 显示伤害数值
        if (HealthCanvas != null)
        {
            HealthCanvas.GetComponent<EnemyHealthCanvasBehavior>().TakeDamage(damageAmount);
        }

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

    public void KnockBack(float forceMagnitude)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 directionToPlayer = player.position - transform.position;
        Vector3 normalizedDirection = directionToPlayer.normalized;
        normalizedDirection.y = 0;
        Vector3 force = -normalizedDirection * forceMagnitude;

        rb.AddForce(force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    void DestroyEnemy()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Die");
        Instantiate(gem, transform.position, transform.rotation);

        //减少怪物数量
        EnemySpawnerController.CurrentEnemyRemain--;
        EnemySpawner.CurrentEnemyOnBoard--;

        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }


}
