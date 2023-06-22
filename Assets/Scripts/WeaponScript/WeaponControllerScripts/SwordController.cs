using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordController : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public Image WeaponIconMask;

    public static bool doubleCast = false;
    public static int BasicDamgeMultipler = 1;

    float originalCoolDown = 3f;
    float currentCoolDown;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        currentCoolDown = originalCoolDown;
        SwordSpawn();
        timer = 0;
        WeaponIconMask.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        WeaponIconMask.fillAmount = (currentCoolDown - timer) / currentCoolDown;

        if (timer >= currentCoolDown)
        {
            SwordSpawn();

            // 如果doubleCast变成了true那就会连续释放两次技能
            if (doubleCast)
            {
                Invoke("SwordSpawn", 0.5f);
            }

            timer = 0;
            WeaponIconMask.fillAmount = 1;
            currentCoolDown = originalCoolDown * WeaponController.coolDownMultipler;
        }
    }

    void SwordSpawn()
    {
        Quaternion WeaponRotation = Quaternion.Euler(0, 0, 0);
        Vector3 WeaponPosition = transform.position;

        // instantiate the weapon
        GameObject spawnedSword = Instantiate(WeaponPrefab, WeaponPosition, transform.rotation)
        as GameObject;

        spawnedSword.transform.parent = gameObject.transform;

    }

    void IncreaseBasicDamage()
    {
        BasicDamgeMultipler++;
    }

    void ActiveDoubleCaset()
    {
        doubleCast = true;
    }

    void MasterWorkUpgrade()
    {
        SwordBehavior.MasterWork = true;
    }
}
