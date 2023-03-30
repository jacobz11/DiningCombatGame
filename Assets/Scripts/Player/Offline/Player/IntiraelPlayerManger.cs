using System;
using UnityEngine;
using DiningCombat;
using Util.Abstraction;
using DiningCombat.FoodObj;
using DiningCombat.Player;
using DiningCombat.Player.UI;
using DiningCombat.Player.Manger;
using DiningCombat.Player.Offline.Movement;
using Assets.Scripts.Test.Stubs;
using static DiningCombat.GameGlobal;
using Assets.Scripts.Player.PlayrAbstraction.ActionHand;

public class IntiraelPlayerManger : MonoBehaviour, IInternalChannel
{
    private int KillMullPonit => GameManager.Singlton.KillMullPonit;
    private float MinAdditionForce => GameManager.Singlton.MinAdditionForce;
    private float MaxAdditionForce => GameManager.Singlton.MaxAdditionForce;
    private float MaxForce => GameManager.Singlton.MaxForce;
    private float MinForce => GameManager.Singlton.MinForce;

    public event Action<Collision> CollisionEnter;
    public event Action<Collision> CollisionExit;
    public event Action<Collider> PickUpZonEnter;
    public event Action<Collider> PickUpZonExit;
    public event Action<float> PlayerForceChange;
    public event Action<int> PlayerScoreChange;
    public event Action<int> LifePointChange;
    public event Action PlayerDead;
    public event Action AnimatorEvntThrow;
    public event Action<float, float> FruitHitPlayer;

    [SerializeField]
    protected GameObject m_PickUpPoint;

    private int m_Score = 0;
    private int m_Kills = 0;
    private int m_LifePoint;
    private float m_Force = 0;
    private bool m_IsHoldingFoodObj = false;

    public GameObject PickUpPoint
    {
        get
        {
            return m_PickUpPoint;
        }
        protected set
        {
            m_PickUpPoint = value;
        }
    }

    public float ForceMull
    {
        get => m_Force;
    }
    public int Kills
    {
        get => m_Kills;
    }

    public int LifePoint
    {
        get => m_LifePoint;
    }

    public bool IsHoldingFoodObj
    {
        get => m_IsHoldingFoodObj;
    }

    internal void FoodThrownByPlayerHit(GameFoodObj i_FoodThrown,
        float i_ScoreHitPoint,
        int i_ThrownKills)
    {
        if (i_FoodThrown == null)
        {
            Debug.LogError("FoodThrown is null: Validation Fails");
            return;
        }

        m_Kills += i_ThrownKills;
        m_Score += (int)i_ScoreHitPoint + i_ThrownKills * KillMullPonit;
        PlayerScoreChange?.Invoke(m_Score);
    }

    public void ChangeForce(float i_AdditionForce)
    {
        if (i_AdditionForce == float.NegativeInfinity)
        {
            m_Force = 0;
        }
        else
        {
            if (!isInReng(i_AdditionForce,
           MaxAdditionForce,
           MinAdditionForce,
           out float o_ForceAdding))
            {
                Debug.LogError("How the AdditionForce " + i_AdditionForce + " is that out of range?");
            }
            float NewForce = ForceMull + o_ForceAdding;
            //Debug.Log("i_AdditionForce : " + i_AdditionForce + ", MaxAdditionForce :" + MaxAdditionForce
            //   + ", MinAdditionForce" + MinAdditionForce + ", o_ForceAdding" + o_ForceAdding +
            //   ", NewForce " + NewForce);

            if (!isInReng(NewForce,
                MaxForce,
                MinForce,
                out float o_NewForce))
            {
                Debug.LogError("How the NewForce " + o_ForceAdding + "is that out of range?");
            }

            //Debug.Log(", ForceMull + o_ForceAdding " + ForceMull + o_ForceAdding + ", o_NewForce : "+o_NewForce);
            m_Force = o_NewForce;
        }
       
        PlayerForceChange?.Invoke(m_Force);
    }

    public void HitPlayer(float i_HitForce, out bool o_IsKill)
    {
        if (i_HitForce < 0)
        {
            throw new Exception("Why negative mf");
        }

        m_LifePoint -= (int)i_HitForce;
        o_IsKill = m_LifePoint <= 0;

        if (o_IsKill)
        {
            PlayerDead?.Invoke();
        }
        else
        {
            LifePointChange?.Invoke(m_LifePoint);
        }
    }

    public void OnPlayerSetFoodObj(GameObject i_GameObject)
    {
        m_IsHoldingFoodObj = i_GameObject != null;
    }

