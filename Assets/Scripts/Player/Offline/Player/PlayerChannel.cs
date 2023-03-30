using Assets.Scripts.Test.Stubs;
using DiningCombat;
using DiningCombat.FoodObj;
using DiningCombat.Player.Manger;
using DiningCombat.Player.Offline.Movement;
using DiningCombat.Player.UI;
using DiningCombat.Player;
using System;
using UnityEngine;
using Util.Abstraction;
using static DiningCombat.GameGlobal;
using DiningCombat.Player.Offline.State;
using Assets.Scrips_new.AI.Stats;
using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using Assets.Scripts.Player.Offline.AI.Stats;

public class PlayerChannel : MonoBehaviour, IInternalChannel
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
        if (!isInReng(i_AdditionForce,
            MaxAdditionForce,
            MinAdditionForce,
            out float o_ForceAdding))
        {
            Debug.LogError("How the AdditionForce "+ i_AdditionForce + " is that out of range?");
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
            Debug.LogError("How the NewForce "+ o_ForceAdding + "is that out of range?");
        }

        //Debug.Log(", ForceMull + o_ForceAdding " + ForceMull + o_ForceAdding + ", o_NewForce : "+o_NewForce);
        //m_Force = o_NewForce;
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
        Debug.Log("Builder :" + i_PlayerData.m_Name);
        //GameObject spawnPlayer = Instantiate(i_PlayerData.m_Prefap, i_PlayerData.m_InitPos, i_PlayerData.m_Quaternion);
        Camera cam = spawnPlayer.GetComponentInChildren<Camera>();
        cam.targetDisplay = i_PlayerData.m_PlayerNum;
        i_PlayerData.Init(spawnPlayer);

        spawnPlayer.name = i_PlayerData.m_Name;
        spawnPlayer.tag = TagNames.k_Player;

        PlayerMovement movement = spawnPlayer.AddComponent<PlayerMovement>();
        PlayerHand playerHand = spawnPlayer.AddComponent<PlayerHand>();
        playerHand.SetPickUpPoint(m_PickUpPoint.transform);
        PlayerMovementImplementor playerMovementImplementor = null;
        AcitonHandStateMachine acitonHandStateMachine = null;
        StateFree freeState = null;
        StateHoldsObj stateHolding = null;
        StatePowering poweringState = null;
        StateThrowing stateThrowing = null;

        switch (i_PlayerData.m_ModeType)
        {
            case ePlayerModeType.OfflinePlayer:
                PlayerUi playerUi = spawnPlayer.AddComponent<PlayerUi>();
                Debug.Log("Builder playerUi :");
                PlayerForceChange += playerUi.OnPlayerForceChange;
                Debug.Log("Builder playerUi.OnPlayerForceChange :");

                Debug.Log("Builder  PlayerMovement : OfflinePlayer");
                playerMovementImplementor = spawnPlayer.AddComponent<OfflinePlayerMovement>();
                playerMovementImplementor.SetPlayerMovement(movement);

                OfflinePlayerStateMachine offlinePlayerHandStateMachine = spawnPlayer.AddComponent<OfflinePlayerStateMachine>();
                offlinePlayerHandStateMachine.SetPlayerHand(playerHand);

                freeState = new StateFreeOffline(playerHand, offlinePlayerHandStateMachine);
                stateHolding = new StateHoldsObjOffline(playerHand, offlinePlayerHandStateMachine);
                poweringState = new StatePoweringOffline(playerHand, offlinePlayerHandStateMachine);
                stateThrowing = new StateThrowingOffline(playerHand, offlinePlayerHandStateMachine);
                acitonHandStateMachine =(AcitonHandStateMachine) offlinePlayerHandStateMachine;
                break;
            case ePlayerModeType.OnlinePlayer:
                Debug.Log("Builder  PlayerMovement : OnlinePlayer");
                return;
            case ePlayerModeType.OfflineAiPlayer:
                Debug.Log("Builder  PlayerMovement : OfflineAiPlayer");
                OfflineAIMovement offlineAIMovement = spawnPlayer.AddComponent<OfflineAIMovement>();
                offlineAIMovement.SetPlayerMovement(movement);

                OfflineAIStateMachine offlineAIHandStateMachine = spawnPlayer.AddComponent<OfflineAIStateMachine>();
                offlineAIHandStateMachine.SetPlayerHand(playerHand);

                freeState = new FreeHandOfflineAI(playerHand, offlineAIHandStateMachine);
                stateHolding = new StateHoldsObjOfflineAI(playerHand, offlineAIHandStateMachine);
                poweringState = new StatePoweringOfflineAI(playerHand, offlineAIHandStateMachine);
                stateThrowing = new StateThrowingOfflineAI(playerHand, offlineAIHandStateMachine);
                acitonHandStateMachine = (AcitonHandStateMachine)offlineAIHandStateMachine;
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
        poweringState.OnPower += ChangeForce;
        freeState.PlayerCollectedFood += OnPlayerSetFoodObj;
        freeState.PlayerCollectedFood += acitonHandStateMachine.OnPlayerSetFoodObj;
        freeState.PlayerCollectedFood += playerHand.SetGameFoodObj;
        PickUpZonEnter += freeState.EnterCollisionFoodObj;
        PickUpZonExit += freeState.ExitCollisionFoodObj;
        acitonHandStateMachine.SetStates(freeState, stateHolding, poweringState, stateThrowing);
        Debug.Log("Builder end :" + i_PlayerData.m_Name);

        // TODO : this 
        //PlayerMovement.Builder(m_Player, m_ModeType, out PlayerMovement movement,
        //    out PlayerMovementImplementor implementor);

        //PlayerHand.Builder(m_Player, m_ModeType, out PlayerHand o_PlayerHand,
        //    out OfflinePlayerStateMachine o_StateMachineImplemntor);
    }

    private void playerChannel_PickUpZonEnter(Collider obj)
    {
        throw new NotImplementedException();
    }
}
