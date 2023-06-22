using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthCanvasBehavior : MonoBehaviour
{
    private Camera mainCamera;
    public Slider EnemyHealthSlider;
    public TextMeshProUGUI Damagetext;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);

        //如果三秒内未收到伤害就取消血条显示
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (EnemyHealthSlider.gameObject.activeSelf)
            {
                EnemyHealthSlider.gameObject.SetActive(false);
                Damagetext.text = "";
            }
        }
    }

    public void SetMax(int MaxHealth)
    {
        EnemyHealthSlider.maxValue = MaxHealth;
        EnemyHealthSlider.value = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        //显示血条
        EnemyHealthSlider.gameObject.SetActive(true);
        Damagetext.text = "" + damageAmount;
        EnemyHealthSlider.value = EnemyHealthSlider.value - damageAmount;
        timer = 3f;
    }
}
