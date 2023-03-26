using System.Diagnostics;
using UnityEngine;

namespace DiningCombat.Player.Offline.Movement
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    internal class OfflinePlayerMovement : PlayerMovementImplementor
    {
        private const string AxisMouseX = "Mouse X";

        void Update()
        {
            MoveVertonta();
            MoveHorizontal();
            MoveRotating();
            Jump();
        }

        public override void MoveVertonta()
        {
            this.m_Vertical = Input.GetAxis("Vertical");
            base.MoveVertonta();
        }
        public override void MoveHorizontal()
        {
            this.m_Horizontal = Input.GetAxis("Horizontal");
            base.MoveHorizontal();
        }

        public override void Jump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_Movement.Jump();
            }
        }
        private void MoveRotating()
        {
            m_Movement.Rotate(Input.GetAxis(AxisMouseX));
        }

        private string GetDebuggerDisplay()
        {
            return "OfflinePlayerMovement";
        }
    }
}