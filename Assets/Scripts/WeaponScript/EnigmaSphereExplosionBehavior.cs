using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaSphereExplosionBehavior : MonoBehaviour
{
    public static int ATKDamage = 10;
    public static float KnockBackForce = 1f;
    public static float originalSize = 3f;

    private Vector3 originalScale;
    private Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        // 武器初始大小
        originalScale = new Vector3(originalSize, originalSize, originalSize);

        // 设置武器的大小
        currentScale = originalScale * WeaponController.scaleMultipler;
        gameObject.transform.localScale = currentScale;

        Destroy(gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 施加伤害修正
        float newDamge = ATKDamage * WeaponController.damgeMultipler * EnigmaSphereController.BasicDamgeMultipler;
        int newDamgeInt = Mathf.RoundToInt(newDamge);
        //Debug.Log("hit enemy");
        if (other.CompareTag("Enemy"))
        {
            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();

            // 对敌人造成击退
            NormalEnemyHitScript.KnockBack(KnockBackForce);
            // 对敌人造成伤害
            NormalEnemyHitScript.TakeDamage(newDamgeInt);
        }
        if (other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(newDamgeInt);
        }
    }
}
