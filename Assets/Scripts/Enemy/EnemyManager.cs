
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
    public bool allowParry;

    public int blockPercent = 50;
    public int dodgePercent = 50;
    public int parryPercent = 50;
   
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
            w.GetComponent<Rigidbody>().AddExplosionForce(5, w.transform.position, 5f, 7f, ForceMode.Impulse);
        }
        Destroy(this);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Dead();
    }

    public void SetDamageAbsorption()
    {}
}
