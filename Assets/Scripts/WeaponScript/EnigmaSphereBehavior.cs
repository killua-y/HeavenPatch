using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaSphereBehavior : MonoBehaviour
{
    public GameObject explosionPrefab;
    public Transform player;

    public static int ATKDamage = 10;
    public static float KnockBackForce = 1f;
    public static float originalSize = 3f;
    public static bool MasterWork = false;

    private Vector3 originalScale;
    private Vector3 currentScale;
    private float trackDistance = 20;
    public int explosionNumber = 3;

    GameObject[] enemies;
    GameObject nextTarget;
    
    void Start()
    {
        if (MasterWork)
        {
            explosionNumber = 10;
        }

        // 获取player的信息
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // 武器初始大小
        originalScale = new Vector3(originalSize, originalSize, originalSize);

        // 设置武器的大小
        currentScale = originalScale * WeaponController.scaleMultipler;
        gameObject.transform.localScale = currentScale;

        //最多存在20秒
        Destroy(gameObject, 20f);

    }

    void Update()
    {
        if (nextTarget == null)
        {
            nextTarget = FindNextTarget();
            if(nextTarget == null)
            {
                nextTarget = GameObject.FindGameObjectWithTag("Boss_Fire");
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextTarget.transform.position, 5 * Time.deltaTime);
        }

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

            if (explosionNumber >= 1)
            {
                explosionPrefab.transform.localScale = transform.localScale;
                GameObject spawnedExplosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;

                if (explosionNumber == 1)
                {
                    Destroy(gameObject, 0.2f);
                }
                explosionNumber--;
            }
        }
        if (other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(newDamgeInt);


            if (explosionNumber >= 1)
            {
                explosionPrefab.transform.localScale = transform.localScale;
                GameObject spawnedExplosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;

                if (explosionNumber == 1)
                {
                    Destroy(gameObject, 0.2f);
                }
                explosionNumber--;
            }
        }
    }

    GameObject FindNextTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, player.transform.position);
            if (distance <= trackDistance)
            {
                return enemy; // Return the enemy within tracking distance
            }
        }
        return null; // No enemy found within tracking distance

    }



}
