using DiningCombat.AI;
using DiningCombat.Environment;
using DiningCombat.FoodObject;
using System;
using UnityEngine;
using UnityEngine.AI;
namespace DiningCombat.NPC
{
    public class UncollectStateCorn : UncollectState
    {
        private const float k_CountdownInitial = 65f;

        public event Action OnCountdownEnding;
        private float m_Countdown;
        private Vector3 m_Target;
        private NavMeshAgent m_Agent;
        private FollowWP m_Follow;
        private Room m_RoomDimension;

        public UncollectStateCorn(GameFoodObj gameFood, NavMeshAgent i_Agent, Room i_Room, FollowWP i_Follow) : base(gameFood)
        {
            m_Agent = i_Agent;
            m_RoomDimension = i_Room;
            m_Follow = i_Follow;
        }

        public override void Update()
        {
            m_Countdown -= Time.deltaTime;
            if (m_Countdown <= 0)
            {
                OnCountdownEnding?.Invoke();
            }

            if (ReachTheDestination())
            {
                Debug.Log("ReachTheDestination");
                SetDestination();
            }
        }

        private void SetDestination()
        {
            m_Target = m_RoomDimension.GetRendonPos();
            m_Agent.SetDestination(m_Target);
        }

        private bool ReachTheDestination()
        {
            bool res = false;
            if (!m_Agent.pathPending)
            {
                if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                {
                    if (!m_Agent.hasPath || m_Agent.velocity.sqrMagnitude == 0f)
                    {
                        Debug.Log("ReachTheDestination");
                        res = true;
                    }
                }
            }

            return res;
        }

        public override void OnStateEnter()
        {
            m_Countdown = k_CountdownInitial;
            SetDestination();
        }

        public override void OnStateExit()
        {
            m_Agent.isStopped = true;
        }
    }
}