using Assets.scrips.Player.Data;
using Assets.scrips.Player.States;
using Assets.Scripts.Manger;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

// TODO : to fix the namespace
namespace Assets.Scripts.AI.States
{
    internal class AIStatePowering : StatePowering
    {
        private const bool k_StopPowering = false;
        private const float k_PoweringTime = 3.0f;
        private const float k_UpdateRate = 1.5f;
        private const float k_MinDistanceToTarget = 7f;

        private float m_Timer;
        private int m_UpdateTimes;
        private Vector3 m_Target;
        private NavMeshAgent m_Agent;
        private Vector3 Position => m_Agent.transform.position;
        public float FindPlayerClosestUpdateRate
        {
            get => k_UpdateRate * m_UpdateTimes;
            private set { m_UpdateTimes = (int)value; }
        }

        public AIStatePowering(ActionStateMachine acitonStateMachine, PoweringData i_Powering, NavMeshAgent i_Agent)
            : base(acitonStateMachine, i_Powering)
        {
            m_Agent = i_Agent;
        }

        private void FindPlayerClosest()
        {
            m_UpdateTimes++;
            m_Target = GameManger.Instance.GetPlayerPos(m_Agent.transform).OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
            m_Agent.SetDestination(m_Target);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            FindPlayerClosest();
            m_UpdateTimes = 0;
            m_Timer = 0;
        }

        public override void Update()
        {
            m_Timer += Time.deltaTime;
            m_IsPowering = ToProceedPowering();

            if (!m_IsPowering)
            {
                OnChargingAction = k_StopPowering;
                return;
            }

            if (m_Timer > FindPlayerClosestUpdateRate)
            {
                FindPlayerClosest();
            }
            base.Update();
        }

        private bool ToProceedPowering()
        {
            return m_Timer < k_PoweringTime && Vector3.Distance(m_Target, m_Agent.transform.position) > k_MinDistanceToTarget;
        }

        public float CalculateDistanceMoved()
        {
            //float acceleration = forceMagnitude / rb.mass;
            //float initialVelocity = rb.velocity.magnitude;
            //float finalVelocity = initialVelocity + acceleration * time;
            //float distanceMoved = (initialVelocity + finalVelocity) / 2 * time;

            //return distanceMoved;
            return 0.5f;
        }
    }
}