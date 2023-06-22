using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boss_Fire_AI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Chase,
        Attack,
        Fireball,
        Dead
    }

    public FSMStates currentState;

    public float attackDistance = 10;
    //public float chaseDistance = 10;
    public float enemySpeed = 5;
    public GameObject player;
    public GameObject[] spellProjectiles;
    public GameObject wandTip;
    public GameObject attackAlert;
    public float shootRate = 2;

    public GameObject fireballPrefab; // Attach your fireball prefab in Unity editor
    public float spawnTime = 30f; // Number of fireballs per second
    public int spawnNum = 2;
    public float spawnRange = 3f; // The range within which fireballs can spawn

    public NavMeshAgent agent;
    // public Transform enemyEyes;
    public float fieldfOfView = 45f;


    
    Vector3 nextDestination;
    Animator anim;
    float distanceToPlayer;
    float elapsedTime = 0;
    Transform deadTransform;

    Boss_Fire_Health bossHealth;
    int health;
    bool isDead;
    int attackMode;

    

    // Start is called before the first frame update
    void Start()
    {
        
        // wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();


        // wandTip = GameObject.FindGameObjectWithTag("WandTip");
        player = GameObject.FindGameObjectWithTag("Player");

        bossHealth = GetComponent<Boss_Fire_Health>();

        health = bossHealth.currentHealth;
        isDead = false;
        // Initialize();
         currentState = FSMStates.Chase;

        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("GetFireball", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(attackMode);
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        health = bossHealth.currentHealth;

        switch (currentState)
        {
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Fireball:
                UpdateFireballState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
            
        }

        elapsedTime += Time.deltaTime;
        if(health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    void GetFireball(){
        anim.SetInteger("animState", 4);
        for (int i = 0; i < spawnNum; i++)
            {
                // Get a random position within the specified range
                Vector3 spawnPosition = transform.position + new Vector3(
                    Random.Range(-spawnRange, spawnRange),
                    3,
                    Random.Range(-spawnRange, spawnRange)
                );

                // Instantiate the fireball at the random position
                Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
            }


        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance)
        {
            currentState = FSMStates.Chase;
        }
    }
    void UpdateChaseState()
    {
        // Debug.Log("Chasing!");

        anim.SetInteger("animState", 1);

        nextDestination = player.transform.position;

        agent.stoppingDistance = attackDistance;

        agent.speed = 5;

        if (distanceToPlayer <= attackDistance)
        {
            agent.speed = 0;
            currentState = FSMStates.Attack;
            
        }
        

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        
        nextDestination = player.transform.position;

        //agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {
            agent.speed = 0;
            currentState = FSMStates.Attack;

        }
        else if (distanceToPlayer > attackDistance)
        {
            currentState = FSMStates.Chase;
        }

        

        
        FaceTarget(nextDestination);
        
        anim.SetInteger("animState", 3);
        attackMode = Random.Range(0, 2);
            
        if(attackMode == 0){
            EnemySpellCast();
        } else if (attackMode == 1){
            EnemyFireballCast();
        }
        

        
    }


    void UpdateFireballState()
    {
        
        nextDestination = player.transform.position;

        //agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {   
            attackMode = Random.Range(0, 2);
            
            if(attackMode == 0){
                agent.speed = 0;

                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("ChangeStateToAttack", animDuration);
                elapsedTime = 0.0f;
                
                
            } else if (attackMode == 1){
                agent.speed = 0;
                currentState = FSMStates.Fireball;
            }

        }
        else if (distanceToPlayer > attackDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);
        anim.SetInteger("animState", 4);
        // EnemySpellCast();
        

        
    }

    void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);
        isDead = true;
        deadTransform = gameObject.transform;

        Destroy(gameObject, 3);
    }

    

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemySpellCast()
    {
        if ((elapsedTime >= shootRate) && (!isDead))
        {
            var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("SpellCasting", animDuration);
            elapsedTime = 0.0f;
        }

    }

    void SpellCasting()
    {
        int randProjectileIndex = Random.Range(0, spellProjectiles.Length);
        GameObject spellProjectile = spellProjectiles[randProjectileIndex];

        Instantiate(spellProjectile, wandTip.transform.position, wandTip.transform.rotation);
    }

    void EnemyFireballCast()
    {

        Vector3 spawnPosition = new Vector3(
                player.transform.position.x,
                0,
                player.transform.position.z
        );

        if ((elapsedTime >= shootRate) && (!isDead))
        {
            // Instantiate the fireball at the random position
            Instantiate(attackAlert, spawnPosition, transform.rotation);    
        }
        
    }
    

    void OnDestory()
    {
        // Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
    

}
