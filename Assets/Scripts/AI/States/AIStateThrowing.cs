using Assets.scrips.Player.States;
using Assets.Scripts.Manger;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI.States
{
    internal class AIStateThrowing : StateThrowing
    {
        private const float k_Speed = 5;

        private Vector3 m_Target;

        private NavMeshAgent m_Agent;

        public AIStateThrowing(NavMeshAgent i_Agent) : base()
        {
            m_Agent = i_Agent;
        }

        private void FindPlayerClosest()
        {
            Transform transform = m_Agent.transform;
            Vector3 pos = transform.position;

            m_Target = GameManger.Instance.GetPlayerPos(transform).OrderBy(v => Vector3.Distance(pos, v)).FirstOrDefault();
            m_Agent.SetDestination(m_Target);

            Vector3 lTargetDir = m_Target - transform.position;
            lTargetDir.y = 0.0f;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * k_Speed);
        }

        public override void Update()
        {
            FindPlayerClosest();
        }
    }
}