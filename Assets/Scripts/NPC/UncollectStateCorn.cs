using Assets.DataObject;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.NPC
{
    internal class UncollectStateCorn : UncollectState
    {
        private const float k_CountdownInitial = 65f;

        public event Action OnCountdownEnding;
        private float m_Countdown;
        private int m_GameObjectsCount;
        private NavMeshAgent m_Agent;
        private RoomDimension m_RoomDimension;
        private GameObject[] m_GameObjects;

        public UncollectStateCorn(GameFoodObj gameFood, NavMeshAgent i_Agent, RoomDimension room, GameObject[] gameObjects) : base(gameFood)
        {
            m_Agent = i_Agent;
            m_RoomDimension = room;
            m_GameObjects = gameObjects;
            Debug.Log("m_GameObjects.Length " + m_GameObjects.Length);
        }

        public override void Update()
        {
            Debug.Log("pos :" + m_GameObjects[m_GameObjectsCount].transform.position + "m_GameObjectsCount " + m_GameObjectsCount);
            m_Countdown -= Time.deltaTime;
            if (m_Countdown >= 0)
            {
                OnCountdownEnding?.Invoke();
            }

            if (ReachTheDestination())
            {
                m_GameObjectsCount++;
                if (m_GameObjectsCount >= m_GameObjects.Length)
                {
                    m_GameObjectsCount = 0;
                }
                m_Agent.SetDestination(m_GameObjects[m_GameObjectsCount].transform.position);
            }
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

        public override void OnSteteEnter()
        {
            m_Countdown = k_CountdownInitial;
            m_GameObjectsCount = 0;
            m_Agent.SetDestination(m_GameObjects[m_GameObjectsCount].transform.position);
        }

        public override void OnSteteExit()
        {
            Debug.Log("OnSteteExit corn");
            m_Agent.isStopped = true;
        }

    }
}
