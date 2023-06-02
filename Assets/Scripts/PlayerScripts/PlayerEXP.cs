using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : MonoBehaviour
{
    public int EXPToUpgrade = 100;
    public Slider expSlider;
    public Text levelText;
    private int playerLevel;
    private int currentEXP;

    // Start is called before the first frame update
    void Start()
    {
        currentEXP = 0;
        playerLevel = 0;
        expSlider.value = currentEXP;
        levelText.text = playerLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeEXP(int expAmount)
    {
        if (currentEXP < EXPToUpgrade)
        {
            currentEXP += expAmount;
            expSlider.value = currentEXP;
        }

        if (currentEXP == EXPToUpgrade)
        {
            Upgrade();
        }

    }

    void Upgrade()
    {
        Debug.Log("Level: " + playerLevel);
        playerLevel += 1;
        levelText.text = playerLevel.ToString();
        
        currentEXP = 0;
        expSlider.value = currentEXP;
    }
}
