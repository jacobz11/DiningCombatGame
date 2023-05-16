using DiningCombat;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// TODO : to fix the namespace
namespace Assets.Scripts.AI
{
    internal class AIControl : MonoBehaviour
    {
        private GameObject[] m_GoalLocations;
        private NavMeshAgent m_Agent;
        private const float k_ArrivalDistanceToTheDestination = 1.0f;

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_GoalLocations = GameObject.FindGameObjectsWithTag(GameGlobal.TagNames.k_Environment);
            _ = m_Agent.SetDestination(GetRendGoalLocations());
        }

        private void Update()
        {
            if (m_Agent.remainingDistance < k_ArrivalDistanceToTheDestination)
            {
                _ = m_Agent.SetDestination(GetRendGoalLocations());
            }
        }

        private Vector3 GetRendGoalLocations()
        {
            return m_GoalLocations[Random.Range(0, m_GoalLocations.Length)].transform.position;
        }
    }
}
