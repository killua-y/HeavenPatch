using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RemoteAttackEnemy1_Behavior : MonoBehaviour
{
    public enum FSMStates{
            Idle,
            Chase,
            Attack
        }

    public float attackRange = 6f;

    public float moveSpeed = 3f;

    public GameObject player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public FSMStates currentState;
    NavMeshAgent agent;

    private float attackCooldown = 2f;  
    private float distance;
    bool canAttack = false;

    Animator anim;

    private void Start()
    {
        currentState = FSMStates.Chase;
        player = GameObject.FindGameObjectWithTag("Player"); 
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Attack", 1f, attackCooldown);
    }

    private void Update()
    {
        if (!LevelManager.isGameOver)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            CanAttackPlayer();

            switch(currentState){
            
                case FSMStates.Chase:
                    UpdateChaseState();
                    break;
                case FSMStates.Attack:
                    UpdateAttackState();
                    break;
            }
        }
    }

    void UpdateChaseState(){
        canAttack = false;
        transform.LookAt(player.transform);
        anim.SetInteger("animState", 1);
        float step = moveSpeed * Time.deltaTime;
        
        if(distance <= attackRange - 1){
            //Debug.Log("In the range now!");
            currentState = FSMStates.Attack;          
        }

        agent.SetDestination(player.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        
    }

    void UpdateAttackState(){
        //print("attack!");
        transform.LookAt(player.transform);
        canAttack = true;
        if (distance <= attackRange)
        {
            currentState = FSMStates.Attack;
        } else if (distance > attackRange) {
            currentState = FSMStates.Chase;
        }
    }

    private bool CanAttackPlayer()
    {
        if (player == null){
            return false;
        }
        return distance <= attackRange;
    }

    private void Attack()
    {
        if(canAttack){
            anim.SetInteger("animState", 2);
            //Debug.Log("Can attack now!");
            Vector3 attackPointPosition = transform.position + transform.forward;
            GameObject bulletInstance = Instantiate(
            bulletPrefab, attackPointPosition, Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90f, 0f)) as GameObject;
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
        }
        
    }

    private void ResetAttackCooldown()
    {
        

    }
}
