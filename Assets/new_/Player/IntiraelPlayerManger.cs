//using System;
//using UnityEngine;
//using DiningCombat;
//using DiningCombat.FoodObj;
//using DiningCombat.Player;
//using DiningCombat.Player.UI;
//using DiningCombat.Player.Manger;
//using DiningCombat.Player.Offline.Movement;
//using static DiningCombat.GameGlobal;
//using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
//using Assets.Scripts.Player.Offline.AI.ML;
//using Assets.Scripts.Player.Trinnig;

//public class IntiraelPlayerManger : MonoBehaviour
//{
//    #region GameManager data 
//    private int KillMullPonit => GameManager.Singlton.KillMullPonit;
//    private float MinAdditionForce => GameManager.Singlton.MinAdditionForce;
//    private float MaxAdditionForce => GameManager.Singlton.MaxAdditionForce;
//    private float MaxForce => GameManager.Singlton.MaxForce;
//    private float MinForce => GameManager.Singlton.MinForce;
//    #endregion
//    [SerializeField]
//    protected Transform m_PickUpPoint;
//    [SerializeField]
//    private Collider m_Collider;
//    #region Actions
//    public event Action<Collision> CollisionEnter;
//    public event Action<Collision> CollisionExit;
//    public event Action<Collider> PickUpZonEnter;
//    public event Action<Collider> PickUpZonExit;
//    public event Action<float> PlayerForceChange;
//    public event Action<int> PlayerScoreChange;
//    public event Action<int> LifePointChange;
//    public event Action PlayerDead;
//    public event Action AnimatorEvntThrow;
//    public event Action<float, float> FruitHitPlayer;
//    #endregion
//    private int m_LifePoint;
//    private float m_Force = 0;

//    public int Score { get; private set; }
//    public int Kills { get; private set; }
//    public bool IsHoldingFoodObj { get; private set; }

//    public int LifePoint
//    {
//        get => m_LifePoint;
//        private set
//        {
//            m_LifePoint = value;
//            LifePointChange?.Invoke(m_LifePoint);
//        }
//    }
//    public float ForceMull
//    {
//        get => m_Force;
//        private set
//        {
//            Debug.Log("ForceMull  " + value);
//            m_Force = value;
//            PlayerForceChange?.Invoke(m_Force);
//        }
//    }

//    private void Awake()
//    {
//        Kills = 0;
//        Score = 0;
//        //m_LifePoint = GameGlobal.PlayerDataG.k_InitLifePoint;
//        IsHoldingFoodObj = false;
//    }

//    public Transform PickUpPoint
//    {
//        get => m_PickUpPoint;
//        protected set => m_PickUpPoint = value;
//    }

//    #region Actions Invoke 
//    /// <summary>
//    /// When there is a good Thrown the <see cref="GameFoodObj"/> will call this method to 
//    /// to update the data 
//    /// </summary>
//    /// <param name="i_FoodThrown">the GameFoodObj is for validation</param>
//    internal void OnSuccessfulThrown(FoodObject i_FoodThrown, float i_ScoreHitPoint, int i_ThrownKills)
//    {
//        //if (i_FoodThrown is null)
//        //{
//        //    Debug.LogError("FoodThrown is null: Validation Fails");
//        //    return;
//        //}

//        Kills += i_ThrownKills;
//        Score += (int)i_ScoreHitPoint + i_ThrownKills * KillMullPonit;
//        PlayerScoreChange?.Invoke(Score);
//    }

//    /// <summary>
//    /// </summary>
//    /// <param name="i_AdditionForce">float.NegativeInfinity to make foce 0</param>
//    public void ChangeForce(float i_AdditionForce)
//    {
//        if (isResetingForce(i_AdditionForce))
//        {
//            ForceMull = 0;
//        }
//        else
//        {
//            IsInReng(i_AdditionForce, MaxAdditionForce, MinAdditionForce, out float o_ForceAdding);
//            IsInReng(o_ForceAdding+ ForceMull, MaxForce, MinForce, out float o_NewForce);
//            ForceMull = o_NewForce;
//        }

