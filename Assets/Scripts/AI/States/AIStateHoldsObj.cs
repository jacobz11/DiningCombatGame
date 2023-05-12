using Assets.scrips.Player.States;
using Assets.Scripts.Manger;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI.States
{
    internal class AIStateHoldsObj : StateHoldsObj
    {
        private const float k_UpdateRate = 3;
        private const float k_MinDistance = 30;

        private float m_Timer;

        private NavMeshAgent m_Agent;
        private AIAcitonStateMachine m_AIAcitonState;
        private Vector3 m_Target;
        private Vector3 Position => m_Agent.transform.position;

        public AIStateHoldsObj(NavMeshAgent i_Agent, AIAcitonStateMachine aIAcitonState) : base()
        {
            this.m_Agent = i_Agent;
            m_AIAcitonState = aIAcitonState;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            FindPlayerClosest();
        }

        private void FindPlayerClosest()
        {
            m_Timer = 0;
            m_Target = GameManger.Instance.GetPlayerPos(m_Agent.transform).OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
            m_Agent.SetDestination(m_Target);
        }

        public override void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= k_UpdateRate)
            {
                FindPlayerClosest();
            }
            if (Vector3.Distance(m_Target, Position) < k_MinDistance)
            {
                m_AIAcitonState.GameInput_OnStartChargingAction(this, System.EventArgs.Empty);
            }
        }
    }
}
