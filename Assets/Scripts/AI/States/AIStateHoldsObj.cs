using Assets.scrips.Player.States;
using UnityEngine.AI;

namespace Assets.Scripts.AI.States
{
    internal class AIStateHoldsObj : StateHoldsObj
    {
        NavMeshAgent m_Agent;
        public AIStateHoldsObj(NavMeshAgent i_Agent) : base()
        {
            this.m_Agent = i_Agent;
        }
    }
}
