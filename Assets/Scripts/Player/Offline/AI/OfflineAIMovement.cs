using Assets.Scrips_new.AI.Algo;
using Assets.Scripts.AI.Algo;
using UnityEngine;

namespace DiningCombat.Player.Manger
{
    internal class OfflineAIMovement :BridgeImplementor3DMovement  // IAgent //Agent
    {
        private SimpleAiAlgo m_Ai;
        [SerializeField]
        [Range(0f, 1f)]
        private float m_AutoRotationSpeed = 0.01f;
        [SerializeField]
        private bool isTest = false;

        public SimpleAiAlgo Ai 
        { 
            get { return m_Ai; } 
            set { m_Ai = value; }
        }

        private new void Awake()
        {
            base.Awake();
            m_Ai = new SimpleAiAlgo(this.transform);
        }

        void Update()
        {
            Ai.RunAlog();
            MoveVertonta();
            MoveHorizontal();
            MoveRotating();
            Jump();
        }

        public override void MoveVertonta()
        {
            this.m_Vertical = Ai.GetVerticalAxis();
            base.MoveVertonta();
        }
        public override void MoveHorizontal()
        {
            this.m_Horizontal = Ai.GetHorizontalAxis();
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
           m_Movement.Rotate(Ai.GetRotateAxis()* m_AutoRotationSpeed);
        }

        private string GetDebuggerDisplay()
        {
            return "OfflinePlayerMovement";
        }
    }
}