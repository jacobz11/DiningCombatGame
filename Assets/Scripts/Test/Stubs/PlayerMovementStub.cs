using DiningCombat.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Test.Stubs
{
    internal class PlayerMovementStub : PlayerMovementImplementor
    {
        [SerializeField]
        [Range(-10, 10)]
        private int m_VerticalStep;
        [SerializeField]
        [Range(-10, 10)]
        private int m_HorizontalStep;
        [SerializeField]
        private bool m_IsJumped;
        [SerializeField]
        [Range(-180f, 180f)]
        private float m_Rotating;

        void Update()
        {
            MoveVertonta();
            MoveHorizontal();
            MoveRotating();
            Jump();
        }

        public override void MoveVertonta()
        {
            if (this.m_VerticalStep != 0)
            {
                this.m_Vertical = m_VerticalStep;
                base.MoveVertonta();
                Debug.Log("MoveVertonta");

                if (m_VerticalStep > 0)
                {
                    m_VerticalStep--;
                }
                else
                {
                    this.m_VerticalStep++;
                }
            }
        }
        public override void MoveHorizontal()
        {
            if (this.m_HorizontalStep != 0)
            {
                this.m_Horizontal = m_HorizontalStep;
                base.MoveHorizontal();
                Debug.Log("MoveHorizontal");

                if (m_HorizontalStep > 0)
                {
                    m_HorizontalStep--;
                }
                else
                {
                    this.m_HorizontalStep++;
                }
            }
        }

        public override bool Jump()
        {
            if (m_IsJumped)
            {
                Debug.Log("Jump");
                m_Movement.Jump();
                m_IsJumped = false;
            }
            return m_IsJumped;
        }
        private void MoveRotating()
        {
            m_Movement.Rotate(m_Rotating);
            m_Rotating= 0;
        }

        private string GetDebuggerDisplay()
        {
            return "PlayerMovementStub";
        }
    }
}
