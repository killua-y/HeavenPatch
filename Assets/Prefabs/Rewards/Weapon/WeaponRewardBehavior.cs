using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRewardBehavior : MonoBehaviour
{
    public GameObject WeaponUI;
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
        WeaponUI.SetActive(true);
        FindObjectOfType<LevelUpController>().WeaponSelectComplete();
        Destroy(gameObject);
    }
}
