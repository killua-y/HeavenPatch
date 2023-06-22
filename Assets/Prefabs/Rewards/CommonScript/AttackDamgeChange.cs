using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamgeChange : MonoBehaviour
{
    public float damageIncrease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applyReward()
    {
        WeaponController.damgeMultipler += damageIncrease;
        Debug.Log("Current damgeMultipler: " + WeaponController.damgeMultipler);
        FindObjectOfType<LevelUpController>().LevelUpComplete();
    }
}
