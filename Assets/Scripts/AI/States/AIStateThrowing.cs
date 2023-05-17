using DiningCombat.Manger;
using DiningCombat.Player.States;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DiningCombat.AI.States
{
    public class AIStateThrowing : StateThrowing
    {
        private const float k_Speed = 5;

        private Vector3 m_Target;

        private readonly NavMeshAgent r_Agent;

        public AIStateThrowing(NavMeshAgent i_Agent) : base()
        {
            r_Agent = i_Agent;
        }

        private void FindPlayerClosest()
        {
            Transform transform = r_Agent.transform;
            Vector3 pos = transform.position;

            m_Target = GameManger.Instance.GetPlayerPos(transform).OrderBy(v => Vector3.Distance(pos, v)).FirstOrDefault();
            _ = r_Agent.SetDestination(m_Target);

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