//        static bool isResetingForce(float i_AdditionForce)
//            => i_AdditionForce == float.NegativeInfinity;
//    }

//    /// <summary></summary>
//    /// <param name="i_HitForce">the hit Force this</param>
//    /// <param name="o_IsKill">if the hit kill the this Player</param>
//    /// <exception cref="Exception">i_HitForce mast be a non-negative number </exception>
//    public void OnHitPlayer(float i_HitForce, out bool o_IsKill)
//    {
//        if (i_HitForce < 0)
//        {
//            throw new Exception("Why negative mf");
//        }

//        LifePoint -= (int)i_HitForce;
//        o_IsKill = m_LifePoint <= 0;

//        if (o_IsKill)
//        {
//            PlayerDead?.Invoke();
//        }

//    }

//    public void OnPlayerSetFoodObj(GameObject i_GameObject)
//    {
//        IsHoldingFoodObj = i_GameObject != null;
//        m_Collider.enabled = !IsHoldingFoodObj;
//    }

//    public void OnPlayerFoodThrown()
//    {
//        IsHoldingFoodObj = false;
//    }

//    protected virtual void OnCollisionEnter(Collision collision)
//    {
//        CollisionEnter?.Invoke(collision);
//    }

//    protected virtual void OnCollisionExit(Collision collision)
//    {
//        CollisionExit?.Invoke(collision);
//    }

//    protected virtual void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag(GameGlobal.TagNames.k_FoodObj))
//        {
//            PickUpZonEnter?.Invoke(other);
//        }
//    }

//    protected virtual void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag(GameGlobal.TagNames.k_FoodObj))
//        {
//            PickUpZonExit?.Invoke(other);
//        }
//    }

//    internal void OnGameOver()
//    {

//    }
//    #endregion

//    public static bool IsInReng(float i_x, float i_Max, float i_Min, out float res)
//    {
//        bool isMinBigger = i_x < i_Min;
//        bool isMaxSmaller = i_x > i_Max;
//        res = isMinBigger ? i_Min : isMaxSmaller ? i_Max : i_x;

//        return !(isMinBigger || isMaxSmaller);
//    }

//    public void Builder(PlayerData i_PlayerData, GameObject spawnPlayer)
//    {
//        if (i_PlayerData.IsInitialize)
//        {
//            Debug.LogError("player cant be  Initialize twice");
//            return;
//        }

//        SetCamToDiffDisply(i_PlayerData.r_PlayerNum, spawnPlayer);
//        i_PlayerData = AddAbstrcts(i_PlayerData, spawnPlayer, out BridgeAbstraction3DMovement movement, out BridgeAbstractionAction playerHand);
//        BridgeImplementor3DMovement playerMovementImplementor = null;
//        BridgeImplementorAcitonStateMachine acitonHandStateMachine = null;

//        switch (i_PlayerData.r_ModeType)
//        {
//            case ePlayerModeType.OfflinePlayer:
//                AddPlayerUi(spawnPlayer);
//                playerMovementImplementor = spawnPlayer.AddComponent<OfflinePlayerMovement>();
//                playerMovementImplementor.SetPlayerMovement(movement);
//                acitonHandStateMachine = spawnPlayer.AddComponent<OfflinePlayerStateMachine>();
//                acitonHandStateMachine.SetPlayerHand(playerHand);
//                acitonHandStateMachine.BuildState();
//                break;
//            case ePlayerModeType.OnlinePlayer:
//                Debug.Log("Builder  PlayerMovement : OnlinePlayer");
//                return;
//            case ePlayerModeType.OfflineAiPlayer:
//                playerMovementImplementor = spawnPlayer.AddComponent<OfflineAIMovement>();
//                playerMovementImplementor.SetPlayerMovement(movement);
//                acitonHandStateMachine = spawnPlayer.AddComponent<OfflineAIStateMachine>();
//                acitonHandStateMachine.SetPlayerHand(playerHand);
//                acitonHandStateMachine.BuildState();
//                break;
//            case ePlayerModeType.OnlineAiPlayer:
//                Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
//                return;
//            case ePlayerModeType.OfflineTestPlayer:
//                //PlayerMovementStub PlayerMovmentSub = spawnPlayer.AddComponent<PlayerMovementStub>();
//                //PlayerMovmentSub.SetPlayerMovement(movement);
//                break;
//            case ePlayerModeType.OnlineTestPlayer:
//                Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
//                return;
//            case ePlayerModeType.MLTrining:
//                playerMovementImplementor = spawnPlayer.AddComponent<ML3DMovement>();
//                playerMovementImplementor.SetPlayerMovement(movement);
//                acitonHandStateMachine = spawnPlayer.AddComponent<MLAcitonStateMachine>();
//                acitonHandStateMachine.SetPlayerHand(playerHand);
//                acitonHandStateMachine.BuildState();
//                PlayerAgent player = spawnPlayer.GetComponent<PlayerAgent>();
//                player.SetBridges(playerMovementImplementor as ML3DMovement,
//                    acitonHandStateMachine as MLAcitonStateMachine, i_PlayerData.r_InitPos);
//                //AddingListenersToAwards();
//                break;
//            default:
//                return;
//        }

