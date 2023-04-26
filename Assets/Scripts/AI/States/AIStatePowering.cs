using Assets.scrips.Player.Data;
using Assets.scrips.Player.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;

namespace Assets.Scripts.AI.States
{
    internal class AIStatePowering : StatePowering
    {
        private NavMeshAgent m_Agent;

        public AIStatePowering(AcitonStateMachine acitonStateMachine, PoweringData i_Powering, NavMeshAgent i_Agent) 
            : base(acitonStateMachine, i_Powering)
        {
            m_Agent = i_Agent;
        }
    }
}
