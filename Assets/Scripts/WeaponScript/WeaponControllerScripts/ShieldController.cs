using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour
{
    public static bool isActive = false;
    public static bool doubleCast = false;
    public static int BasicDamgeMultipler = 1;

    public GameObject ShieldPrefab;
    public Image WeaponIconMask;
    public Transform player;

    Vector3 originalScale;
    float originalCoolDown = 10f;
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
        originalScale = new Vector3(5, 5, 5);

        ShieldSpawn();
        timer = 0;
        WeaponIconMask.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManager.isPaused)
        {
            if (ShieldBehavior.MasterWork)
            {
                originalScale *= 1.5f;
            }
            timer += Time.deltaTime;
            WeaponIconMask.fillAmount = (currentCoolDown - timer) / currentCoolDown;

            if (timer >= currentCoolDown)
            {
                ShieldSpawn();
                if (doubleCast)
                {
                    Invoke("ShieldSpawn", 0.5f);
                }

                timer = 0;
                WeaponIconMask.fillAmount = 1;
                currentCoolDown = originalCoolDown * WeaponController.coolDownMultipler;
            }
        }
    }

    void ShieldSpawn()
    {
        // 设置武器的大小
        ShieldPrefab.transform.localScale = originalScale * WeaponController.scaleMultipler;

        Quaternion WeaponRotation = player.transform.rotation;
        //WeaponRotation *= Quaternion.Euler(-90, 0, 0);
        Vector3 WeaponPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + 1.5f);

        // instantiate the weapon
        GameObject spawnedShield = Instantiate(ShieldPrefab, WeaponPosition, WeaponRotation)
            as GameObject;

        spawnedShield.transform.parent = gameObject.transform;
        
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
    public void AcquireWeapon()
    {
        isActive = true;
    }
}
