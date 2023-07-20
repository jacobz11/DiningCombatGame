using DiningCombat.Manger;
using DiningCombat.Player;
using DiningCombat.Player.States;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace DiningCombat.AI.States
{
    public class AIStateFree : StateFree
    {
        private const bool k_AiIsStuck = false;
        private float[] m_Lest10Moves = new float[10];
        private Vector3 m_LestPos;
        private Vector3 m_WanderTarget;
        private Vector3 m_Target;
        private readonly NavMeshAgent r_Agent;

        public bool TargetExist { get; private set; }
        private Vector3 Position => r_Agent.transform.position;

        public AIStateFree(ActionStateMachine i_ActionStateMachine, NavMeshAgent i_Agent)
            : base(i_ActionStateMachine)
        {
            r_Agent = i_Agent;
            m_WanderTarget = Vector3.one;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            FindTarget();
        }

        public override void Update()
        {
            if (!TargetExist || Vector3.Distance(Position, m_Target) < 1.5f)
            {
                FindTarget();
            }
            else if (HaveGameObject)
            {
                m_AcitonStateMachine.GameInput_OnPickUpAction(this, System.EventArgs.Empty);
            }
            else
            {
                HaveAnyMovmentInLest10Update();
            }
        }

        private void HaveAnyMovmentInLest10Update()
        {
            float delta = Vector3.Distance(m_LestPos, Position);

            for (int i = 0; i < m_Lest10Moves.Length - 1; i++)
            {
                m_Lest10Moves[i] = m_Lest10Moves[i + 1];
            }

            m_Lest10Moves[m_Lest10Moves.Length - 1] = delta;

            // Check if the sum of the array elements is below a certain threshold
            float sum = 0;
            foreach (float move in m_Lest10Moves)
            {
                sum += move;
            }

            float average = sum / m_Lest10Moves.Length;

            if (average < 0.01f) // Adjust the threshold value as needed
            {
                Debug.Log("the Ai is stuck");
                FindTarget(k_AiIsStuck);
            }

            // Update the last position for the next frame
            m_LestPos = Position;
        }

        private void FindTarget(bool i_AiIsStuck)
        {
            try
            {
                List<Vector3> all = ManagerGameFoodObj.Instance.GetAllUncollcted();

                if (all.Count == 0)
                {
                    m_Target = Vector3.zero;
                    TargetExist = false;
                }
                else
                {
                    if (!i_AiIsStuck)
                    {
                        m_Target = all.OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
                        TargetExist = m_Target != Position;
                    }
                    else
                    {
                        m_Target = all.Where(v => v != m_Target)
                            .OrderBy(v => Vector3.Distance(Position, v)).FirstOrDefault();
                        TargetExist = m_Target != Position;
                    }
                }

                if (TargetExist)
                {
                    AIMethods.Seek(r_Agent, m_Target);
                }
                else
                {
                    AIMethods.Wander(ref m_WanderTarget, r_Agent);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"FindTarget trow error {e.ToString()}");
            }
        }

        public void FindTarget()
        {
            FindTarget(true);
        }

        public void OnCollectedAnyFood()
        {
            TargetExist = false;
        }
    }
}