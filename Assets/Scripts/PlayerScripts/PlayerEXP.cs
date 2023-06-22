using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : MonoBehaviour
{
    public int EXPToUpgrade = 100;
    public Slider expSlider;
    public Text levelText;
    public static float expRate = 1;
    private int playerLevel;
    private float currentEXP;

    // Start is called before the first frame update
    void Start()
    {
        currentEXP = 0;
        playerLevel = 0;
        expSlider.maxValue = EXPToUpgrade;
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
            currentEXP += expAmount * expRate;
            expSlider.value = currentEXP;
        }

        if (currentEXP >= EXPToUpgrade)
        {
            Upgrade();
        }
    }

    void Upgrade()
    {
        playerLevel += 1;
        levelText.text = playerLevel.ToString();
        
        currentEXP = currentEXP - EXPToUpgrade;
        
        gameObject.GetComponent<PlayerHealth>().TakeHealth(10);

        // to make it become harder to upgrade
        EXPToUpgrade = playerLevel + 3;
        expSlider.maxValue = EXPToUpgrade;
        expSlider.value = currentEXP;

        // 在特定等级的时候提供武器奖励，反之则是buff奖励
        if((playerLevel == 1) || (playerLevel == 5) ||
            (playerLevel == 10) || (playerLevel == 15))
        {
            FindObjectOfType<LevelUpController>().WeaponSelect();
        }
        else
        {
            FindObjectOfType<LevelUpController>().LevelUp();
        }
    }
}
