using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public static int ATKDamage = 5;
    public static bool MasterWork = false;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 施加伤害修正
        float newDamge = ATKDamage * WeaponController.damgeMultipler * ArrowsController.BasicDamgeMultipler;
        int newDamgeInt = Mathf.RoundToInt(newDamge);

        //Debug.Log("hit enemy");
        if (other.CompareTag("Enemy"))
        {
            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();
            NormalEnemyHitScript.TakeDamage(newDamgeInt);

            if(MasterWork)
            {
                NormalEnemyHitScript.TakeDamage(newDamgeInt);
                NormalEnemyHitScript.TakeDamage(newDamgeInt);
            }
        }
        if (other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(ATKDamage);

            if (MasterWork)
            {
                BossHitScript.TakeDamage(newDamgeInt);
                BossHitScript.TakeDamage(newDamgeInt);
            }
        }
    }
}