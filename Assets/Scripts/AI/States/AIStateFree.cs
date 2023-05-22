using DiningCombat.Manger;
using DiningCombat.Player;
using DiningCombat.Player.States;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DiningCombat.AI.States
{
    public class AIStateFree : StateFree
    {
        private readonly NavMeshAgent r_Agent;
        private Vector3 m_Target;
        public bool TargetExist { get; private set; }
        private Vector3 Position => r_Agent.transform.position;

        public AIStateFree(ActionStateMachine i_AcitonStateMachine, NavMeshAgent agent)
            : base(i_AcitonStateMachine)
        {
            r_Agent = agent;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            FindTarget();
        }

        public override void Update()
        {
            if (!TargetExist)
            {
                FindTarget();
            }

            if (HaveGameObject)
            {
                m_AcitonStateMachine.GameInput_OnPickUpAction(this, System.EventArgs.Empty);
            }

            if (Vector3.Distance(Position, m_Target) < 1.5f)
            {
                FindTarget();
            }
        }

        private void FindTarget()
        {
            List<Vector3> all = ManagerGameFoodObj.Instance.GetAllUncollcted();

            if (all.Count == 0)
            {
                m_Target = Vector3.zero;
                TargetExist = false;
            }
            else
            {
                m_Target = all.OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
                TargetExist = m_Target != Position;
            }

            _ = r_Agent.SetDestination(m_Target);
        }

        public void OnCollcatedAnyFood()
        {
            TargetExist = false;
        }
    }
}