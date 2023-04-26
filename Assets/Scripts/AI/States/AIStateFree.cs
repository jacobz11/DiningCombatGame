using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.MLAgents;
using UnityEngine.AI;

namespace Assets.Scripts.AI.States
{
    internal class AIStateFree : StateFree
    {
        private NavMeshAgent m_Agent;

        public AIStateFree(AcitonStateMachine i_AcitonStateMachine, NavMeshAgent agent) 
            : base(i_AcitonStateMachine)
        {
            m_Agent = agent;
        }

        public override void Update()
        {

        }
    }
}
