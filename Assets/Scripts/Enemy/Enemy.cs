using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public string lightTag = "LightSpot";

    bool isInLight = false;

    [SerializeField] Transform target;

    NavMeshAgent agent;


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
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(lightTag))
        {
            isInLight = true;
            agent.isStopped = false; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(lightTag))
        {
            isInLight = false;
            agent.isStopped = true; 
        }
    }
}
