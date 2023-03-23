using Assets.Scrips_new.Bridges.Player.Movment.Abstraction;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scrips_new.Player
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    internal class OfflinePlayerMovement : PlayerMovementImplementor
    {
        private const string AxisMouseX = "Mouse X";

        // Update is called once per frame
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