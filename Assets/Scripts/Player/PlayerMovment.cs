namespace Assets.scrips
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    internal class PlayerMovment : MonoBehaviour
    {
        private const float k_PlayerHeight = 2f;
        private const float k_PlayerRadius = 0.7f;


        private bool m_IsRunnig;
        private bool m_IsRunnigBack;


        private GameInput m_GameInput;
        private Rigidbody m_Rb;


        [SerializeField]
        private PlayerMovmentData m_MovmentData;


        public event Action<bool> OnIsRunnigChang;
        public event Action<bool> OnIsRunnigBackChang;


        public Vector3 Position => transform.position;
        public bool IsGrounded { get; private set; }

        public float PlayerSpeedNormalized
        {
            get => m_MovmentData.m_PlayerSpeed * Time.deltaTime;
            private set => m_MovmentData.m_PlayerSpeed = value;
        }
        public float RotationeNormalized
        {
            get => m_MovmentData.m_MouseSensetivity * m_GameInput.GetRotin() * Time.deltaTime;
            private set => m_MovmentData.m_MouseSensetivity = value;
        }
        public bool IsRunnig
        {
            get
            {
                return m_IsRunnig;
            }
            private set
            {
                if (value != m_IsRunnig)
                {
                    m_IsRunnig = value;
                    OnIsRunnigChang?.Invoke(m_IsRunnig);
                }
            }
        }
        public bool IsRunnigBack
        {
            get
            {
                return m_IsRunnigBack;
            }
            private set
            {
                if (value != m_IsRunnigBack)
                {
                    m_IsRunnigBack = value;
                    OnIsRunnigBackChang?.Invoke(m_IsRunnigBack);
                }
            }
        }

        #region Unity
        private void Awake()
        {
            m_Rb = GetComponent<Rigidbody>();
            m_GameInput = GetComponent<GameInput>();
            m_Rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void Start()
        {
            m_GameInput.OnJumpAction += GameInput_OnJumpAction;

        }

        public void Update()
        {
            UpdateIsGrounded();
            if (IsGrounded)
            {
                HandleGroundedMovement(m_GameInput.GetMovementVectorNormalized());
            }
            else
            {
                HandleMovementWhileJumping(m_GameInput.GetMovementVectorNormalized());
            }
            HandleRotatione();
        }
        #endregion
        //public PlayerMovment(Rigidbody i_Rb, PlayerMovmentDataSO i_MovmentDataSO, Transform transform, GameInput gameInput)
        //{
        //    m_Rb = i_Rb;
        //    m_GameInput = gameInput;
        //    m_Rb.constraints = RigidbodyConstraints.FreezeRotation;
        //    m_MovmentData = i_MovmentDataSO;
        //    m_Transform = transform;    
        //}

        #region Handle Movement
        private void HandleGroundedMovement(Vector2 i_InputVector)
        {
            Vector3 movment = HandleMovement(i_InputVector, 0, PlayerSpeedNormalized);
            if (movment == Vector3.zero)
            {
                IsRunnig = false;
                IsRunnigBack = false;
            }
            else
            {
                if (movment.z < float.Epsilon)
                {
                    IsRunnig = false;
                    IsRunnigBack = true;
                }
                else
                {
                    IsRunnig = true;
                    IsRunnigBack = false;
                }
            }
        }
        private void HandleMovementWhileJumping(Vector2 i_InputVector)
        {
            float speed = PlayerSpeedNormalized * m_MovmentData.m_JumpSlowDonwSpeep;
            HandleMovement(i_InputVector, Position.y, speed);
            IsRunnig = false;
            IsRunnigBack = false;
        }

        private Vector3 HandleMovement(Vector2 i_InputVector, float i_YPosition, float i_MoveDistance)
        {
            Vector3 moveDir = new Vector3(i_InputVector.x, i_YPosition, i_InputVector.y);
            bool canMove = CanMove(moveDir, i_MoveDistance);

            if (!canMove)
            {
                Vector3 moveDirX = new Vector3(moveDir.x, i_YPosition, 0).normalized;
                canMove = IsInReng(moveDir.x) && CanMove(moveDirX, i_MoveDistance);
                if (canMove)
                {
                    moveDir = moveDirX;
                }
                else
                {
                    Vector3 moveDirZ = new Vector3(0, i_YPosition, moveDir.z).normalized;
                    canMove = IsInReng(moveDir.z) && CanMove(moveDirZ, i_MoveDistance);
                    if (canMove)
                    {
                        moveDir = moveDirZ;
                    }
                }
            }

            if (canMove)
            {
                transform.Translate(moveDir * i_MoveDistance);
                return moveDir;
            }
            return Vector3.zero;
            //    if (i_InputVector.x < 0)
            //    {
            //        Debug.Log("sfndj");
            //        IsRunnig = true;
            //        IsRunnigBack = false;
            //    }
            //    else 
            //    {
            //        IsRunnig = false;
            //        IsRunnigBack = true;
            //    }
            //}
            //else
            //{
            //    IsRunnig = false;
            //    IsRunnigBack = true;
            //}

            bool IsInReng(float moveDir)
            {
                return moveDir is < (-0.5f) or > (+0.5f);
            }
        }

        private bool CanMove(Vector3 moveDir, float moveDistance)
        {
            return !Physics.CapsuleCast(Position, Position + Vector3.up * k_PlayerHeight, k_PlayerRadius, moveDir, moveDistance);
        }
        #endregion

        #region jumping matods
        private void UpdateIsGrounded()
        {
            float distToGround = 0f;
            IsGrounded = !Physics.Raycast(Position, -Vector3.up, (float)(distToGround + 0.1), m_MovmentData.m_Ground, QueryTriggerInteraction.UseGlobal);
        }

        private void HandleRotatione()
        {
            transform.Rotate(Vector3.up, RotationeNormalized);
        }

        internal void GameInput_OnJumpAction(object sender, EventArgs e)
        {
            if (IsGrounded)
            {
                m_Rb.AddForce(Vector3.up * m_MovmentData.m_JumpHeight);
            }
        }
        #endregion
    }
}
