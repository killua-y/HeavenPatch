using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualReward : MonoBehaviour
{
    public static bool IncreaseBasicDamageBool = false;
    public static bool ApplyDoubleCastBool = false;
    public static bool MasterWorkUpgradeBool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishApplyReward()
    {
        //恢复游戏
        FindObjectOfType<GameStateManager>().ResumeGame();

        //把所有的bool设置为false
        IncreaseBasicDamageBool = false;
        ApplyDoubleCastBool = false;

        gameObject.SetActive(false);
    }

    public void IncreaseBasicDamage()
    {
        gameObject.SetActive(true);
        IncreaseBasicDamageBool = true;

        FindObjectOfType<LevelUpController>().LevelUpComplete();

        // 因为levelUpCompelete会恢复游戏所以要重新暂停
        FindObjectOfType<GameStateManager>().PauseGame();
    }

    public void ApplyDoubleCast()
    {
        Debug.Log("applied double cast");
        gameObject.SetActive(true);
        ApplyDoubleCastBool = true;

        FindObjectOfType<LevelUpController>().LevelUpComplete();
        // 因为levelUpCompelete会恢复游戏所以要重新暂停
        FindObjectOfType<GameStateManager>().PauseGame();
    }

    public void MasterWorkUpgrade()
    {
        gameObject.SetActive(true);
        MasterWorkUpgradeBool = true;

        FindObjectOfType<LevelUpController>().LevelUpComplete();
        // 因为levelUpCompelete会恢复游戏所以要重新暂停
        FindObjectOfType<GameStateManager>().PauseGame();
    }
}
