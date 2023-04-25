using Assets.DataObject;
using System;
using UnityEngine.AI;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    internal class UncollectStateCorn : UncollectState
    {
        public event Action OnCountdownEnding;
        private const float k_CountdownInitial = 65f;
        private float m_Countdown;
        private NavMeshAgent m_Agent;
        private RoomDimension m_RoomDimension;
        public UncollectStateCorn(GameFoodObj gameFood, NavMeshAgent i_Agent, RoomDimension room) : base(gameFood)
        {
            m_Agent = i_Agent;
            m_RoomDimension = room;
        }

        public override void Update()
        {
            m_Countdown -= Time.deltaTime;
            if (m_Countdown >= 0) 
            {
                OnCountdownEnding?.Invoke();
            }
            if (ReachTheDestination())
            {
                m_Agent.SetDestination(m_RoomDimension.GetRendonPos());
            }
        }

        private bool ReachTheDestination()
        {
            if (!m_Agent.pathPending)
            {
                if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                {
                    if (!m_Agent.hasPath || m_Agent.velocity.sqrMagnitude == 0f)
                    {
                        Debug.Log("ReachTheDestination");
                        return true;
                    }
                }
            }
            return false;
        }

        public override void OnSteteEnter()
        {
            m_Countdown = k_CountdownInitial;
            m_Agent.SetDestination(m_RoomDimension.GetRendonPos());
        }

        public override void OnSteteExit()
        {
            Debug.Log("OnSteteExit corn");
            m_Agent.isStopped = true;
        }

    }
}
