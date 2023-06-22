using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    public static int ATKDamage = 10;
    public static float KnockBackForce = 7f;
    public static float originalSize = 7f;
    public static bool MasterWork = false;

    private Vector3 originalScale;
    private Vector3 currentScale;

    void Start()
    {
        // 武器初始大小
        originalScale = new Vector3(originalSize, 0.2f, originalSize);

        // 设置武器的大小
        currentScale = originalScale * WeaponController.scaleMultipler;
        currentScale.y = 0.2f;
        gameObject.transform.localScale = currentScale;

        // 特效播放完毕就删除自己
        Destroy(gameObject, 0.4f);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // 施加伤害修正
        float newDamge = ATKDamage * WeaponController.damgeMultipler * SwordController.BasicDamgeMultipler;
        int newDamgeInt = Mathf.RoundToInt(newDamge);

        // 升级效果
        // 有40%的概率造成4倍伤害
        if (MasterWork)
        {
            int randomIndex = Random.Range(1, 11);
            if (randomIndex <= 4)
            {
                newDamgeInt *= 4;
            }
        }

        //Debug.Log("hit enemy");
        if (other.CompareTag("Enemy"))
        {
            // 对敌人造成伤害
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

