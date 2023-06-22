using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehavior : MonoBehaviour
{   
    public float destroyDuration = 3.5f;
    public int damageAmount = 20;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
            
        }
    }
}
