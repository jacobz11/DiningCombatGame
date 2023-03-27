using Assets.Scrips_new.AI.Algo;
using DiningCombat.Player;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DiningCombat.AI.Offline.Movement
{
    internal class OfflineAIMovement<IAiAlgoAgent> : PlayerMovementImplementor
    {
        AIPlayer<IAiAlgoAgent> agents;

        private void Awake()
        {
            agents = gameObject.AddComponent<AIPlayer<IAiAlgoAgent>>();
        }
        void Update()
        {
            agents.RunAlog();
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
            //m_Movement.Rotate(Input.GetAxis(AxisMouseX));
        }

        private string GetDebuggerDisplay()
        {
            return "OfflinePlayerMovement";
        }
    }
}