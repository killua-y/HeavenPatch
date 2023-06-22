using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    public GameObject rewardBoard;

    public List<GameObject> weaponRewards;
    public List<GameObject> normalRewards;
    public List<GameObject> rareRewards;
    public List<GameObject> goldRewards;
    public List<GameObject> mythicRewards;

    private List<GameObject> AllRewards;

    public static bool isChangeingWeapon;
    private GameStateManager gamePauseScript;


    GameObject firstReward;
    GameObject secondReward;
    GameObject thirdReward;

    GameObject firstRewardObject;
    GameObject secondRewardObject;
    GameObject thirdRewardObject;
    // Start is called before the first frame update
    void Start()
    {
        isChangeingWeapon = false;
        gamePauseScript = FindObjectOfType<GameStateManager>();

        //AllRewards.AddRange(normalRewards);
        //AllRewards.AddRange(rareRewards);
        //AllRewards.AddRange(goldRewards);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WeaponSelectComplete()
    {
        // 把未被选择的奖励disactive并添加回list里
        if (firstReward != null)
        {
            weaponRewards.Add(firstReward);
            firstReward.SetActive(false);
        }

        if (secondReward != null)
        {
            weaponRewards.Add(secondReward);
            secondReward.SetActive(false);
        }

        if (thirdReward != null)
        {
            weaponRewards.Add(thirdReward);
            thirdReward.SetActive(false);
        }

        for (int i = weaponRewards.Count - 1; i >= 0; i--)
        {
            if (weaponRewards[i] == null)
            {
                Debug.Log("null find");
                // Remove the missing GameObject reference from the list
                weaponRewards.RemoveAt(i);
            }
        }

        // resume the game
        gamePauseScript.ResumeGame();
    }


    public void WeaponSelect()
    {
        // pause the game
        gamePauseScript.PauseGame();

        // 随机选择三个奖励
        firstReward = SelectRandomObject(weaponRewards);

        secondReward = SelectRandomObject(weaponRewards);

        thirdReward = SelectRandomObject(weaponRewards);

        if(firstReward != null)
        {
            firstReward.SetActive(true);
        }
        else
        {
            firstReward = SelectRandomObject(weaponRewards);
            firstReward.SetActive(true);
        }

        if(secondReward != null)
        {
            secondReward.SetActive(true);
        }
        else
        {
            secondReward = SelectRandomObject(weaponRewards);
            secondReward.SetActive(true);
        }

        if (thirdReward != null)
        {
            thirdReward.SetActive(true);
        }
        else
        {
            thirdReward = SelectRandomObject(weaponRewards);
            thirdReward.SetActive(true);
        }


    }

    public void LevelUp()
    {
        // pause the game
        gamePauseScript.PauseGame();

        // 把奖励分为四种
        // 1：纯随机 0%
        // 2: 全是normal 40%
        // 3: 全是rare 20%
        // 4: 全是gold 20%
        // 5: 全是mythic 20%
        int randomIndex = Random.Range(1, 11);
        // gold奖励
        if((randomIndex == 1) || (randomIndex == 2))
        {
            firstReward = SelectRandomObject(goldRewards);
            secondReward = SelectRandomObject(goldRewards);
            thirdReward = SelectRandomObject(goldRewards);

            // 再讲奖励添加回奖励池（避免重复奖励的出现）
            goldRewards.Add(firstReward);
            goldRewards.Add(secondReward);
            goldRewards.Add(thirdReward);
        }
        // mythic 奖励
        if ((randomIndex == 10) || (randomIndex == 9))
        {
            firstReward = SelectRandomObject(mythicRewards);
            secondReward = SelectRandomObject(mythicRewards);
            thirdReward = SelectRandomObject(mythicRewards);

            // 再讲奖励添加回奖励池（避免重复奖励的出现）
            mythicRewards.Add(firstReward);
            mythicRewards.Add(secondReward);
            mythicRewards.Add(thirdReward);
        }
        // rare 奖励
        else if ((randomIndex == 8) || (randomIndex == 7))
        {
            firstReward = SelectRandomObject(rareRewards);
            secondReward = SelectRandomObject(rareRewards);
            thirdReward = SelectRandomObject(rareRewards);

            // 再讲奖励添加回奖励池（避免重复奖励的出现）
            rareRewards.Add(firstReward);
            rareRewards.Add(secondReward);
            rareRewards.Add(thirdReward);
        }
        // normal 奖励
        else
        {
            firstReward = SelectRandomObject(normalRewards);
            secondReward = SelectRandomObject(normalRewards);
            thirdReward = SelectRandomObject(normalRewards);

            // 再讲奖励添加回奖励池（避免重复奖励的出现）
            normalRewards.Add(firstReward);
            normalRewards.Add(secondReward);
            normalRewards.Add(thirdReward);
        }


        //创建三个奖励并将parent设置成rewardboard
        firstRewardObject = Instantiate(firstReward);
        firstRewardObject.transform.SetParent(rewardBoard.transform, false);

        secondRewardObject = Instantiate(secondReward);
        secondRewardObject.transform.SetParent(rewardBoard.transform, false);

        thirdRewardObject = Instantiate(thirdReward);
        thirdRewardObject.transform.SetParent(rewardBoard.transform, false);



    }

    public void LevelUpComplete()
    {
        //删除三个奖励
        Destroy(firstRewardObject);
        Destroy(secondRewardObject);
        Destroy(thirdRewardObject);

        // resume the game
        gamePauseScript.ResumeGame();
    }


    private GameObject SelectRandomObject(List<GameObject> myObjects)
    {
        if (myObjects.Count == 0)
        {
            Debug.LogWarning("List is empty.");
            return null;
        }

        // Generate a random index within the range of the list
        int randomIndex = Random.Range(0, myObjects.Count);

        // Retrieve the randomly selected object
        GameObject selectedObject = myObjects[randomIndex];

        // Remove the selected object from the list
        myObjects.RemoveAt(randomIndex);

        return selectedObject;
    }

}
