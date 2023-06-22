using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NormalEnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float range = 1f;
    public int damageAmount = 10;
    private Transform player;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            float step = moveSpeed * Time.deltaTime;
            float distance = Vector3.Distance(transform.position, player.position);
            var EnemyHit = gameObject.GetComponent<NormalEnemyHit>();
            if (distance > range)
            {
                if (!EnemyHit.isDied)
                {
                    transform.LookAt(player);
                    agent.SetDestination(player.position);
                    //transform.position = Vector3.MoveTowards(transform.position, player.position, step);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }

}
