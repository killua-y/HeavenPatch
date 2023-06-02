using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float gravity = 9.81f;
    [HideInInspector] public Animator anim;
    CharacterController controller;
    Vector3 input, moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetInteger("States", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (Vector3.right * moveHorizontal + Vector3.forward * moveVertical).normalized;

        input *= moveSpeed;
        
        moveDirection = input;

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("States", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("States", 0);
        }
    }
}
