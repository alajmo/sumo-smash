using UnityEngine;
using UnityEngine.AI;

using static Tools;

public class EnemyBrain : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 20;

    bool playerInRange;
    float timer;

    Animator anim;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent navMeshAgent;

    GameObject _target;
    GameObject target
    {
        get
        {
            return _target;
        }
        set
        {
            var previous = _target;
            _target = value;
            if (previous != _target && previous) onTargetReleased();
            if (previous != _target && value) onTargetAcquired();
        }
    }


    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        target = GetClosest(GetPlayers().OnlyAlive(), transform.position);
        if (!target) return;

        anim.SetTrigger("HaveTarget");
        MoveTowards(target);

        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack(target);
        }
    }

    void Attack(GameObject player)
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        if (!playerHealth || !playerHealth.isAlive) return;

        Debug.Log("Enemy attacked player " + player);
        Debug.Log(player + " attacked" + playerHealth.currentHealth);
        timer = 0f;
        playerHealth.TakeDamage(attackDamage);

        if (playerHealth.isDead)
        {
            OnPlayerKilled(player);
        }
    }

    // Events

    void OnPlayerKilled(GameObject player)
    {
        Debug.Log("Enemy killed player " + player);
        anim.SetTrigger("PlayerDied");
        target = null;
        playerInRange = false;
    }


    void onTargetAcquired()
    {
    }

    void onTargetReleased()
    {
        navMeshAgent.enabled = false;
    }

    void MoveTowards(GameObject target)
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(target.transform.position);
    }
}
