using DiningCombat.DataObject;
using DiningCombat.Manger;
using DiningCombat.Player;
using DiningCombat.Player.States;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DiningCombat.AI.States
{
    public class AIStatePowering : StatePowering
    {
        private const bool k_StopPowering = false;

        private float m_Timer;
        private int m_UpdateTimes;
        private Vector3 m_Target;
        private readonly NavMeshAgent r_Agent;
        private Vector3 Position => r_Agent.transform.position;
        public float FindPlayerClosestUpdateRate
        {
            get => m_Powering.m_UpdateRate * m_UpdateTimes;
            private set { m_UpdateTimes = (int)value; }
        }

        public AIStatePowering(ActionStateMachine acitonStateMachine, PoweringDataSo i_Powering, NavMeshAgent i_Agent)
            : base(acitonStateMachine, i_Powering)
        {
            r_Agent = i_Agent;
        }

        private void FindPlayerClosest()
        {
            m_UpdateTimes++;
            m_Target = GameManger.Instance.GetPlayerPos(r_Agent.transform).OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
            AIMatud.Seek(r_Agent, m_Target);
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
            ToProceedPowering();

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

        private void ToProceedPowering()
        {
            bool isOverMinPower = PowerCharging > m_Powering.m_MinPower;
            if (isOverMinPower)
            {
                float distance = Vector3.Distance(m_Target, r_Agent.transform.position);
                bool thereIsStillTime = m_Timer < m_Powering.m_MaxPoweringTime;
                bool isMoreThenMinDist = distance > m_Powering.m_MinDistanceToTarget;
                m_IsPowering = thereIsStillTime && isMoreThenMinDist;
            }
        }
    }
}