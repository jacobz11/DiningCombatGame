using Assets.scrips.Player.Data;
using Assets.scrips.Player.States;
using Assets.Scripts.Manger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using UnityEngine;

namespace Assets.Scripts.AI.States
{
    internal class AIStatePowering : StatePowering
    {
        private NavMeshAgent m_Agent;
        private float m_Timer;
        private AIAcitonStateMachine m_AIAcitonState;
        private Vector3 m_Target;
        private Vector3 Position => m_Agent.transform.position;
        public AIStatePowering(AcitonStateMachine acitonStateMachine, PoweringData i_Powering, NavMeshAgent i_Agent) 
            : base(acitonStateMachine, i_Powering)
        {
            m_Agent = i_Agent;
        }

        private void FindPlayerClosest()
        {
            m_Timer = 0;
            m_Target = GameManger.Instance.GetPlayerPos(m_Agent.transform)
                .OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
            m_Agent.SetDestination(m_Target);
        }

        public float CalculateDistanceMoved()
        {
            //float acceleration = forceMagnitude / rb.mass;
            //float initialVelocity = rb.velocity.magnitude;
            //float finalVelocity = initialVelocity + acceleration * time;
            //float distanceMoved = (initialVelocity + finalVelocity) / 2 * time;

            //return distanceMoved;
            return 0.5f;

        }
    }
}
