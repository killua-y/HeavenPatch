using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiIndividualReward : MonoBehaviour
{
    public GameObject WeaponController;

    MonoBehaviour WeaponControllerScipt;
    // Start is called before the first frame update
    void Start()
    {
        WeaponControllerScipt = WeaponController.GetComponent<MonoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyReward()
    {
        if(IndividualReward.IncreaseBasicDamageBool)
        {
            WeaponControllerScipt.Invoke("IncreaseBasicDamage", 0f);

            // 完成奖励施加
            FindObjectOfType<IndividualReward>().FinishApplyReward();
        }
        else if(IndividualReward.ApplyDoubleCastBool)
        {
            WeaponControllerScipt.Invoke("ActiveDoubleCaset", 0f);

            // 完成奖励施加
            FindObjectOfType<IndividualReward>().FinishApplyReward();
        }
        else if (IndividualReward.MasterWorkUpgradeBool)
        {
            WeaponControllerScipt.Invoke("MasterWorkUpgrade", 0f);

            // 完成奖励施加
            FindObjectOfType<IndividualReward>().FinishApplyReward();
        }
    }


}
