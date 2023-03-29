using Assets.Scripts.Test.Stubs;
using DiningCombat;
using DiningCombat.FoodObj;
using DiningCombat.Player.Manger;
using DiningCombat.Player.Offline.Movement;
using DiningCombat.Player.UI;
using DiningCombat.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Abstraction;
using static DiningCombat.GameGlobal;
using DesignPatterns.Abstraction;
using DiningCombat.Player.Offline.State;

public class PlayerChannel : MonoBehaviour, IInternalChannel
{
    public const int k_KillMullPonit = 100;
    private const float k_MinAdditionForce = 20;
    private const float k_MaxAdditionForce = 200;
    private const float k_MaxForce = 2000;
    private const float k_MinForce = 0;

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

    public void FoodThrownByPlayerHit(GameFoodObj i_FoodThrown,
        float i_ScoreHitPoint,
        int i_ThrownKills)
    {
        if (i_FoodThrown == null)
        {
            Debug.LogError("FoodThrown is null: Validation Fails");
            return;
        }

        m_Kills += i_ThrownKills;
        m_Score += (int)i_ScoreHitPoint + i_ThrownKills * k_KillMullPonit;
        PlayerScoreChange?.Invoke(m_Score);
    }

    public void ChangeForce(float i_AdditionForce)
    {
        if (!isInReng(i_AdditionForce,
            k_MaxAdditionForce,
            k_MinAdditionForce,
            out float o_ForceAdding))
        {
            Debug.LogError("How the AdditionForce is that out of range?");
        }

        if (!isInReng(ForceMull + o_ForceAdding,
            k_MaxForce,
            k_MinForce,
            out float o_NewForce))
        {
            Debug.LogError("How the NewForce is that out of range?");
        }

        m_Force = o_NewForce;
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

        return isMinBigger && isMaxSmaller;
    }

    public void Builder(PlayerData i_PlayerData, GameObject spawnPlayer)
    {
        if (i_PlayerData.IsInitialize)
        {
            Debug.LogError("player cant be  Initialize twice");
            return;
        }

        //GameObject spawnPlayer = Instantiate(i_PlayerData.m_Prefap, i_PlayerData.m_InitPos, i_PlayerData.m_Quaternion);
        Camera cam = spawnPlayer.GetComponentInChildren<Camera>();
        cam.targetDisplay = i_PlayerData.m_PlayerNum;
        i_PlayerData.Init(spawnPlayer);

        spawnPlayer.name = i_PlayerData.m_Name;
        spawnPlayer.tag = TagNames.k_Player;

        PlayerMovement movement = spawnPlayer.AddComponent<PlayerMovement>();
        PlayerHand playerHand = spawnPlayer.AddComponent<PlayerHand>();
        playerHand.SetPickUpPoint(m_PickUpPoint.transform);
        switch (i_PlayerData.m_ModeType)
        {
            case ePlayerModeType.OfflinePlayer:
                Debug.Log("Builder  PlayerMovement : OfflinePlayer");
                OfflinePlayerMovement offlinPlayerMovment = spawnPlayer.AddComponent<OfflinePlayerMovement>();
                offlinPlayerMovment.SetPlayerMovement(movement);

                OfflinePlayerStateMachine offlinePlayerHandStateMachine = spawnPlayer.AddComponent<OfflinePlayerStateMachine>();
                offlinePlayerHandStateMachine.SetPlayerHand(playerHand);

                List<DCState> states = new List<DCState>();
                StateFreeOffline free = spawnPlayer.AddComponent<StateFreeOffline>();
                free.StateId = 0;
                states.Add(free);
                free.PlayerCollectedFood += OnPlayerSetFoodObj;
                free.PlayerCollectedFood += offlinePlayerHandStateMachine.OnPlayerSetFoodObj;
                free.PlayerCollectedFood += playerHand.SetGameFoodObj;
                PickUpZonEnter += free.EnterCollisionFoodObj;
                PickUpZonExit += free.ExitCollisionFoodObj;

                StateHoldsObjOffline holdsObj = spawnPlayer.AddComponent<StateHoldsObjOffline>();
                holdsObj.StateId = 1;
                states.Add(holdsObj);

                StatePoweringOffline powering = spawnPlayer.AddComponent<StatePoweringOffline>();
                powering.StateId = 2;
                states.Add(powering);

                StateThrowingOffline throwing = spawnPlayer.AddComponent<StateThrowingOffline>();
                throwing.StateId = 3;
                states.Add(throwing);

                offlinePlayerHandStateMachine.SetStates(states);
                //offlinePlayerHandStateMachine.BuildOfflinePlayerState();
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
                offlineAIHandStateMachine.BuildOfflineAIState();
                return;
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

        // TODO : this 
        //PlayerMovement.Builder(m_Player, m_ModeType, out PlayerMovement movement,
        //    out PlayerMovementImplementor implementor);

        //PlayerHand.Builder(m_Player, m_ModeType, out PlayerHand o_PlayerHand,
        //    out OfflinePlayerStateMachine o_StateMachineImplemntor);

        spawnPlayer.AddComponent<PlayerUi>();
    }

    private void playerChannel_PickUpZonEnter(Collider obj)
    {
        throw new NotImplementedException();
    }
}
