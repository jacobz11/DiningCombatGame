using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiningCombat
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string k_AxisHorizontal = "Horizontal";
        private const string k_AxisVertical = "Vertical";
        private const bool k_NotOnTheGround = false;

        private static Vector3 s_ScaleToRight = Vector3.one;
        private static Vector3 s_ScaleToLeft = new(-1, 1, 1);

        [SerializeField]
        private bool m_IsOnGround = true;

        // variables
        [SerializeField]
        private float m_MoveSpeed;

        [SerializeField]
        private float m_RunSpeed;

        [SerializeField]
        private float m_RunSideSpeed;
        private Vector3 m_MoveDirection;
        private Vector3 m_MoveDirectionSide;
        private Vector3 m_Velocity;

        [SerializeField]
        private float m_GroundCheckDistance;
        [SerializeField]
        private LayerMask m_GroundMask;
        [SerializeField]
        private float m_Gravity;
        [SerializeField]
        private float m_JumpHeight;

        // references
        private CharacterController m_Controller;
        private Animator m_Anim;

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

        public bool IsForward
        {
            get
            {
                return Input.GetKey(GameGlobal.k_ForwardKey) || Input.GetKey(GameGlobal.k_ForwardKeyArrow);
            }
        }

        public bool IsBack
        {
            get
            {
                return Input.GetKey(GameGlobal.k_BackKey) || Input.GetKey(GameGlobal.k_BackKeyArrow);
            }
        }

        public bool IsRight
        {
            get
            {
                return Input.GetKey(GameGlobal.k_RightKey) || Input.GetKey(GameGlobal.k_RightKeyArrow);
            }
        }

        public bool IsLeft
        {
            get
            {
                return Input.GetKey(GameGlobal.k_LeftKey) || Input.GetKey(GameGlobal.k_LeftKeyArrow);
            }
        }

        protected void Start()
        {
            m_Controller = GetComponent<CharacterController>();
            m_Anim = GetComponentInChildren<Animator>();
        }

        protected void Update()
        {
            move();
        }

        private void move()
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

                if (IsHorizontalMove)
                {
                    runningSide();
                }
                else if (!IsVerticalMove)
                {
                    idle();
                }

                m_MoveDirection *= m_MoveSpeed;
                m_MoveDirectionSide *= m_MoveSpeed;

                throwing();
                jump();
            }
        }

        private bool isGrounded()
        {
            return Physics.CheckSphere(transform.position, m_GroundCheckDistance, m_GroundMask);
        }

        private void idle()
        {
        }

        private void running()
        {
            bool animationVertical = IsForward || IsBack;

            m_Anim.SetBool(GameGlobal.k_AnimationRunning, animationVertical);
            m_MoveSpeed = m_RunSpeed;
        }

        private void runningSide()
        {
            bool animationHorizontal = IsLeft || IsRight;

            if (animationHorizontal)
            {
                transform.localScale = IsLeft ? s_ScaleToLeft : s_ScaleToRight;
            }

            m_Anim.SetBool(GameGlobal.k_AnimationRunning, animationHorizontal);
            m_MoveSpeed = m_RunSpeed;
        }

        private void throwing()
        {
            if (Input.GetKeyDown(GameGlobal.k_PowerKey))
            {
                m_Anim.SetBool(GameGlobal.k_AnimationRunning, true);
                transform.localScale = s_ScaleToRight;
            }
            else if (Input.GetKeyUp(GameGlobal.k_PowerKey))
            {
                m_Anim.SetBool(GameGlobal.k_AnimationRunning, false);
                transform.localScale = s_ScaleToRight;
            }
        }

        private void jump()
        {
            if (Input.GetKeyDown(GameGlobal.k_JumpKey))
            {
                m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2 * m_Gravity);
            }
        }
    }
}