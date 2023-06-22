using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 100;
    public int MaxHealth = 100;
    public static float defenseRate = 1;
    public static float healthProportion = 1;
    public Slider healthSlider;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = StartingHealth;
        healthSlider.value = currentHealth;
        MaxHealth = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MaxHealth = (int)(StartingHealth * healthProportion);
        healthSlider.maxValue = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= (int)(damageAmount/defenseRate);
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }
    
    public void TakeHealth(int healthAmount)
    {
        if (currentHealth > 0 && currentHealth < MaxHealth)
        {
            currentHealth += healthAmount;
            healthSlider.value = currentHealth;
        }
    }

    void PlayerDies()
    {
        //Debug.Log("Player is dead ...");
        GetComponent<PlayerController>().anim.SetInteger("States", 2);
        FindObjectOfType<LevelManager>().LevelLost();
    }
}
