using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIBehavior : MonoBehaviour
{
    public GameObject WeaponControllerSelf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableWeapon()
    {
        // 当我们替换武器的时候通过点击UI来替换
        if (LevelUpController.isChangeingWeapon)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {

    }

    private void OnEnable()
    {
        WeaponControllerSelf.SetActive(true);
    }

    private void OnDisable()
    {
        //WeaponControllerSelf.SetActive(false);
    }
}
