using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float range = 1f;
    public int damageAmount = 10;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > range)
        {
                if (!NormalEnemyHit.isDied)
                {
                    transform.LookAt(player);
                    transform.position = Vector3.MoveTowards(transform.position, player.position, step);
                    gameObject.GetComponent<Animator>().SetBool("Attack", false);
                    gameObject.GetComponent<Animator>().SetBool("Move", true);
                }
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Move", false);
            gameObject.GetComponent<Animator>().SetBool("Attack", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
