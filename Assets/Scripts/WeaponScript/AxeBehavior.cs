using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehavior : MonoBehaviour
{
    public static int ATKDamage = 10;
    public static float KnockBackForce = 4f;
    public static bool MasterWork = false;


    public float rotationSpeed = 1800;
    private float currentRotation = 0f;
    private Transform player;
    private Vector3 direction;
    private bool ComeBack = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = player.forward;

    }

    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        currentRotation += rotationSpeed * Time.deltaTime;

        // 触碰到敌人之后会返回到玩家手中
        if(!ComeBack)
        {
            transform.position += direction * Time.deltaTime * 10;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
        }

        // 如果斧头距离太远会自动回到玩家手里
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance >= 30f)
        {
            ComeBack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit enemy");
        if(other.CompareTag("Enemy"))
        {
            // 施加伤害修正
            float newDamge = ATKDamage * WeaponController.damgeMultipler * AxeController.BasicDamgeMultipler;
            int newDamgeInt = Mathf.RoundToInt(newDamge);
            
            if (MasterWork)
            {
                rotationSpeed *= 2;
                KnockBackForce *= 2;
                newDamgeInt *= 2;
            }

            var NormalEnemyHitScript = other.GetComponent<NormalEnemyHit>();
            NormalEnemyHitScript.TakeDamage(newDamgeInt);
            NormalEnemyHitScript.KnockBack(KnockBackForce);

            // 让斧头转回来
            Invoke("BackToPlayer", 1);
        }

        if (other.CompareTag("Boss_Fire"))
        {
            var BossHitScript = other.GetComponent<Boss_Fire_Health>();
            BossHitScript.TakeDamage(ATKDamage);
        }

        if (other.CompareTag("Player"))
        {
            if(ComeBack)
            {
                Destroy(gameObject);
            }
        }
    }

    void BackToPlayer()
    {
        ComeBack = true;
    }
}