    public void OnPlayerFoodThrown()
    {
        m_IsHoldingFoodObj = false;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        CollisionEnter?.Invoke(collision);
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        CollisionExit?.Invoke(collision);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            PickUpZonEnter?.Invoke(other);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            PickUpZonExit?.Invoke(other);
        }
    }

    internal void OnGameOver()
    {

    }

    public static bool isInReng(float i_x, float i_Max, float i_Min, out float res)
    {
        bool isMinBigger = i_x < i_Min;
        bool isMaxSmaller = i_x > i_Max;
        res = isMinBigger ? i_Min : isMaxSmaller ? i_Max : i_x;

        return !(isMinBigger || isMaxSmaller);
    }

    public void Builder(PlayerData i_PlayerData, GameObject spawnPlayer)
    {
        if (i_PlayerData.IsInitialize)
        {
            Debug.LogError("player cant be  Initialize twice");
            return;
        }
        Debug.Log("Builder :" + i_PlayerData.r_Name);
        SetCamToDiffDisply(i_PlayerData, spawnPlayer);
        i_PlayerData = AddAbstrcts(i_PlayerData, spawnPlayer,
                        out PlayerMovement movement, out PlayerHand playerHand);
        PlayerMovementImplementor playerMovementImplementor = null;
        AcitonHandStateMachine acitonHandStateMachine = null;

        switch (i_PlayerData.r_ModeType)
        {
            case ePlayerModeType.OfflinePlayer:
                AddPlayerUi(spawnPlayer);
                playerMovementImplementor = spawnPlayer.AddComponent<OfflinePlayerMovement>();
                playerMovementImplementor.SetPlayerMovement(movement);
                acitonHandStateMachine = spawnPlayer.AddComponent<OfflinePlayerStateMachine>();
                acitonHandStateMachine.SetPlayerHand(playerHand);
                acitonHandStateMachine.BuildState();
                break;
            case ePlayerModeType.OnlinePlayer:
                Debug.Log("Builder  PlayerMovement : OnlinePlayer");
                return;
            case ePlayerModeType.OfflineAiPlayer:
                playerMovementImplementor = spawnPlayer.AddComponent<OfflineAIMovement>();
                playerMovementImplementor.SetPlayerMovement(movement);
                acitonHandStateMachine = spawnPlayer.AddComponent<OfflineAIStateMachine>();
                acitonHandStateMachine.SetPlayerHand(playerHand);
                acitonHandStateMachine.BuildState();
                break;
            case ePlayerModeType.OnlineAiPlayer:
                Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
                return;
            case ePlayerModeType.OfflineTestPlayer:
                PlayerMovementStub PlayerMovmentSub = spawnPlayer.AddComponent<PlayerMovementStub>();
                PlayerMovmentSub.SetPlayerMovement(movement);
                break;
            case ePlayerModeType.OnlineTestPlayer:
                Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
                return;
            default:
                return;
        }

        AddingListenersToAnimationEvent(playerHand, acitonHandStateMachine);
        AddingListeners(playerHand, acitonHandStateMachine);
    }

    private PlayerData AddAbstrcts(PlayerData i_PlayerData,
                                    GameObject spawnPlayer,
                                    out PlayerMovement o_Movement,
                                    out PlayerHand o_PlayerHand
        )
    {
        i_PlayerData.Init(spawnPlayer);
        o_Movement = spawnPlayer.AddComponent<PlayerMovement>();
        o_PlayerHand = spawnPlayer.AddComponent<PlayerHand>();
        o_PlayerHand.SetPickUpPoint(m_PickUpPoint.transform);
        return i_PlayerData;
    }

    private void AddingListeners(PlayerHand i_PlayerHand, AcitonHandStateMachine i_StateMachine)
    {
        i_StateMachine.Powering.OnPower += ChangeForce;
        i_StateMachine.Free.PlayerCollectedFood += OnPlayerSetFoodObj;
        i_StateMachine.Free.PlayerCollectedFood += i_StateMachine.OnPlayerSetFoodObj;
        i_StateMachine.Free.PlayerCollectedFood += i_PlayerHand.SetGameFoodObj;
        PickUpZonEnter += i_StateMachine.Free.EnterCollisionFoodObj;
        PickUpZonExit += i_StateMachine.Free.ExitCollisionFoodObj;
    }

    private void AddingListenersToAnimationEvent(PlayerHand i_PlayerHand, AcitonHandStateMachine i_StateMachine)
    {
        PlayerAnimationChannel animationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();

        if (animationChannel != null)
        {
            animationChannel.onThrowPoint += i_PlayerHand.ThrowObj;
            animationChannel.onThrowPoint += i_StateMachine.Throwing.ThrowingPointObj;
            animationChannel.onThrowPoint += CoroutinePoweringState(i_StateMachine.Powering);
        }
        else
        {
            Debug.Log(" animationChannel is null");
        }
    }

    private void AddPlayerUi(GameObject spawnPlayer)
    {
        PlayerUi playerUi = spawnPlayer.AddComponent<PlayerUi>();
        PlayerForceChange += playerUi.OnPlayerForceChange;
    }

    private static void SetCamToDiffDisply(PlayerData i_PlayerData, GameObject spawnPlayer)
    {
        Camera cam = spawnPlayer.GetComponentInChildren<Camera>();
        cam.targetDisplay = i_PlayerData.r_PlayerNum;
    }

    private Action CoroutinePoweringState(StatePowering poweringState)
    {
       return () => StartCoroutine(poweringState.OnTringPoint());
    }
}
