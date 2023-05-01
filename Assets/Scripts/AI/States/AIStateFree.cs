using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.MLAgents;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.AI.States
{
    internal class AIStateFree : StateFree
    {
        private NavMeshAgent m_Agent;
        private Vector3 m_Target;

        public bool TargetExist { get; private set; }

        private Vector3 Position => m_Agent.transform.position;

        public AIStateFree(AcitonStateMachine i_AcitonStateMachine, NavMeshAgent agent) 
            : base(i_AcitonStateMachine)
        {
            m_Agent = agent;
        }

        public override void OnSteteEnter()
        {
            base.OnSteteEnter();
            FindTarget();
        }
        
        private void FindTarget()
        {
            List<Vector3> all = ManagerGameFoodObj.Instance.GetAllUncollcted();
            if (all.Count == 0) {
                m_Target = Vector3.zero;
                TargetExist = false;
            }
            else
            {
                TargetExist = true;
                m_Target = all.OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();

            }
            m_Agent.SetDestination(m_Target);
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

        internal void OnCollcatedAnyFood()
        {
            TargetExist = false;
        }
    }
}
