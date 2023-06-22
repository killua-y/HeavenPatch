using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpeedChange : MonoBehaviour
{
    public float movingSpeedIncrease;
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
        PlayerController.moveSpeed = 10 * movingSpeedIncrease + 10;

        FindObjectOfType<LevelUpController>().LevelUpComplete();
    }
}
