using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsController : MonoBehaviour
{
    public static bool isActive = false;
    public static bool doubleCast = false;
    public static int BasicDamgeMultipler = 1;
    
    public GameObject ArrowsPrefab;
    public Image WeaponIconMask;
    public Transform player;

    Vector3 originalScale;
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
        originalScale = new Vector3(1, 1, 1);

        ArrowSpawn();
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
                ArrowSpawn();
                if (doubleCast)
                {
                    Invoke("ArrowSpawn", 0.5f);
                }
                if (SwordBehavior.MasterWork)
                {
                    Invoke("ArrowSpawn", 0.5f);
                    Invoke("ArrowSpawn", 1f);
                }
                timer = 0;
                WeaponIconMask.fillAmount = 1;
                currentCoolDown = originalCoolDown * WeaponController.coolDownMultipler;
            }
        }
    }

    void ArrowSpawn()
    {
        // 设置武器的大小
        ArrowsPrefab.transform.localScale = originalScale * WeaponController.scaleMultipler;

        Quaternion WeaponRotation = player.transform.rotation;
        WeaponRotation *= Quaternion.Euler(-90, 0, 0);
        Vector3 WeaponPosition = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);

        // instantiate the weapon
        GameObject spawnedArrow = Instantiate(ArrowsPrefab, WeaponPosition, WeaponRotation)
            as GameObject;

        Rigidbody rb = spawnedArrow.GetComponent<Rigidbody>();

        rb.AddForce(player.transform.forward * 2000, ForceMode.Acceleration);

        spawnedArrow.transform.parent = gameObject.transform;
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
        ArrowBehavior.MasterWork = true;
    }

    public void AcquireWeapon()
    {
        isActive = true;
    }
}
