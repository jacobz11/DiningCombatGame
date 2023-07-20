using DiningCombat.Manger;
using DiningCombat.Player.States;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DiningCombat.AI.States
{
    public class AIStateThrowing : StateThrowing
    {
        private const float k_RotationSpeed = 5;

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

            m_Target = GameManger.Instance.GetPlayerPos(transform)
                .OrderBy(v => Vector3.Distance(pos, v)).
                FirstOrDefault();
            AIMethods.Seek(r_Agent, m_Target);

            Vector3 targetDir = m_Target - transform.position;
            targetDir.y = 0.0f;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * k_RotationSpeed);
        }

        public override void Update()
        {
            FindPlayerClosest();
        }
    }
}