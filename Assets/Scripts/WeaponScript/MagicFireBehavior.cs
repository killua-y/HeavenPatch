using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireBehavior : MonoBehaviour
{
    public static int ATKDamage = 10;
    public static float KnockBackForce = 1f;
    public static bool MasterWork = false;

    public GameObject impactParticle; // Effect spawned when projectile hits a collider
    public GameObject projectileParticle; // Effect attached to the gameobject as child
    public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned
    public GameObject magicFireExplosion;

    int explosionNumber = 1;
    void Start()
    {
        if(MasterWork)
        {
            explosionNumber = 2;
        }

        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 3f); // 2nd parameter is lifetime of effect in seconds
        }

        // 如果没有打中敌人那就三秒后删除自己
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity); // Sets rotation to look at direction of movement
        }
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // 施加伤害修正
        float newDamge = ATKDamage * WeaponController.damgeMultipler * MagicFireController.BasicDamgeMultipler;
        int newDamgeInt = Mathf.RoundToInt(newDamge);
        //Debug.Log("hit enemy");
        if (other.CompareTag("Enemy"))
        {
            // 对敌人造成伤害
            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();
            NormalEnemyHitScript.KnockBack(KnockBackForce);
            NormalEnemyHitScript.TakeDamage(newDamgeInt);

            if (MasterWork)
            {
                // 如果是最后一下爆炸那就造成完伤害删除自己
                if (explosionNumber == 1)
                {
                    FinalExplosion();
                    Destroy(gameObject);
                }
                else
                {
                    Explosion();
                    explosionNumber--;
                }
            }
            else
            {
                Explosion();
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(newDamgeInt);
            
            if (MasterWork)
            {
                //因为是击中了boss所以直接进行最终爆炸
                FinalExplosion();
                Destroy(gameObject);
            }
            else
            {
                Explosion();
                Destroy(gameObject);
            }
        }

    }

    void Explosion()
    {
        GameObject impactP = Instantiate(impactParticle, transform.position, transform.rotation) as GameObject; // Spawns impact effect

        Destroy(impactP, 3.5f); // Removes impact effect after delay
    }

    void FinalExplosion()
    {
        impactParticle.transform.localScale = new Vector3(3, 3, 3);
        GameObject impactP = Instantiate(impactParticle, transform.position, transform.rotation) as GameObject; // Spawns impact effect
        impactParticle.transform.localScale = new Vector3(1, 1, 1);

        // explosion会自己destory自己
        GameObject Explosion = Instantiate(magicFireExplosion, transform.position, transform.rotation) as GameObject; // Spawns impact effect


        Destroy(impactP, 3.5f); // Removes impact effect after delay
        Destroy(projectileParticle, 3f); // Removes particle effect after delay
    }
}
