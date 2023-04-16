//using Abstraction.Player;
//using DiningCombat.Player.Offline.Movement;
//using System;
//using UnityEngine;
//using static DiningCombat.GameGlobal;

//namespace DiningCombat.Player
//{
//    public class TwoValues
//    {
//        private Vector2 m_Vector;
//        public float High => m_Vector.y;
//        public float Low => m_Vector.x;

//        public TwoValues(float high, float low)
//        {
//            m_Vector = new Vector2(low, high);
//        }
//        public TwoValues() { }
//    }
//    public class BridgeAbstraction3DMovement : MonoBehaviour, IMovement, IRotation
//    {
//        private bool m_IsGrounded;
//        protected PlayerAnimationChannel m_AnimationChannel;
//        private Action<bool> RunAnimation;
//        private Rigidbody m_Rb;
//        private float m_PlayerSpeed = 3.0f;
//        [SerializeField]
//        [Range(5, 1000)]
//        private float m_MouseSensetivity = 20;
//        [SerializeField]
//        [Range(5, 100)]
//        private TwoValues m_RunnigSpeed = new TwoValues(3f, 5f);
//        [SerializeField]
//        [Range(2, 500)]
//        private float m_JumpHeight = 430.0f;

//        private void Awake()
//        {
//            m_AnimationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();

//            if (m_AnimationChannel is null)
//            {
//                Debug.LogError("the PlayerAnimationChannel Not found");
//            }
//            else
//            {
//                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRun;
//                m_AnimationChannel.JumpingEnd += OnJumpingUpEnd;
//            }
//            m_PlayerSpeed = m_RunnigSpeed.Low;
//            m_Rb = gameObject.AddComponent<Rigidbody>();
//            m_Rb.constraints = RigidbodyConstraints.FreezeRotation;
//        }

//        private void OnJumpingUpEnd()
//        {
//            m_Rb.velocity = Vector3.zero;
//        }

//        public virtual bool IsGrounded
//        {
//            get => this.m_IsGrounded;
//            set => this.m_IsGrounded = value;
//        }

//        public virtual bool Jump()
//        {
//            bool res = false;
//            if (IsGrounded)
//            {
//                m_Rb.AddForce(Vector3.up * m_JumpHeight);
//                m_AnimationChannel.SetPlayerAnimationToJump();
//                res = true;
//            }

//            return res;
//        }

//        public virtual void MoveBackward()
//        {
//            transform.Translate(Vector3.back * Time.deltaTime * m_PlayerSpeed);
//            m_AnimationChannel.SetPlayerAnimationToRunBack(true);
//            RunAnimation?.Invoke(false);
//        }

//        public virtual void MoveForward()
//        {
//            transform.Translate(Vector3.forward * Time.deltaTime * m_PlayerSpeed);
//            m_AnimationChannel.SetPlayerAnimationToRunBack(false);
//            RunAnimation?.Invoke(true);
//        }
//        public virtual void MoveLeft()
//        {
//            transform.Translate(Vector3.left * Time.deltaTime * m_PlayerSpeed);
//            RunAnimation?.Invoke(true);
//        }
//        public virtual void MoveRight()
//        {
//            transform.Translate(Vector3.right * Time.deltaTime * m_PlayerSpeed);
//            RunAnimation?.Invoke(true);
//        }
//        public virtual void Rotate(float amount)
//        {
//            transform.Rotate(Vector3.up, amount * m_MouseSensetivity);
//        }

//        private void OnCollisionExit(Collision collision)
//        {
//            if (collision.gameObject.CompareTag("Ground"))
//            {
//                IsGrounded = false;
//            }
//        }

//        private void OnCollisionEnter(Collision collision)
//        {
//            if (collision.gameObject.CompareTag("Ground"))
//            {
//                IsGrounded = true;
//            }
//        }


//        public virtual void ChangRuningAniamtion(bool i_IsToFastRunnin)
//        {
//            if (i_IsToFastRunnin)
//            {
//                m_PlayerSpeed = m_RunnigSpeed.High;
//                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRunFast;
//                RunAnimation -= m_AnimationChannel.SetPlayerAnimationToRun;
//            }
//            else
//            {
//                m_PlayerSpeed = m_RunnigSpeed.Low;
//                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRun;
//                RunAnimation -= m_AnimationChannel.SetPlayerAnimationToRunFast;
//            }
//        }

//        public static void Builder(GameObject i_PlayerCharacter, ePlayerModeType i_Type,
//            out BridgeAbstraction3DMovement o_Movement, out BridgeImplementor3DMovement o_Implementor)
//        {
//            o_Movement = i_PlayerCharacter.AddComponent<BridgeAbstraction3DMovement>();

//            switch (i_Type)
//            {
//                case ePlayerModeType.OfflinePlayer:
//                    Debug.Log("Builder  PlayerMovement : OfflinePlayer");
//                    o_Implementor = i_PlayerCharacter.AddComponent<OfflinePlayerMovement>();
//                    o_Implementor.SetPlayerMovement(o_Movement);
//                    break;
//                case ePlayerModeType.OnlinePlayer:
//                    Debug.Log("Builder  PlayerMovement : OnlinePlayer");
//                    o_Implementor = null;
//                    return;
//                case ePlayerModeType.OfflineAiPlayer:
//                    Debug.Log("Builder  PlayerMovement : OfflineAiPlayer");
//                    o_Implementor = null;
//                    return;
//                case ePlayerModeType.OnlineAiPlayer:
//                    Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
//                    o_Implementor = null;
//                    return;
//                case ePlayerModeType.OfflineTestPlayer:
//                    //o_Implementor = i_PlayerCharacter.AddComponent<PlayerMovementStub>();
//                    //o_Implementor.SetPlayerMovement(o_Movement);
//                    //break;
//                    Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
//                    o_Implementor = null;
//                    return;
//                case ePlayerModeType.OnlineTestPlayer:
//                    Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
//                    o_Implementor = null;
//                    return;
//                default:
//                    o_Implementor = null;
//                    return;
//            }
//        }

//        internal void Idel(float m_Horizontal, float m_Vertical)
//        {
//            if (m_Horizontal == 0 && m_Vertical ==0) 
//            {
//                RunAnimation?.Invoke(false);
//                m_AnimationChannel.SetPlayerAnimationToRunBack(false);
//            }
//        }
//    }
//}
