using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string lightTag = "LightSpot";
    public float attackInterval = 2f;
    public float enemyDamage = 10f;
    public float enemyHealth = 50f;
    public SpriteRenderer enemySpriteRenderer;
    public GameObject exclamationMark;
    private bool isInLight = false;

    [SerializeField] Transform target;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    private bool isAttacking = false;         
    private Coroutine attackCoroutine = null; 
    private Coroutine alertCoroutine = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            RotateTowardsPlayer();
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
        else if (!canAttack && isAttacking && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            isAttacking = false;
        }

    }

    private void LateUpdate()
    {
        if (exclamationMark != null)
            exclamationMark.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(lightTag))
        {
            isInLight = true;
            audioSource.Play();

            if (agent != null && agent.isOnNavMesh)
                agent.isStopped = false;
            
            if (alertCoroutine == null)
                alertCoroutine = StartCoroutine(ShowAlert(1f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10f);
            StartCoroutine(HitFlash(0.3f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(lightTag))
        {
            isInLight = false;
            audioSource.Pause();
            
            if (agent != null && agent.isOnNavMesh)
                agent.isStopped = true;

            if (isAttacking)
            {
                StopCoroutine(attackCoroutine);
                isAttacking = false;
            }
            
            exclamationMark.SetActive(false);

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

    private void RotateTowardsPlayer()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 10f);
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
    private IEnumerator HitFlash(float duration)
    {
        Color originalColor = enemySpriteRenderer.color;
        enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        enemySpriteRenderer.color = originalColor;
    }

    private IEnumerator ShowAlert(float duration)
    {
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(duration);
        exclamationMark.SetActive(false);
        alertCoroutine = null;
    }

}
