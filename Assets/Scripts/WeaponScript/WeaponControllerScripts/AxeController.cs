using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeController : MonoBehaviour
{
    public GameObject AxePrefab;
    public Image WeaponIconMask;
    public Transform player;

    public static bool isActive = false;
    public static bool doubleCast = false;
    public static int BasicDamgeMultipler = 1;

    Vector3 originalScale;
    float originalCoolDown = 6f;
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
        originalScale = new Vector3(3, 3, 3);
        AxeSpawn();
        timer = 0;
        WeaponIconMask.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameStateManager.isPaused)
        {
            timer += Time.deltaTime;
            WeaponIconMask.fillAmount = (currentCoolDown - timer) / currentCoolDown;

            if (timer >= currentCoolDown)
            {
                AxeSpawn();
                if (doubleCast)
                {
                    Invoke("AxeSpawn", 0.7f);
                }

                timer = 0;
                WeaponIconMask.fillAmount = 1;
                currentCoolDown = originalCoolDown * WeaponController.coolDownMultipler;
            }
        }
    }

    void AxeSpawn()
    {
        // 设置武器的大小
        AxePrefab.transform.localScale = originalScale * WeaponController.scaleMultipler;

        Quaternion WeaponRotation = Quaternion.Euler(-90, 0, 0);
        Vector3 WeaponPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // instantiate the weapon
        GameObject spawnedAxe = Instantiate(AxePrefab, WeaponPosition, WeaponRotation)
            as GameObject;

        spawnedAxe.transform.parent = gameObject.transform;
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
        AxeBehavior.MasterWork = true;
    }

    public void AcquireWeapon()
    {
        isActive = true;
    }
}
