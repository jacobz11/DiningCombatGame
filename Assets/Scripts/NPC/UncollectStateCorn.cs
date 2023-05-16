﻿using System;
using UnityEngine;
using UnityEngine.AI;
// TODO : to fix the namespace
namespace Assets.Scripts.NPC
{
    internal class UncollectStateCorn : UncollectState
    {
        private const float k_CountdownInitial = 65f;

        public event Action OnCountdownEnding;
        private float m_Countdown;
        private Vector3 m_Target;
        private NavMeshAgent m_Agent;
        private Room m_RoomDimension;

        public UncollectStateCorn(GameFoodObj gameFood, NavMeshAgent i_Agent, Room room) : base(gameFood)
        {
            m_Agent = i_Agent;
            m_RoomDimension = room;
        }

        public override void Update()
        {
            //Debug.Log("pos :" + m_GameObjects[m_GameObjectsCount].transform.position + "m_GameObjectsCount " + m_GameObjectsCount);
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

            //m_Agent.gameObject.transform.LookAt(m_Target);
            OnDrawGizmos();
        }

        private void OnDrawGizmos()
        {
            if (m_Agent.hasPath)
            {
                Vector3[] path = m_Agent.path.corners;
                for (int i = 0; i < path.Length - 1; i++)
                {
                    Debug.DrawLine(path[i], path[i + 1], Color.blue);
                }
            }
        }

        private void SetDestination()
        {
            m_Target = m_RoomDimension.GetRendonPos();
            Debug.Log("SetDestination " + m_Target);
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
            Debug.Log("OnSteteExit corn");
            m_Agent.isStopped = true;
        }

    }
}
