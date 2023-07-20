using DiningCombat.Manger;
using DiningCombat.Player.States;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DiningCombat.AI.States
{
    public class AIStateHoldsObj : StateHoldsObj
    {
        private const float k_UpdateRate = 3;
        private const float k_MinDistance = 30;

        private float m_Timer;
        private readonly NavMeshAgent r_Agent;
        private readonly AIAcitonStateMachine r_AIAcitonStateMachine;
        private Vector3 m_Target;
        private Vector3 Position => r_Agent.transform.position;

        public AIStateHoldsObj(NavMeshAgent i_Agent, AIAcitonStateMachine i_AIAcitonState) : base()
        {
            r_Agent = i_Agent;
            r_AIAcitonStateMachine = i_AIAcitonState;
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
            AIMethods.Seek(r_Agent, m_Target);
        }

        public override void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= k_UpdateRate)
            {
                FindPlayerClosest();
            }

            if (Vector3.Distance(Position, m_Target) < k_MinDistance)
            {
                r_AIAcitonStateMachine.GameInput_OnStartChargingAction(this, System.EventArgs.Empty);
            }
        }
    }
}