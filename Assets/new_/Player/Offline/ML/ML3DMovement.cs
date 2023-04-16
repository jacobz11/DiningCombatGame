//using Assets.Scripts.Player.Trinnig;
//using DiningCombat.Player;

//namespace Assets.Scripts.Player.Offline.AI.ML
//{
//    internal class ML3DMovement : BridgeImplementor3DMovement
//    {
//        private DCActionBuffers DCAction = DCActionBuffers.Empty;

//        void Update()
//        {
//            RunBoostUpdate();
//            MoveVertonta();
//            MoveHorizontal();
//            MoveRotating();
//            Jump();
//            OnEndingUpdate();
//            DCAction.Used();
//        }

//        private void RunBoostUpdate()
//        {
//            if (DCAction.IsBoost)
//            {
//                StartCoroutine(BoostRunning());
//            }
//        }

//        public void SetSensorData(DCActionBuffers actionBuffers)
//        {
            
//            //Debug.Log(actionBuffers.ToString());
//            DCAction =  actionBuffers;
//        }
//        public override void MoveVertonta()
//        {
//            m_Vertical = DCAction.Vertical;
//            base.MoveVertonta();
//        }
//        public override void MoveHorizontal()
//        {
//            m_Horizontal = DCAction.Horizontal;
//            base.MoveHorizontal();
//        }

//        public override bool Jump()
//        {
//            bool res = false;
//            if (DCAction.IsJumping)
//            {
//                res = m_Movement.Jump();
//            }
//            return res;
//        }
//        private void MoveRotating()
//        {
//            m_Movement.Rotate(DCAction.Rotate);
//        }

//        private string GetDebuggerDisplay()
//        {
//            return "OfflinePlayerMovement";
//        }
//    }
//}
