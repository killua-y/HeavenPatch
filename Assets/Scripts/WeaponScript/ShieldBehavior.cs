using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    public static int ATKDamage = 10;
    public static bool MasterWork = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.rotation;
        transform.position = player.position + player.forward * 1.5f;
        Destroy(gameObject, 5);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        float newDamge = ATKDamage * WeaponController.damgeMultipler * SwordController.BasicDamgeMultipler;
        int newDamgeInt = Mathf.RoundToInt(newDamge);

        //Debug.Log("hit enemy");
        if(other.CompareTag("Enemy"))
        {
            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();
            NormalEnemyHitScript.TakeDamage(newDamgeInt);
            
        }

        if(other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(newDamgeInt);
        }
    }
}
