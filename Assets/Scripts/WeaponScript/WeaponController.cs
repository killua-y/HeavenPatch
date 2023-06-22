using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform player;

    public static float scaleMultipler = 1;
    public static float coolDownMultipler = 1;
    public static float damgeMultipler = 1;
    public static float abilityHaste = 0;

    public static Vector3 weaponPosition;
    public static Quaternion weaponRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        weaponPosition = player.transform.position;
        weaponRotation = player.transform.rotation;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            weaponPosition = player.transform.position;
            weaponRotation = player.transform.rotation;
            transform.position = player.transform.position;
        }
    }

    public void UpdateCDR(int increaseAmount)
    {
        abilityHaste += increaseAmount;
        float coolDownMultipler = 1f - (1f / (1f + (abilityHaste / 100f)));
        Debug.Log("coolDownMultipler: " + (coolDownMultipler * 100f) + "%");
    }


}
