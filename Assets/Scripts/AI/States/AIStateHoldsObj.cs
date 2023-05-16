using Assets.scrips.Player.States;
using Assets.Scripts.Manger;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

// TODO : to fix the namespace
namespace Assets.Scripts.AI.States
{
    internal class AIStateHoldsObj : StateHoldsObj
    {
        private const float k_UpdateRate = 3;
        private const float k_MinDistance = 30;

        private float m_Timer;

        private readonly NavMeshAgent r_Agent;
        private readonly AIAcitonStateMachine r_AIAcitonState;
        private Vector3 m_Target;
        private Vector3 Position => r_Agent.transform.position;

        public AIStateHoldsObj(NavMeshAgent i_Agent, AIAcitonStateMachine aIAcitonState) : base()
        {
            this.r_Agent = i_Agent;
            r_AIAcitonState = aIAcitonState;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            FindPlayerClosest();
        }

        private void FindPlayerClosest()
        {
            m_Timer = 0;
            m_Target = GameManger.Instance.GetPlayerPos(r_Agent.transform).OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
            _ = r_Agent.SetDestination(m_Target);
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
                r_AIAcitonState.GameInput_OnStartChargingAction(this, System.EventArgs.Empty);
            }
        }
    }
}
