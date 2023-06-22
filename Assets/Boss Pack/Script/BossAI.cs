using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public enum FSMStates{
            Idle,
            Chase,
            AttackMagic,
            AttackNormal,
            die
        }

    public float attackMagicRange = 5f;
    public float attackNormalRange = 2f;
    public float ChaseRange = 10f;
    public GameObject circlePrefab;
    public float moveSpeed = 3f;
    public Transform player;
    float timer = 0f;
float delay = 3.8f;
bool isTimerRunning = false;


    //public GameObject player;
    NavMeshAgent agent;
    public FSMStates currentState;

    

    private float attackCooldown = 3f;  
    private float distance;
    bool canAttack = false;
    public int bossCurrentHealth;

    Animator anim;

    private void Start()
    {
        currentState = FSMStates.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SpawnCircle", 0.5f, attackCooldown);
        
    }

    private void Update()
    {           
        transform.LookAt(player);
        distance = Vector3.Distance(transform.position, player.transform.position);
        //CanAttackPlayer();
        bossCurrentHealth = GetComponent<Boss_Fire_Health>().currentHealth;
        if(bossCurrentHealth <= 0){
            currentState = FSMStates.die;
        }

        switch(currentState){
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.AttackMagic:
                UpdateAttackMagicState();
                break;
            case FSMStates.AttackNormal:
                UpdateNormalAttackState();
                break;    
            case FSMStates.die:
                UpdateDieState();
                break;
        }

        if (isTimerRunning)
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            
            PerformDelayedAction();

        
            timer = 0f;
            isTimerRunning = false;
        }
    }
    }

    void UpdateIdleState(){
        canAttack = false;
        anim.SetInteger("animState", 0 );
        //Debug.Log(distance);
        if(distance < ChaseRange){
            currentState = FSMStates.Chase;
        }else{
            currentState = FSMStates.Idle;
        }

    }


    void UpdateChaseState(){
        canAttack = false;
        transform.LookAt(player.transform);
        anim.SetInteger("animState", 1);
        float step = moveSpeed * Time.deltaTime;
        
        if(distance < attackMagicRange){
            //.Log("In the range now!");
            currentState = FSMStates.AttackMagic;          
        }
        agent.SetDestination(player.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        
    }

    void UpdateAttackMagicState(){
        print("magic attack!");
        transform.LookAt(player.transform);
        anim.SetInteger("animState",2);
        canAttack = true;
        //InvokeRepeating("SpawnCircle", 0f, 3f);

        if (distance < attackMagicRange && distance > attackNormalRange )
         {
            currentState = FSMStates.AttackMagic;
         } else if (distance < attackNormalRange) {
        currentState = FSMStates.AttackNormal;
        } else{
            currentState = FSMStates.Chase;
        }
        //isTimerRunning = true;
    }

    void UpdateNormalAttackState(){
        print("normal attack");
        int randomAnim = Random.Range(0, 2);
        canAttack = false;

        if (randomAnim == 0)
    {
        anim.SetInteger("animState", 3); 
    }
    else
    {
        anim.SetInteger("animState", 5); 
    }

        if (distance <= attackNormalRange )
        {
            currentState = FSMStates.AttackNormal;
        }else{
            currentState = FSMStates.AttackMagic;
        }

    }
    void SpawnCircle()
{
    if(canAttack == true){
    Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
    playerPosition.y = 0;
    GameObject circle = Instantiate(circlePrefab, playerPosition, Quaternion.identity);
    }
}

    void UpdateDieState(){
        anim.SetInteger("animState", 4); 
        Destroy(gameObject, 4f);
    }


    void PerformDelayedAction()
{
    if (distance < attackMagicRange && distance > attackNormalRange)
    {
        currentState = FSMStates.AttackMagic;
    }
    else if (distance < attackNormalRange)
    {
        currentState = FSMStates.AttackNormal;
    }
    else
    {
        currentState = FSMStates.Chase;
    }
}

    // private bool CanAttackPlayer()
    // {
    //     if (player == null){
    //         return false;
    //     }
    //     return distance <= attackRange;
    // }

    // private void Attack()
    // {
    //     if(canAttack){
    //         anim.SetInteger("animState", 2);
    //         Debug.Log("Can attack now!");
            
    //     }
        
    // }

    // private void ResetAttackCooldown()
    // {
        

    // }
}
