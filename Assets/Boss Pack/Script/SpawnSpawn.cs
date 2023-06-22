using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpawn : MonoBehaviour
{
    public float transitionTime = 3f;

    private Renderer circlerenderer;
    private Color startColor;
    private Color targetColor;
    public int damageAmount = 10;
    private float elapsedTime;
    public GameObject player;

    private void Start()
    {

        circlerenderer = GetComponent<Renderer>();

        startColor = Color.white;
        targetColor = Color.red;
        elapsedTime = 0f;
        GameObject player = GameObject.FindWithTag("Player");
        
        DestroyAfterDelay(transitionTime); // 调用销毁方法
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        player = GameObject.FindGameObjectWithTag("Player");
        
        float t = Mathf.PingPong(Time.time, 1f);

        Color lerpedColor = Color.Lerp(startColor, targetColor, t);
        
        circlerenderer.material.color = lerpedColor;
        Debug.Log(IsPlayerInCircle(player.transform.position));

        if (t >= 1f)
        {
            if (player != null && IsPlayerInCircle(player.transform.position))
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    Debug.Log("take damage");
                    playerHealth.TakeDamage(damageAmount);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }

    private bool IsPlayerInCircle(Vector3 playerPosition)
    {
        Vector3 circlePosition = transform.position;

        float distance = Vector3.Distance(playerPosition, circlePosition);

        if (distance <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DestroyAfterDelay(float delay)
    {
        Destroy(gameObject, delay);
    }
}