using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashController : MonoBehaviour
{
    public Image dashIconMask;
    CharacterController controller;
    public float dashSpeed;
    float dashTime = 0.2f;
    Vector3 Direction;

    public float dashCoolDown;
    float timer;
    bool isCooling = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooling)
        {
            timer += Time.deltaTime;
            dashIconMask.fillAmount = (dashCoolDown - timer) / dashCoolDown;

            if (timer >= dashCoolDown)
            {
                isCooling = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isCooling)
            {
                Direction = gameObject.transform.forward;
                StartCoroutine(Dash());
                timer = 0;
                isCooling = true;
                dashIconMask.fillAmount = 1;
            }
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            controller.Move(Direction * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
