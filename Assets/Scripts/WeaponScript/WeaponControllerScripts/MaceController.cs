using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaceController : MonoBehaviour
{
    public GameObject MacePrefab;
    public Image WeaponIconMask;
    public Transform player;

    public static bool isActive = false;
    public static bool doubleCast = false;
    public static int BasicDamgeMultipler = 1;

    float originalCoolDown = 4f;
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

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        currentCoolDown = originalCoolDown;
        MaceSpawn();
        timer = 0;
        WeaponIconMask.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManager.isPaused)
        {
            timer += Time.deltaTime;
            WeaponIconMask.fillAmount = (currentCoolDown - timer) / currentCoolDown;

            if (timer >= currentCoolDown)
            {
                MaceSpawn();

                // 如果doubleCast变成了true那就会连续释放两次技能
                if (doubleCast)
                {
                    Invoke("MaceSpawn", 0.5f);
                }

                timer = 0;
                WeaponIconMask.fillAmount = 1;
                currentCoolDown = originalCoolDown * WeaponController.coolDownMultipler;
            }
        }
    }

    void MaceSpawn()
    {
        Quaternion WeaponRotation = player.transform.rotation;
        WeaponRotation *= Quaternion.Euler(0, -90, 0);
        Vector3 WeaponPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // instantiate the weapon
        GameObject spawnedMace = Instantiate(MacePrefab, WeaponPosition, WeaponRotation)
            as GameObject;

        spawnedMace.transform.parent = gameObject.transform;
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
        MaceBehavior.MasterWork = true;
    }

    public void AcquireWeapon()
    {
        isActive = true;
    }
}
