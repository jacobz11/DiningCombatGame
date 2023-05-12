namespace Assets.scrips
{
    using Assets.Scripts.Manger;
    using System;
    using Unity.Netcode;
    using UnityEngine;

    internal class PlayerMovment : NetworkBehaviour
    {
        public static int m_Cunnter = 0;

        private const float k_PlayerHeight = 2f;
        private const float k_PlayerRadius = 0.7f;

        //public static Color[] m_Colors = new Color[] { Color.red, Color.green, Color.blue };

        //public event Action<bool> OnIsRunnigChang;
        //public event Action<bool> OnIsRunnigBackChang;
        private PlayerAnimationChannel m_AnimationChannel;

        private bool m_IsRunnig;
        private bool m_IsRunnigBack;

        private GameInput m_GameInput;
        private Rigidbody m_Rb;

        [SerializeField]
        private PlayerMovmentData m_MovmentData;
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
                    //OnIsRunnigChang?.Invoke(m_IsRunnig);
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
                    //OnIsRunnigBackChang?.Invoke(m_IsRunnigBack);
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
            float speed = PlayerSpeedNormalized;
            float yOffset = 0;
            UpdateIsGrounded();
            if (!IsGrounded)
            {
                speed *= m_MovmentData.m_JumpSlowDonwSpeep;
                //yOffset = Position.y;
            }

            Vector3 movment = HandleMovement(m_GameInput.GetMovementVectorNormalized(), yOffset, speed);
            Debug.Log("IsGrounded " + IsGrounded);

            m_AnimationChannel.AnimationFloat(PlayerAnimationChannel.AnimationsNames.k_Forward, movment.z);
            m_AnimationChannel.AnimationFloat(PlayerAnimationChannel.AnimationsNames.k_Sides, movment.x);

            //if (!IsGrounded && movment != Vector3.zero)
            //{
            //    bool isRunnigFord = movment.z < float.Epsilon;
            //    IsRunnig = isRunnigFord;
            //    IsRunnigBack = !isRunnigFord;
            //}
            //else
            //{
            //    IsRunnig = false;
            //    IsRunnigBack = false;
            //}

            HandleMovementServerRpc(movment);

            //else
            //{
            //    Vector3 movnet = 
            //    IsRunnig = false;
            //    IsRunnigBack = false;
            //    moment = HandleMovementWhileJumping(m_GameInput.GetMovementVectorNormalized());
            //}
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

        internal void GameInput_OnJumpAction(object sender, EventArgs e)
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

//public PlayerMovment(Rigidbody i_Rb, PlayerMovmentDataSO i_MovmentDataSO, Transform transform, GameInput gameInput)
//{
//    m_Rb = i_Rb;
//    m_GameInput = gameInput;
//    m_Rb.constraints = RigidbodyConstraints.FreezeRotation;
//    m_MovmentData = i_MovmentDataSO;
//    Transform = transform;    
//}

//private Vector3 HandleGroundedMovement(Vector2 i_InputVector)
//{
//    Vector3 movment = HandleMovement(i_InputVector, 0, PlayerSpeedNormalized);
//    if (movment == Vector3.zero)
//    {
//        IsRunnig = false;
//        IsRunnigBack = false;
//    }
//    else
//    {
//        if (movment.z < float.Epsilon)
//        {
//            IsRunnig = false;
//            IsRunnigBack = true;
//        }
//        else
//        {
//            IsRunnig = true;
//            IsRunnigBack = false;
//        }
//    }
//    return movment;
//}
//private Vector3 HandleMovementWhileJumping(Vector2 i_InputVector)
//{
//    float speed = PlayerSpeedNormalized * m_MovmentData.m_JumpSlowDonwSpeep;
//    Vector3 movnet = HandleMovement(i_InputVector, Position.y, speed);
//    IsRunnig = false;
//    IsRunnigBack = false;
//    return movnet;
//}