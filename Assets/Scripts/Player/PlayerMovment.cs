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

        private bool m_IsRunnig;
        private bool m_IsRunnigBack;

        private GameInput m_GameInput;
        private Rigidbody m_Rb;

        [SerializeField]
        private PlayerMovmentDataSO m_MovmentData;
        [SerializeField]
        private Material m_Material;

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
            get => m_IsRunnig;
            private set
            {
                if (value ^ m_IsRunnig)
                {
                    m_IsRunnig = value;
                }
            }
        }
        public bool IsRunnigBack
        {
            get => m_IsRunnigBack;
            private set
            {
                if (value ^ m_IsRunnigBack)
                {
                    m_IsRunnigBack = value;
                }
            }
        }

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
            m_GameInput.OnJumpAction += GameInput_OnJumpAction;
            Camera camera = gameObject.GetComponentInChildren<Camera>();
            camera.targetDisplay = GameManger.Instance.GetTargetDisplay();
            SkinnedMeshRenderer m = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            NetworkObject networkObj = GetComponent<NetworkObject>();

            if (networkObj == null)
            {
                Debug.LogWarning("Object does not have a NetworkObject component");
                return;
            }

            // Request ownership of the object
            if (networkObj.IsSpawned && !networkObj.IsOwnedByServer)
            {
            }
            if (IsHost)
            {
                m.material = m_Material;
            }
        }

        public void Update()
        {
            if (IsOwner)
            {
                HandleMovementClientRpc();
                HandleRotationeClientRpc();

            }
        }

        [ClientRpc]
        private void HandleMovementClientRpc()
        {
            float yOffset = 0;
            UpdateIsGrounded();
            float speed = IsGrounded ? PlayerSpeedNormalized : m_MovmentData.m_JumpSlowDonwSpeep;
            Vector2 inputVector = m_GameInput.GetMovementVectorNormalized();
            Vector3 movment = HandleMovement(inputVector, yOffset, speed);
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

        #region jumping matods
        private void UpdateIsGrounded()
        {
            float distToGround = 0f;
            bool currentIsGrounded = !Physics.Raycast(Position, -Vector3.up, (float)(distToGround + 0.1), m_MovmentData.m_Ground, QueryTriggerInteraction.UseGlobal);
            if (currentIsGrounded ^ IsGrounded)
            {
                IsGrounded = currentIsGrounded;
                m_AnimationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Grounded, IsGrounded);
            }
        }
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

        public void GameInput_OnJumpAction(object sender, EventArgs e)
        {
            if (!IsOwner)
            {
                return;
            }
            if (IsGrounded)
            {
                m_Rb.AddForce(Vector3.up * m_MovmentData.m_JumpHeight);
            }
        }
        #endregion
    }
}