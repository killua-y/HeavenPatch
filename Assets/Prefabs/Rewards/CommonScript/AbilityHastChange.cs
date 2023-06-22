using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHastChange : MonoBehaviour
{
    public int AbilityHasteIncrease;
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
        FindObjectOfType<WeaponController>().UpdateCDR(AbilityHasteIncrease);
        FindObjectOfType<LevelUpController>().LevelUpComplete();
    }
}
