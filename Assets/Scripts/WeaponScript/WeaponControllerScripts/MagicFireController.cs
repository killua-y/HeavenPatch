using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicFireController : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public Image WeaponIconMask;
    public Transform player;

    public static bool isActive = true;
    public static bool doubleCast = true;
    public static int BasicDamgeMultipler = 1;

    public static float WeaponNumber = 4f; // Number of function calls
    public float duration = 1f; // Duration in seconds

    float originalCoolDown = 7f;
    float currentCoolDown;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (!isActive)
        {
            gameObject.SetActive(false);
            WeaponIconMask.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            WeaponIconMask.transform.parent.gameObject.SetActive(true);
        }

        // 获取player的位置和转向
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //StartCoroutine(CallFunction());

        //currentCoolDown = originalCoolDown;
        WeaponSpawn();
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

            CastSpell();

            //如果doubleCast变成了true那就会连续释放两次技能
            if (doubleCast)
            {
                Invoke("CastSpell", 1f);
            }

            timer = 0;
            WeaponIconMask.fillAmount = 1;
            currentCoolDown = originalCoolDown * WeaponController.coolDownMultipler;
        }
    }

    void CastSpell()
    {
        StartCoroutine(MultipleCast());
    }

    private IEnumerator MultipleCast()
    {
        float calls = Mathf.Round(WeaponNumber * WeaponController.scaleMultipler);
        float interval = duration / calls; // Calculate the interval between each call

        for (int i = 0; i < calls; i++)
        {
            // Call your function here
            WeaponSpawn();

            yield return new WaitForSeconds(interval);
        }
    }

    void WeaponSpawn()
    {
        Quaternion WeaponRotation = Quaternion.Euler(0, 0, 0);
        Vector3 WeaponPosition = transform.position;
        WeaponPosition.y += 1;

        // instantiate the weapon
        GameObject spawnedWeapon = Instantiate(WeaponPrefab, WeaponPosition, transform.rotation)
        as GameObject;

        spawnedWeapon.GetComponent<Rigidbody>().AddForce(player.forward * 2000);

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
        MagicFireBehavior.MasterWork = true;
    }

    public void AcquireWeapon()
    {
        isActive = true;
    }
}
