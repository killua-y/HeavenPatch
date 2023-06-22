using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseChange : MonoBehaviour
{
    public float defenseRateIncrease;
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
        PlayerHealth.defenseRate += defenseRateIncrease;

        FindObjectOfType<LevelUpController>().LevelUpComplete();
    }
}
