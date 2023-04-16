//using Abstraction.Player;
//using Assets.Scripts.Util.Channels;
//using System;
//using System.Collections;
//using UnityEngine;

//namespace DiningCombat.Player
//{
//    public abstract class BridgeImplementor3DMovement : MonoBehaviour
//    {
//        private bool ToFastRunnig => true;
//        [SerializeField] [Range(0.0001f, 2f)]
//        private static float s_MinMovmentAbs;
//        [SerializeField] [Range(0f, 50f)]
//        private float m_BoostRunnigSpeed = 5f;
//        [SerializeField] [Range(1f, 20f)]
//        private float m_BoostTime = 5;
//        [SerializeField] [Range(1f, 200f)]
//        private float m_StandbyTime = 50;
//        protected BridgeAbstraction3DMovement m_Movement;
//        protected float m_Horizontal;
//        protected float m_Vertical;
//        protected bool m_IsAnyMovement = false;
//        internal TimeBuffer m_WaitingBoostTime ;

//        protected void Awake()
//        {
//           (m_WaitingBoostTime = new TimeBuffer(m_StandbyTime)).AddToData(-m_StandbyTime * 2);
//        }

//        public static bool IsMovment(float i_Vale, out bool o_IsPositive)
//        {
//            o_IsPositive = i_Vale > float.Epsilon;
//            return Math.Abs(i_Vale) > s_MinMovmentAbs;
//        }

//        public virtual void OnEndingUpdate()
//        {
//            m_Movement.Idel(m_Horizontal, m_Vertical);
//            m_Horizontal = 0f;
//            m_Vertical = 0f;
//        }

//        public virtual void MoveHorizontal()
//        {
//            if (IsMovment(m_Horizontal, out bool o_IsRight))
//            {
//                if (o_IsRight)
//                {
//                    m_Movement.MoveRight();
//                }
//                else
//                {
//                    m_Movement.MoveLeft();
//                }
//            }
//        }

//        protected IEnumerator BoostRunning()
//        {
//            if (m_WaitingBoostTime.IsBufferOver())
//            {
//                m_WaitingBoostTime.PreventDoubleEntry(m_BoostTime);
//                m_Movement.ChangRuningAniamtion(ToFastRunnig);

//                yield return new WaitForSeconds(m_BoostTime);
//                m_Movement.ChangRuningAniamtion(!ToFastRunnig);
//                m_WaitingBoostTime.Clear();
//            }
//        }

//        public virtual void MoveVertonta()
//        {
//            if (IsMovment(m_Vertical, out bool o_IsForward))
//            {
//                if (o_IsForward)
//                {
//                    m_Movement.MoveForward();
//                }
//                else
//                {
//                    m_Movement.MoveBackward();
//                }
//            }
//        }

//        public virtual bool Jump()
//        {
//            bool res = false;
//            if (m_Movement.Jump())
//            {
//                res = true;
//            }

//            return res;
//        }

//        public virtual void SetPlayerMovement(BridgeAbstraction3DMovement playerMovement)
//        {
//            m_Movement = playerMovement;
//        }
//    }
//}
