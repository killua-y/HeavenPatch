using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Fire_Health : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    public Slider healthSlider;

    public int currentHealth;

    // Start is called before the first frame update
    private void Awake()
    {
        //healthSlider = GetComponentInChildren<Slider>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;

        // 帮助玩家收集其他的gem
        GemPickup.collectDistance = 300;
        Invoke("FinishCollectGem", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        //Debug.Log("Current health: " + currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("AAA");
        AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        FindObjectOfType<LevelManager>().LevelBeat();
    }

    void FinishCollectGem()
    {
        GemPickup.collectDistance = 3;
    }
}