//        AddingListenersToAnimationEvent(playerHand, acitonHandStateMachine);
//        AddingListeners(playerHand, acitonHandStateMachine);
//    }

//    private PlayerData AddAbstrcts(PlayerData i_PlayerData, GameObject spawnPlayer,
//                out BridgeAbstraction3DMovement o_Movement, out BridgeAbstractionAction o_PlayerHand)
//    {
//        i_PlayerData.Init(spawnPlayer);
//        o_Movement = spawnPlayer.AddComponent<BridgeAbstraction3DMovement>();
//        o_PlayerHand = spawnPlayer.AddComponent<BridgeAbstractionAction>();
//        o_PlayerHand.SetPickUpPoint(PickUpPoint);

//        return i_PlayerData;
//    }

//    private void AddingListeners(BridgeAbstractionAction i_PlayerHand, BridgeImplementorAcitonStateMachine i_StateMachine)
//    {
//        i_StateMachine.Powering.OnPower += ChangeForce;
//        i_StateMachine.Free.PlayerCollectedFood += OnPlayerSetFoodObj;
//        i_StateMachine.Free.PlayerCollectedFood += i_StateMachine.OnPlayerSetFoodObj;
//        i_StateMachine.Free.PlayerCollectedFood += i_PlayerHand.SetGameFoodObj;
//        PlayerForceChange += i_PlayerHand.SetForceMulti;
//        PickUpZonEnter += i_StateMachine.Free.EnterCollisionFoodObj;
//        PickUpZonExit += i_StateMachine.Free.ExitCollisionFoodObj;
//    }

//    private void AddingListenersToAnimationEvent(BridgeAbstractionAction i_PlayerHand, BridgeImplementorAcitonStateMachine i_StateMachine)
//    {
//        PlayerAnimationChannel animationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
//        if (animationChannel == null)
//        {
//            Debug.Log("AnimationChannel is null");
//            return;
//        }

//        animationChannel.ThrowPoint += i_PlayerHand.ThrowObj;
//        animationChannel.ThrowPoint += i_StateMachine.Throwing.ThrowingPointObj;
//        animationChannel.ThrowPoint += CoroutinePoweringState(i_StateMachine.Powering);
//        PlayerDead += animationChannel.OnPlayerDead;
//    }

//    private void AddPlayerUi(GameObject spawnPlayer)
//    {
//        PlayerUi playerUi = spawnPlayer.AddComponent<PlayerUi>();
//        PlayerForceChange += playerUi.OnPlayerForceChange;
//    }

//    private static void SetCamToDiffDisply(int i_TargetDisplay, GameObject spawnPlayer)
//    {
//        Camera cam = spawnPlayer.GetComponentInChildren<Camera>();
//        cam.targetDisplay = i_TargetDisplay;
//    }

//    private Action CoroutinePoweringState(StatePowering poweringState)
//    {
//        return () => StartCoroutine(poweringState.OnTringPoint());
//    } 
//}
