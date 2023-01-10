
using UnityEngine;

namespace DiningCombat
{
    public class PlayerMovement : MonoBehaviour
    {
        // Axis
        private const string k_AxisHorizontal = "Horizontal";
        private const string k_AxisVertical = "Vertical";

        // Scale Vector
        private static readonly Vector3 sr_ScaleToRight = Vector3.one;
        private static readonly Vector3 sr_ScaleToLeft = new(-1, 1, 1);

        private Vector3 m_MoveDirection;
        private Vector3 m_MoveDirectionSide;
        private Vector3 m_Velocity;
        private CharacterController m_Controller;
        private Animator m_Anim;
        private KeysHamdler m_Forward;
        private KeysHamdler m_Backward;
        private KeysHamdler m_Left;
        private KeysHamdler m_Right;
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
            m_Forward = new KeysHamdler(GameGlobal.k_ForwardKey, GameGlobal.k_ForwardKeyArrow);
            m_Backward = new KeysHamdler(GameGlobal.k_BackKey, GameGlobal.k_BackKeyArrow);
            m_Left = new KeysHamdler(GameGlobal.k_LeftKey, GameGlobal.k_LeftKeyArrow);
            m_Right = new KeysHamdler(GameGlobal.k_RightKey, GameGlobal.k_RightKeyArrow);
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

        // ==================================================
        // types of movements
        // ==================================================
        private void idle()
        {
        }

        private void running()
        {
            bool animationVertical = m_Forward.Press|| m_Backward.Press;

            m_Anim.SetBool(GameGlobal.AnimationName.k_Running, animationVertical);
            m_MoveSpeed = m_RunSpeed;
        }

        private void runningSide()
        {
            bool animationHorizontal = m_Left.Press || m_Right.Press;

            if (animationHorizontal)
            {
                transform.localScale = m_Left.Press ? sr_ScaleToLeft : sr_ScaleToRight;
            }

            m_Anim.SetBool(GameGlobal.AnimationName.k_RunningSide, animationHorizontal);
            m_MoveSpeed = m_RunSideSpeed;
        }

        private void throwing()
        {
            if (Input.GetKeyDown(GameGlobal.k_PowerKey))
            {
                m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, true);
                transform.localScale = sr_ScaleToRight;
            }
            else if (Input.GetKeyUp(GameGlobal.k_PowerKey))
            {
                m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, false);
                transform.localScale = sr_ScaleToRight;
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