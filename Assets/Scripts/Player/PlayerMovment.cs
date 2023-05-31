using DiningCombat.Manger;
using System;
using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{
    public class PlayerMovment : NetworkBehaviour
    {
        public static int m_Cunnter = 0;

        private const float k_PlayerHeight = 2f;
        private const float k_PlayerRadius = 0.7f;

        private PlayerAnimationChannel m_AnimationChannel;

        private bool m_IsFallingAnimation;
        private bool m_IsJumpingAnimation;

        private GameInput m_GameInput;
        private Rigidbody m_Rb;

        [SerializeField]
        private PlayerMovmentDataSO m_MovmentData;
        [SerializeField]
        private Material m_Material;

        //Jumping parameters
        private float m_YGravitySpeed;
        //[SerializeField] bool m_IsGrounded = true;
        private Rigidbody m_PlayerRb;
        //Ground Layer parameters for check ground with sphere trigger
        public Transform m_GroundCheck;
        private float m_GroundDistance = 0.3f;
        public bool m_IsOnGround;

        private void Start()
        {
            m_PlayerRb = GetComponent<Rigidbody>();
        }

        public Vector3 Position => transform.position;
        //public bool IsGrounded { get; private set; }

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
        public bool IsFallingAnimation
        {
            get => m_IsFallingAnimation;
            private set
            {
                if (value ^ m_IsFallingAnimation)
                {
                    m_IsFallingAnimation = value;
                    m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Falling, m_IsFallingAnimation);
                }
            }
        }
        public bool IsJumpingAnimation
        {
            get => m_IsJumpingAnimation;
            private set
            {
                if (value ^ m_IsJumpingAnimation)
                {
                    m_IsJumpingAnimation = value;
                    m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Jumping, m_IsJumpingAnimation);
                }
            }
        }

        public bool PlayerCanMove { get; private set; }

        #region Unity
        private void Awake()
        {
            m_Rb = GetComponent<Rigidbody>();

            m_Rb.constraints = RigidbodyConstraints.FreezeRotation;
            m_AnimationChannel = GetComponentInChildren<PlayerAnimationChannel>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            m_GameInput = GetComponent<GameInput>();
            Camera camera = gameObject.GetComponentInChildren<Camera>();
            camera.targetDisplay = GameManger.Instance.GetTargetDisplay();
            SkinnedMeshRenderer m = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            NetworkObject networkObj = GetComponent<NetworkObject>();
            if (networkObj is null)
            {
                Debug.LogWarning("Object does not have a NetworkObject component");
                return;
            }

            if (gameObject.TryGetComponent<Player>(out Player player))
            {
                player.OnPlayerSweepFall += Player_OnPlayerSweepFall;
            }

            PlayerCanMove = true;
            // Request ownership of the object
            if (networkObj.IsSpawned && !networkObj.IsOwnedByServer)
            {
            }
        }

        private void Player_OnPlayerSweepFall(bool i_IsPlayerSweepFall)
        {
            PlayerCanMove = i_IsPlayerSweepFall;
        }

        public void Update()
        {
            if (IsOwner)
            {
                if (PlayerCanMove)
                {
                    //UpdateIsGrounded();
                    HandleMovementClientRpc();
                    HandleRotationeClientRpc();
                    HandleJumping();
                }
            }
        }

        [ClientRpc]
        private void HandleMovementClientRpc()
        {
            float yOffset = 0;
            Vector2 inputVector = m_GameInput.GetMovementVectorNormalized();
            Vector3 movment = HandleMovement(inputVector, yOffset, PlayerSpeedNormalized * m_MovmentData.ConfigJumpSlowDownSpeed(m_IsOnGround));
            m_AnimationChannel.AnimationFloat(PlayerAnimationChannel.AnimationsNames.k_Forward, inputVector.y);
            m_AnimationChannel.AnimationFloat(PlayerAnimationChannel.AnimationsNames.k_Sides, inputVector.x);
            HandleMovementServerRpc(movment);
        }

        [ServerRpc(RequireOwnership = false)]
        private void HandleMovementServerRpc(Vector3 i_Movment)
        {
            transform.Translate(i_Movment);
        }
        #endregion
        #region Handle Movement
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
                return moveDir * i_MoveDistance;
            }
            return Vector3.zero;

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

        #region Jumping Methods
        private void HandleJumping()
        {
            m_IsOnGround = Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_MovmentData.m_Ground);
            m_YGravitySpeed += Physics.gravity.y * Time.deltaTime;

            if (m_IsOnGround)
            {
                m_YGravitySpeed = 0f;
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Grounded, true);
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Jumping, false);
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Falling, false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_YGravitySpeed = m_MovmentData.m_JumpHeight;
                    m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Jumping, true);
                    m_IsOnGround = false;
                }
                m_PlayerRb.AddForce(Vector3.up * m_YGravitySpeed, ForceMode.Impulse);
            }
            else
            {
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Grounded, false);
                if (m_YGravitySpeed < 2f)
                {
                    m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Falling, true);
                }
            }
        }
        /*
         *  private void Jumping()
        {
            m_IsOnGround = Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask);
            m_YGravitySpeed += Physics.gravity.y * Time.deltaTime;

            if (m_IsOnGround)
            {
                m_YGravitySpeed = 0f;
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Grounded, true);
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Jumping, false);
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Falling, false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_YGravitySpeed = m_MovmentData.m_JumpHeight;
                    m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Jumping, true);
                    m_IsOnGround = false;
                }
                m_PlayerRb.AddForce(Vector3.up * m_YGravitySpeed, ForceMode.Impulse);
            }
            else
            {
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Grounded, false);
                if (m_YGravitySpeed < 2f)
                {
                    m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Falling, true);
                }
            }
         */

/*        private void UpdateIsGrounded()
        {
            bool currentIsGrounded = Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_MovmentData.m_Ground);
            if (currentIsGrounded ^ IsGrounded)
            {
                IsGrounded = currentIsGrounded;
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Grounded, IsGrounded);
            }
        }*/
        #endregion
        [ClientRpc]
        private void HandleRotationeClientRpc()
        {
            HandleRotationeServerRpc(RotationeNormalized);
        }
        [ServerRpc(RequireOwnership = false)]
        private void HandleRotationeServerRpc(float i_RotationeNormalized)
        {
            transform.Rotate(Vector3.up, i_RotationeNormalized);
        }
    }
}