using Assets.scrips.Player.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;

namespace Assets.Scripts.AI.States
{
    internal class AIStateThrowing : StateThrowing
    {
        private NavMeshAgent m_Agent;

        public AIStateThrowing(NavMeshAgent i_Agent) : base()
        {
            m_Agent = i_Agent;
        }
    }
}
