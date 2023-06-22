using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{
    Transform player;
    public AudioClip lootSFX;
    public int expAmount = 1;
    public static float collectDistance = 3;
    float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        if(transform.position.y < Random.Range(1.0f, 3.0f)){
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        // calculate the distance to player, if the player is getting close enough
        // move to the player.
        if (Vector3.Distance(player.position, gameObject.transform.position)
            <= collectDistance)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, player.position, 20 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            //Debug.Log("Player Picks me!");
            // gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(lootSFX, transform.position);
            var playerExp = other.GetComponent<PlayerEXP>();
            playerExp.TakeEXP(expAmount);
            Destroy(gameObject);
        }
    }
}