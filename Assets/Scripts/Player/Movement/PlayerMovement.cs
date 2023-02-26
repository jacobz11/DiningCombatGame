using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DiningCombat
{
    public class PlayerMovement : MonoBehaviour
    {
        // ================================================
        // constant Variable 
        private const string k_RunningSide = GameGlobal.AnimationName.k_RunningSide;
        private const string k_Running = GameGlobal.AnimationName.k_Running;

        // ----------------keyboard------------------------ 
        private const byte k_LeftKey = GameKeyboardControls.k_Left;
        private const byte k_RightKey = GameKeyboardControls.k_Right;
        private const byte k_ForwarKey = GameKeyboardControls.k_Forwar;
        private const byte k_BackKey = GameKeyboardControls.k_Back;
        private const byte k_PowerKey = GameKeyboardControls.k_Power;
        private const byte k_JumpKey = GameKeyboardControls.k_Jump;

        // ----------------Axis---------------------------- 
        private const string k_AxisHorizontal = "Horizontal";
        private const string k_AxisVertical = "Vertical";

        /// <summary> constant var for get-Update-Direction </summary>
        private const bool k_ThisIsAxisHorizontal = true;
        private const bool k_ThisIsAxisVertical = false;

        // ----------------Scale Vector-------------------- 

        private static readonly Vector3 sr_ScaleToRight = Vector3.one;
        private static readonly Vector3 sr_ScaleToLeft = new(-1, 1, 1);

        // ================================================
        // Delegate
        public event EventHandler Destruction;

        // ================================================
        // Fields 
        // ----------------Movement Vector-----------------
        private Vector3 m_HorizontalDirection;
        private Vector3 m_VerticalDirectionSide;

        /// <summary>Velocity for jumping</summary>
        private Vector3 m_Velocity;

        // ----------------Axis---------------------------- 
        private CharacterController m_Controller;
        private GameKeyboardControls m_Controls;

        private Animator m_Anim;

        // ================================================
        // ----------------Serialize Field-----------------
        // ----------------Movement Field------------------
        [SerializeField]
        private float m_MoveSpeed;
        [SerializeField]
        private float m_RunSpeed;
        [SerializeField]
        private float m_RunSideSpeed;
        [SerializeField]
        private float m_JumpHeight;

        // to debud 
        [SerializeField]
        private bool m_AnimeRunnig;
        [SerializeField]
        private bool m_AnimeSideRunnig;
        [SerializeField]
        private bool m_AnimeThrowingRunnig;
        [SerializeField]
        private string m_TheRealSituation;


        // ----------------Ground Field--------------------
        [SerializeField]
        private float m_GroundCheckDistance;
        [SerializeField]
        private LayerMask m_GroundMask;


        // ----------------Physics-------------------------
        [SerializeField]
        private float m_Gravity;

        // ================================================
        // properties
        // ----------------Movement Vector-----------------
        public bool IsVelocity
        {
            get => m_Velocity.y < 0;
        }

        public bool IsVerticalMove
        {
            get => m_HorizontalDirection.sqrMagnitude > 0.02f;
        }

        public bool IsHorizontalMove
        {
            get => m_VerticalDirectionSide.sqrMagnitude > 0.02f;// != Vector3.zero;
        }

        private bool isGrounded()
        {
            return Physics.CheckSphere(transform.position,
                m_GroundCheckDistance,
                m_GroundMask);
        }

        public bool AnimaRunning
        {
            get => m_Anim.GetBool(k_Running);
            set
            {
                m_Anim.SetBool(k_RunningSide, false);
                m_Anim.SetBool(k_Running, value);
            }
        }
        //AnimaRunningSide 
        // AnimaRunning
        public bool AnimaRunningSide
        {
            get => m_Anim.GetBool(k_RunningSide);
            set
            {
                m_Anim.SetBool(k_Running, false);
                m_Anim.SetBool(k_RunningSide, value);
            }
        }
        // ================================================
        // auxiliary methods programmings


        // ================================================
        // Unity Game Engine

        void Awake()
        {
            if (m_RunSpeed <= 0)
            {
                m_RunSpeed = GameGlobal.PlayerData.k_DefaultPlayerMovementRunSpeed;
            }
            if (m_RunSideSpeed <= 0) 
            {
                m_RunSideSpeed = GameGlobal.PlayerData.k_DefaultPlayerMovementRunSideSpeed;
            }

            if (m_GroundCheckDistance == 0)
            {
                m_GroundCheckDistance = GameGlobal.PlayerData
                    .k_DefaultPlayerMovementGroundCheckDistance;
            }
            if (m_Gravity == 0)
            {
                m_Gravity = GameGlobal.PlayerData.k_DefaultPlayerMovementGravity;
            }

            if (m_JumpHeight <= 0)
            {
                m_JumpHeight = GameGlobal.PlayerData.k_DefaultPlayerMovementJumpHeight;
            }
            m_Controls = new GameKeyboardControls();
        }

        void Start()
        {
            m_Controller = GetComponent<CharacterController>();
            m_Anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (isGrounded() && IsVelocity)
            {
                m_Velocity.y = -2f;
            }

            m_HorizontalDirection = getUpdateDirection(k_ThisIsAxisHorizontal);
            m_VerticalDirectionSide = getUpdateDirection(k_ThisIsAxisVertical);

            onGroundMovement();
        }

        private void LateUpdate()
        {
            // Actual Movements
            m_Controller.Move(m_HorizontalDirection * Time.deltaTime);
            m_Controller.Move(m_VerticalDirectionSide * Time.deltaTime);

            updateDebug();

            // Actual Jumping
            m_Velocity.y += m_Gravity * Time.deltaTime;
            m_Controller.Move(m_Velocity * Time.deltaTime);
        }
        private void updateDebug()
        {
            m_AnimeRunnig = m_Anim.GetBool(k_Running);
            m_AnimeSideRunnig = m_Anim.GetBool(k_RunningSide);
            m_AnimeThrowingRunnig = m_Anim.GetBool(GameGlobal.AnimationName.k_Throwing);
        }

        // ================================================
        //  methods
        private void onGroundMovement()
        {
            if (isGrounded())
            {
                if (IsHorizontalMove)
                {
                    running();
                }
                else if (IsVerticalMove)
                {
                    runningSide();
                }
                else
                {
                    idle();
                }

                m_HorizontalDirection *= m_MoveSpeed;
                m_VerticalDirectionSide *= m_MoveSpeed;

                jump();
            }
        }

        // ----------------Types Of Movements--------------
        private void idle()
        {
            AnimaRunning = false; 
            m_TheRealSituation = "idle";
        }

        private void running()
        {
            AnimaRunning = true;
            m_TheRealSituation = "running";
            m_MoveSpeed = m_RunSpeed;
        }

        private void runningSide()
        {
            localScaleePleyar();
            AnimaRunningSide = true;
            m_TheRealSituation = "runningSide";
            m_MoveSpeed = m_RunSideSpeed;
        }

        private void localScaleePleyar()
        {
            transform.localScale = m_Controls[k_LeftKey].Press ? sr_ScaleToLeft : sr_ScaleToRight;
        }

        private void jump()
        {
            if (m_Controls[k_JumpKey].Press)
            {
                m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2 * m_Gravity);
            }
        }

        // ================================================
        // auxiliary methods
        /// <summary>
        /// Calculate the distance from this transform to Input.GetAxis
        /// </summary>
        /// <param name="i_IsVertical">
        /// <true>true - k_ThisIsAxisHorizontal </true> 
        /// <false> false - k_ThisIsAxisVertical </false>
        /// </param>
        /// <returns>Vector3 </returns>
        private Vector3 getUpdateDirection(bool i_IsVertical)
        {
            float x = 0, y = 0, z = 0;

            if (i_IsVertical)
            {
                x = Input.GetAxis(k_AxisHorizontal);
            }
            else
            {
                z = Input.GetAxis(k_AxisVertical);
            }

            return transform.TransformDirection(new Vector3(x, y, z));
        }
    }
}
