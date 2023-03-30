using Assets.Scrips_new.AI.Algo;
using UnityEngine;

namespace DiningCombat.Player.Manger
{
    internal class OfflineAIMovement :PlayerMovementImplementor  // IAgent //Agent
    {
        private IAiAlgoAgent<Vector3, Vector3> m_Ai;

        public IAiAlgoAgent<Vector3, Vector3> Ai 
        { 
            get { return m_Ai; } 
            set { m_Ai = value; }
        }
        void Update()
        {
            if (true)
            {
                return;
            }
            Ai.RunAlog();
            MoveVertonta();
            MoveHorizontal();
            MoveRotating();
            Jump();
        }

        public override void MoveVertonta()
        {
            this.m_Vertical = Ai.GetAxis("Vertical");
            base.MoveVertonta();
        }
        public override void MoveHorizontal()
        {
            this.m_Horizontal = Ai.GetAxis("Horizontal");
            base.MoveHorizontal();
        }

        public override bool Jump()
        {
            return false;
            if (Ai.Jump())
            {
                m_Movement.Jump();
            }
        }
        private void MoveRotating()
        {
           m_Movement.Rotate(Ai.GetAxis("Rotate"));
        }

        private string GetDebuggerDisplay()
        {
            return "OfflinePlayerMovement";
        }
    }
}