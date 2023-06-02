using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{

    public AudioClip lootSFX;
    public int expAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        if(transform.position.y < Random.Range(1.0f, 3.0f)){
            Destroy(gameObject.GetComponent<Rigidbody>());
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