using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceExplosionBehavior : MonoBehaviour
{
    public static float KnockBackForce = 10f;
    public static int ATKDamage = 20;

    public static float originalSize = 5f;

    private Vector3 originalScale;
    private Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        // 武器初始大小
        originalScale = new Vector3(originalSize, 0.2f, originalSize);

        // 设置武器的大小
        currentScale = originalScale * WeaponController.scaleMultipler;
        gameObject.transform.localScale = currentScale;

        Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 施加伤害修正
        float newDamge = ATKDamage * WeaponController.damgeMultipler * MaceController.BasicDamgeMultipler;
        int newDamgeInt = Mathf.RoundToInt(newDamge);

        //Debug.Log("hit enemy");
        if (other.CompareTag("Enemy"))
        {
            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();
            NormalEnemyHitScript.KnockBack(KnockBackForce);
            NormalEnemyHitScript.TakeDamage(newDamgeInt);
        }
        if (other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(ATKDamage);
        }
    }
}
