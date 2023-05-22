using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private GameObject[] m_GoalLocations;
    private NavMeshAgent m_Agent;
    private Animator m_Anim;
    private float m_SpeedMult;
    private readonly float r_DetectionRadius = 20.0f;
    private readonly float r_FleeRadius = 10.0f;

    void Start()
    {

        m_Agent = GetComponent<NavMeshAgent>();
        m_GoalLocations = GameObject.FindGameObjectsWithTag("goal");
        int i = Random.Range(0, m_GoalLocations.Length);
        _ = m_Agent.SetDestination(m_GoalLocations[i].transform.position);
        m_Anim = this.GetComponent<Animator>();
        m_Anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
        ResetAgent();
    }

    void ResetAgent()
    {

        m_SpeedMult = Random.Range(0.1f, 1.5f);
        m_Anim.SetFloat("speedMult", m_SpeedMult);
        m_Agent.speed *= m_SpeedMult;
        m_Anim.SetTrigger("isWalking");
        m_Agent.angularSpeed = 120.0f;
        m_Agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 position)
    {

        if (Vector3.Distance(position, this.transform.position) < r_DetectionRadius)
        {

            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newGoal = this.transform.position + (fleeDirection * r_FleeRadius);

            NavMeshPath path = new NavMeshPath();
            _ = m_Agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                _ = m_Agent.SetDestination(path.corners[^1]);
                m_Anim.SetTrigger("isRunning");
                m_Agent.speed = 10.0f;
                m_Agent.angularSpeed = 500.0f;
            }
        }
    }

    void Update()
    {

        if (m_Agent.remainingDistance < 1.0f)
        {

            ResetAgent();
            int i = Random.Range(0, m_GoalLocations.Length);
            _ = m_Agent.SetDestination(m_GoalLocations[i].transform.position);
        }
    }
}