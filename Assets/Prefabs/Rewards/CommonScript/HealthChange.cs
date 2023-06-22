using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChange : MonoBehaviour
{
    public float healthIncrease;
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
        PlayerHealth.healthProportion += healthIncrease;

        FindObjectOfType<LevelUpController>().LevelUpComplete();
    }
}
