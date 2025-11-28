using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string lightTag = "LightSpot";
    public float attackInterval = 2f;
    public float enemyDamage = 10f;
    public float enemyHealth = 50f;
    private bool isInLight = false;

    [SerializeField] Transform target;
    private NavMeshAgent agent;

    private bool isAttacking = false;         
    private Coroutine attackCoroutine = null; 

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isInLight)
        {
            agent.SetDestination(target.position);
        }

        bool canAttack =
            isInLight &&
            agent.remainingDistance <= agent.stoppingDistance &&
            !agent.pathPending &&
            agent.velocity.magnitude < 0.05f; 

        if (canAttack && !isAttacking)
        {
            attackCoroutine = StartCoroutine(AttackRepeating(attackInterval));
            isAttacking = true;
        }
        else if (!canAttack && isAttacking)
        {
            StopCoroutine(attackCoroutine);
            isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(lightTag))
        {
            isInLight = true;

            if (agent != null && agent.isOnNavMesh)
                agent.isStopped = false;
            
        }
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(10f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(lightTag))
        {
            isInLight = false;

            if (agent != null && agent.isOnNavMesh)
                agent.isStopped = true;

            if (isAttacking)
            {
                StopCoroutine(attackCoroutine);
                isAttacking = false;
            }
        }
    }

    private void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AttackRepeating(float interval)
    {
        Player player = target.GetComponent<Player>();

        while (true)
        {
            player.TakeDamage(10f);
            yield return new WaitForSeconds(interval);
        }
    }
}
