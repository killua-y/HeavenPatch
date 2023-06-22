using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireExplosionBehavior : MonoBehaviour
{
    public static int ATKDamage = 10;
    public static float KnockBackForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit enemy");
        // 施加伤害修正
        float newDamge = ATKDamage * WeaponController.damgeMultipler * MagicFireController.BasicDamgeMultipler;
        int newDamgeInt = Mathf.RoundToInt(newDamge);
        if (other.CompareTag("Enemy"))
        {   // 对敌人造成伤害
            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();
            NormalEnemyHitScript.KnockBack(KnockBackForce);
            NormalEnemyHitScript.TakeDamage(newDamgeInt);
        }
        if (other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(newDamgeInt);
        }
    }
}
