using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPRateChange : MonoBehaviour
{
    public float EXPRateIncrease;
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
        PlayerEXP.expRate += EXPRateIncrease;

        FindObjectOfType<LevelUpController>().LevelUpComplete();
    }
}
