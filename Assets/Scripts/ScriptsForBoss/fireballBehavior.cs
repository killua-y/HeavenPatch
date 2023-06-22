using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballBehavior : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 10;
    
    public int damageAmount = 10;

    
    // public AudioClip enemySFX;
    Rigidbody rb;
    

    void Start()
    {
        
        // find the player to kill
        if(player == null){
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if the game is not over yet
        // transform.LookAt(player);
        transform.Rotate(Vector3.forward, 360 * Time.deltaTime);
        
        // move towards player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        

    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("FireBall")){
            //AudioSource.PlayClipAtPoint(enemySFX, Camera.main.transform.position);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Player")){
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
            
        }

        
    }

    
}
