using Palmmedia.ReportGenerator.Core;
using System;
using UnityEngine;

namespace DiningCombat
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string k_ClassName = nameof(PlayerMovement);
        private const byte k_LeftKey = GameKeyboardControls.k_Left;
        private const byte k_RightKey = GameKeyboardControls.k_Right;
        private const byte k_ForwarKey = GameKeyboardControls.k_Forwar;
        private const byte k_BackKey = GameKeyboardControls.k_Back;
        private const byte k_PowerKey = GameKeyboardControls.k_Power;
        private const byte k_JumpKey = GameKeyboardControls.k_Power;

        // Axis
        private const string k_AxisHorizontal = "Horizontal";
        private const string k_AxisVertical = "Vertical";
        
        // Scale Vector
        private static readonly Vector3 sr_ScaleToRight = Vector3.one;
        private static readonly Vector3 sr_ScaleToLeft = new(-1, 1, 1);
        public event EventHandler Destruction;

        private Vector3 m_MoveDirection;
        private Vector3 m_MoveDirectionSide;
        private Vector3 m_Velocity;
        private Animator m_Anim;
        private CharacterController m_Controller;
        private GameKeyboardControls m_Controls;
        [SerializeField]
        private float m_MoveSpeed;
        [SerializeField]
        private float m_RunSpeed;
        [SerializeField]
        private float m_RunSideSpeed;
        [SerializeField]
        private float m_GroundCheckDistance;
        [SerializeField]
        private LayerMask m_GroundMask;
        [SerializeField]
        private float m_Gravity;
        [SerializeField]
        private float m_JumpHeight;
        public Vector3 MoveDirection
        {
            get
            {
                return m_MoveDirection;
            }
        }

        // ==================================================
        // property
        // ==================================================
        public bool IsVelocity
        {
            get
            {
                return m_Velocity.y < 0;
            }
        }

        public bool IsVerticalMove
        {
            get
            {
                return m_MoveDirection != Vector3.zero;
            }
        }

        public bool IsHorizontalMove
        {
            get
            {
                return m_MoveDirectionSide != Vector3.zero;
            }
        }

        private bool isGrounded()
        {
            return Physics.CheckSphere(transform.position, m_GroundCheckDistance, m_GroundMask);
        }

        // ==================================================
        // Unity Game Engine
        // ==================================================
        protected void Start()
        {
            m_Controls = new GameKeyboardControls();
            initDefualtSerializeField();
            m_Controller = GetComponent<CharacterController>();
            m_Anim = GetComponentInChildren<Animator>();
        }

        private void initDefualtSerializeField()
        {
            if (m_RunSpeed <= 0)
            {
                m_RunSpeed = GameGlobal.k_DefaultPlayerMovementRunSpeed;
            }

            if (m_RunSideSpeed <= 0)
            {
                m_RunSideSpeed = GameGlobal.k_DefaultPlayerMovementRunSideSpeed;
            }

            if (m_GroundCheckDistance == 0)
            {
                m_GroundCheckDistance = GameGlobal.k_DefaultPlayerMovementGroundCheckDistance;
            }

            if (m_Gravity == 0)
            {
                m_Gravity = GameGlobal.k_DefaultPlayerMovementGravity;
            }

            if (m_JumpHeight <= 0)
            {
                m_JumpHeight = GameGlobal.k_DefaultPlayerMovementJumpHeight;
            }
        }

        protected void Update()
        {
            if (isGrounded() && IsVelocity)
            {
                m_Velocity.y = -2f;
            }

            m_MoveDirection = transform.TransformDirection(new Vector3(0, 0, Input.GetAxis(k_AxisVertical)));
            m_MoveDirectionSide = transform.TransformDirection(new Vector3(Input.GetAxis(k_AxisHorizontal), 0, 0));

            onGroundMovement();

            // Actual Movements
            m_Controller.Move(m_MoveDirection * Time.deltaTime);
            m_Controller.Move(m_MoveDirectionSide * Time.deltaTime);

            // Actual Jumping
            m_Velocity.y += m_Gravity * Time.deltaTime;
            m_Controller.Move(m_Velocity * Time.deltaTime);
        }

        private void onGroundMovement()
        {
            if (isGrounded())
            {
                if (IsVerticalMove)
                {
                    running();
                }
                else if (IsHorizontalMove)
                {
                    runningSide();
                }
                else
                {
                    idle();
                }

                m_MoveDirection *= m_MoveSpeed;
                m_MoveDirectionSide *= m_MoveSpeed;

                jump();
            }
        }

        // ==================================================
        // types of movements
        // ==================================================

        //private void animationRun(bool i_Running, bool i_SideRunning)
        //{
        //    m_Anim.SetBool(GameGlobal.AnimationName.k_Running, i_Running);
        //    m_Anim.SetBool(GameGlobal.AnimationName.k_RunningSide, i_SideRunning);
        //}
        private void idle()
        {
            //m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, false);
            //animationRun(false, false);
        }

        private void running()
        {
            m_Anim.SetBool(GameGlobal.AnimationName.k_Running, true);
            m_MoveSpeed = m_RunSpeed;
        }

        private void runningSide()
        {
            if (m_Controls.IsHorizontal)
            {
                transform.localScale = m_Controls[k_LeftKey].Press ? sr_ScaleToLeft : sr_ScaleToRight;
            }
            m_Anim.SetBool(GameGlobal.AnimationName.k_RunningSide, m_Controls.IsHorizontal);
            m_MoveSpeed = m_RunSideSpeed;
        }

        private void throwing()
        {
            if (m_Controls[k_PowerKey].Down)
            {
                m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, true);
                transform.localScale = sr_ScaleToRight;
            }
            else if(m_Controls[k_PowerKey].Up)
            {
                m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, false);
                transform.localScale = sr_ScaleToRight;
            }
        }

        private void jump()
        {
            if (m_Controls[k_JumpKey].Press)
            {
                m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2 * m_Gravity);
            }
        }
    }
}