
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
    public LayerMask groundLayer, detectionLayer;
    public Rigidbody rigidbody;
    public State currentState;
    public StatsManager currentTarget;
    public float health;
    public bool isPerformingAction;
    public float rotationSpeed = 15;

    public float currentRecoveryTime = 0;

    public float detectionRadius;
    public float maximumAttackRange = 40f;

    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;
    
    public WeaponType weaponType;
    public GameObject[] weapon;
   
    private void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
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
}
