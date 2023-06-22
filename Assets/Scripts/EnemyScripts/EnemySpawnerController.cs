using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawnerController : MonoBehaviour
{
    public int EnemyRemain;
    public static int CurrentEnemyRemain;

    // 用于控制怪物血量buff
    public static float EnemyHealthMultipler = 1;
    public static float LevelDifficulty = 1f;

    public TextMeshProUGUI EnemyRemianText;

    public GameObject SpiderSpawner;
    public GameObject WolfSpawner;
    public GameObject ArcherSpawner;
    public GameObject BossPrefab;

    bool level1 = false;
    bool level2 = false;
    bool level3 = false;

    // Start is called before the first frame update
    void Start()
    {
        // 设置该level的地方击杀数
        CurrentEnemyRemain = EnemyRemain;

        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            level1 = true;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            level2 = true;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            level3 = true;
        }
    }

    public void Easy()
    {
        LevelDifficulty = 0.5f;
    }

    public void Medium()
    {
        LevelDifficulty = 1f;
    }

    public void Hard()
    {
        LevelDifficulty = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // 设置剩余敌人text
        EnemyRemianText.text = "Enemy Remain: " + CurrentEnemyRemain;

        // 根据当前level的不同生成不同的怪物
        if(level1)
        {
            Level1EnemySpawn();
        }
        else if(level2)
        {
            Level2EnemySpawn();
        }
        else if(level3)
        {
            Level3EnemySpawn();
        }

    }

    void Level1EnemySpawn()
    {
        // 当怪物还剩200 到 150
        if (CurrentEnemyRemain == 200)
        {
            // 生成蜘蛛形怪物
            SpiderSpawner.SetActive(true);
            SpiderSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);

            // 生成狼形怪物
            WolfSpawner.SetActive(false);
        }
        // 当怪物还剩150 到 100
        else if (CurrentEnemyRemain == 150)
        {
            // 停止生成蜘蛛形怪物
            SpiderSpawner.SetActive(true);
            SpiderSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);

            // 停止生成弓箭手怪物
            ArcherSpawner.SetActive(false);
        }
        // 当怪物还剩100 到 0
        else if (CurrentEnemyRemain == 100)
        {
            // 提高怪物血量
            EnemyHealthMultipler = 2;

            // 生成蜘蛛形怪物
            SpiderSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.3f);

            // 生成弓箭手怪物
            ArcherSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);
        }
        else if (CurrentEnemyRemain == 50)
        {
            // 提高怪物血量
            EnemyHealthMultipler = 3;

            // 停止蜘蛛形怪物
            SpiderSpawner.SetActive(false);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.2f);

            // 生成弓箭手怪物
            ArcherSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);
        }
        // 当怪物还剩0，游戏胜利，停止生成怪物
        else if (CurrentEnemyRemain <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(BossPrefab, transform.position, transform.rotation);
        }
    }

    void Level2EnemySpawn()
    {
        // 当怪物还剩200 到 150
        if (CurrentEnemyRemain == 200)
        {
            EnemySpawner.MaxEnemyOnBoard = 50;

            // 生成蜘蛛形怪物
            SpiderSpawner.SetActive(true);
            SpiderSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);

        }
        // 当怪物还剩150 到 100
        else if (CurrentEnemyRemain == 150)
        {
            // 生成蜘蛛形怪物
            SpiderSpawner.SetActive(true);
            SpiderSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.4f);

        }
        // 当怪物还剩100 到 50
        else if (CurrentEnemyRemain == 100)
        {
            // 提高怪物血量(如果是从上一个level过来的)
            if(EnemyHealthMultipler >= 2)
            {
                EnemyHealthMultipler = 4;
            }

            // 停止生成蜘蛛形怪物
            SpiderSpawner.SetActive(false);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.3f);

            // 停止生成弓箭手怪物
            ArcherSpawner.SetActive(false);
        }
        // 当怪物还剩50 到 25
        else if (CurrentEnemyRemain == 50)
        {
            // 停止生成蜘蛛形怪物
            SpiderSpawner.SetActive(false);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.2f);

            // 停止生成弓箭手怪物
            ArcherSpawner.SetActive(false);
        }
        // 当怪物还剩25 到 0
        else if (CurrentEnemyRemain == 25)
        {
            // 提高怪物血量(如果是从上一个level过来的)
            if (EnemyHealthMultipler >= 2)
            {
                EnemyHealthMultipler = 5;
            }

            // 停止生成蜘蛛形怪物
            SpiderSpawner.SetActive(false);

            // 生成狼形怪物
            Debug.Log("Gethere");
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.1f);

            // 生成弓箭手怪物
            ArcherSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.5f);
        }
        // 当怪物还剩0，游戏胜利，停止生成怪物
        else if (CurrentEnemyRemain <= 0)
        {
            gameObject.SetActive(false);

            // 生成boss
            Instantiate(BossPrefab, transform.position, transform.rotation);
        }
    }

    void Level3EnemySpawn()
    {
        // 当怪物还剩300 到 200
        if (CurrentEnemyRemain == 300)
        {
            EnemySpawner.MaxEnemyOnBoard = 60;

            // 生成蜘蛛形怪物
            SpiderSpawner.SetActive(true);
            SpiderSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);

            // 生成弓箭手怪物
            ArcherSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(1f);

        }
        // 当怪物还剩200 到 100
        else if (CurrentEnemyRemain == 200)
        {
            // 提高怪物血量(如果是从上一个level过来的)
            if (EnemyHealthMultipler >= 2)
            {
                EnemyHealthMultipler = 6;
            }

            // 生成蜘蛛形怪物
            SpiderSpawner.SetActive(true);
            SpiderSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);

            // 生成弓箭手怪物
            ArcherSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);

        }
        // 当怪物还剩 100 到 50
        else if (CurrentEnemyRemain == 100)
        {

            // 停止生成蜘蛛形怪物
            SpiderSpawner.SetActive(false);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.1f);

            // 停止生成弓箭手怪物
            ArcherSpawner.SetActive(false);
        }
        // 当怪物还剩50 到 0
        else if (CurrentEnemyRemain == 50)
        {
            // 提高怪物血量(如果是从上一个level过来的)
            if (EnemyHealthMultipler >= 2)
            {
                EnemyHealthMultipler = 7;
            }

            // 停止生成蜘蛛形怪物
            SpiderSpawner.SetActive(false);

            // 生成狼形怪物
            WolfSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.1f);

            // 生成弓箭手怪物
            ArcherSpawner.SetActive(true);
            WolfSpawner.GetComponent<EnemySpawner>().UpdateSpwanTime(0.6f);
        }
        // 当怪物还剩0，游戏胜利，停止生成怪物
        else if (CurrentEnemyRemain <= 0)
        {
            gameObject.SetActive(false);

            // 生成boss
            Instantiate(BossPrefab, transform.position, transform.rotation);
        }
    }


}
