
using UnityEngine;
using UnityEngine.AI;

public enum WeaponType
{
    Spear,
    Sword,
    Shield
}


public class EnemyManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyAnimatorManager enemyAnimatorManager;
    public Rigidbody rigidbody;
    public State currentState;
    public StatsManager currentTarget;
    public EnemyCombatManager combatManager;
    public float health;
    public float damageReduction = 0f;
    public bool isPerformingAction;
    public float rotationSpeed = 360;
    public float currentRecoveryTime = 0;
    public float combatTimer = 0;

    public float detectionRadius;
    public float maximumAttackRange = 2.5f;
    public float maximumAggroRange = 5f;

    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;
    
    public WeaponType weaponType;
    public GameObject[] weapon;

    public bool isBlocking;
    public bool allowBlock;
    public bool allowDodge;

    public int blockPercent = 50;
    public int dodgePercent = 50;
   
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        combatManager = GetComponent<EnemyCombatManager>();
        rigidbody = GetComponent<Rigidbody>();
        agent.enabled = false;
    }

    void Start()
    {
        rigidbody.isKinematic = false;
    }

    void Update()
    {
        HandleRecoveryTimer();
        isPerformingAction = enemyAnimatorManager.animator.GetBool("isInteracting");
        agent.speed = enemyAnimatorManager.animator.GetInteger("agentSpeed");

        if(!enemyAnimatorManager.animator.GetBool("isBlocking"))
            damageReduction = 0f;
    }

    void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if(currentState != null)
        {
            State nextState = currentState.Tick(this, enemyAnimatorManager);

            if(nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }
    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTimer()
    {
        if(currentRecoveryTime > 0)
            currentRecoveryTime -= Time.deltaTime;

        if(isPerformingAction)
        {
            if(currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Dead()
    {
        GetComponent<RagdollController>().EnableRagdoll();
        Destroy(agent);
        foreach(GameObject w in weapon){
            w.AddComponent<Rigidbody>();
            w.GetComponent<Rigidbody>().AddExplosionForce(7.5f, w.transform.position, 5f, 7f, ForceMode.Impulse);
        }
        Destroy(this);
    }

    public void TakeHit(float damage)
    {
        if(damageReduction == 1)
            enemyAnimatorManager.animator.CrossFade("ShieldBash", .2f);
        else
        {
            if(!isBlocking){
                TakeDamage(damage);
                enemyAnimatorManager.animator.CrossFade("Damage", .2f);
            }
            else
                TakeDamage(damage * damageReduction);
        }
    }


    public void TakeDamage(float damage)
    {
        if(GetComponent<EnemyDamageCollider>())
            GetComponent<EnemyDamageCollider>().DisableDamageCollider();
            
        health -= damage;
        if (health <= 0) Dead();
    }

    public void SetDamageAbsorption(float percent)
    {
        damageReduction = percent / 100f;
    }
}
