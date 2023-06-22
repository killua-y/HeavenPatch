using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceBehavior : MonoBehaviour
{
    public GameObject effect;
    public GameObject ExplosionPrefab;

    public static float KnockBackForce = 10f;
    public static int ATKDamage = 20;
    public static bool MasterWork = false;

    public float rotationSpeed = 300;
    private float currentRotation = 0f;
    bool stopRotation = false;
    public static float originalSize = 5f;

    float delayTime = 2f;
    float EffectScaleMultipler;

    private Vector3 originalScale;
    private Vector3 currentScale;
    ParticleSystem[] childParticleSystems;
    bool casted = false;
    void Start()
    {
        
        // 武器初始大小
        originalScale = new Vector3(originalSize, originalSize, originalSize);

        // 设置武器的大小
        currentScale = originalScale * WeaponController.scaleMultipler;
        gameObject.transform.localScale = currentScale;

    }

    void Update()
    {
        // Rotate the object around its Y-axis
        if (!stopRotation)
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
            currentRotation += rotationSpeed * Time.deltaTime;
        }

        // Check if the object has completed a full rotation
        if ((currentRotation >= 90f) && (!casted))
        {
            // 让锤子停止旋转停在原地并消失
            stopRotation = true;
            gameObject.transform.parent = null;
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;

            // 生成砸地特效
            if (MasterWork)
            {
                StartCoroutine(MultipleCast());
            }
            else
            {
                GameObject spawnedExplosion = Instantiate(ExplosionPrefab, effect.transform.position, Quaternion.Euler(0, 0, 0))
                as GameObject;
            }

            // 删除锤子
            Destroy(gameObject, delayTime);

            //保证这个if只会call一次
            casted = true;
        }
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

    private IEnumerator MultipleCast()
    {

        float calls = 4;
        float duration = 1;
        float interval = duration / calls; // Calculate the interval between each call

        for (int i = 0; i < calls; i++)
        {
            // Call your function here
            GameObject spawnedExplosion = Instantiate(ExplosionPrefab, effect.transform.position, Quaternion.Euler(0, 0, 0))
                as GameObject;

            yield return new WaitForSeconds(interval);
        }
    }
}
