//using UnityEngine;

//namespace DiningCombat.Player.Manger
//{
//    internal class OfflineAIMovement :BridgeImplementor3DMovement  // IAgent //Agent
//    {
//        //private SimpleAiAlgo m_Ai;
//        [SerializeField]
//        [Range(0f, 1f)]
//        private float m_AutoRotationSpeed = 0.01f;
//        [SerializeField]
//        private bool isTest = false;

//        //public SimpleAiAlgo Ai 
//        //{ 
//        //    get { return m_Ai; } 
//        //    set { m_Ai = value; }
//        //}

//        //private new void Awake()
//        //{
//        //    base.Awake();
//        //    m_Ai = new SimpleAiAlgo(this.transform);
//        //    m_Ai.m_CoroutineStarter += CoroutineStarter;
//        //    CoroutineStarter(SimpleAiAlgo.eCoroutine.AlgoCoroutine);
//        //}
//        private void Start()
//        {
            
//        }
//        //StartCoroutine

//        //private void CoroutineStarter(SimpleAiAlgo.eCoroutine coroutine)
//        //{
//        //    switch (coroutine)
//        //    {
//        //        case SimpleAiAlgo.eCoroutine.AlgoCoroutine:
//        //            StartCoroutine(Ai.AlgoCoroutine());
//        //            break;
//        //        case SimpleAiAlgo.eCoroutine.CalculatAngles:
//        //            StartCoroutine(Ai.AutoAnglesCalculat());
//        //            break;
//        //    }
//        //}

//        void Update()
//        {
//            //Ai.RunAlog();
//            MoveVertonta();
//            MoveHorizontal();
//            MoveRotating();
//            Jump();
//            OnEndingUpdate();
//        }

//        public override void MoveVertonta()
//        {
//            //this.m_Vertical = Ai.GetVerticalAxis();
//            base.MoveVertonta();
//        }
//        public override void MoveHorizontal()
//        {
//            //this.m_Horizontal = Ai.GetHorizontalAxis();
//            base.MoveHorizontal();
//        }

//        public override bool Jump()
//        {
//            return false;
//            //if (Ai.Jump())
//            //{
//            //    m_Movement.Jump();
//            //}
//        }
//        private void MoveRotating()
//        {
//           //m_Movement.Rotate(Ai.GetRotateAxis()* m_AutoRotationSpeed);
//        }

//        private string GetDebuggerDisplay()
//        {
//            return "OfflinePlayerMovement";
//        }

//        private void StartCoroutineAutoAnglesCalculat()
//        {
//            //StartCoroutine(Ai.AutoAnglesCalculat());
//        }
//    }
//}