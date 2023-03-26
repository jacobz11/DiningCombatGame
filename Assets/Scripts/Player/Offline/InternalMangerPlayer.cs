using Assets.Scrips_new.AI.Algo;
using Assets.Scrips_new.Util.Channels.Internal;
using DiningCombat.FoodObj;
using DiningCombat.Player.UI;
using System;
using System.Collections;
using UnityEngine;
using static DiningCombat.GameGlobal;

namespace DiningCombat.Player.Manger
{
    internal class InternalMangerPlayer : IManager<PlayerInternalChannel>
    {
        public const int k_KillMullPonit = 100;
        private const float k_MinAdditionForce = 20;
        private const float k_MaxAdditionForce = 200;
        private const float k_MaxForce = 2000;
        private const float k_MinForce = 0;

        public event Action<float> PlayerForceChange;
        public event Action<int> PlayerScoreChange;
        public event Action<int> LifePointChange;
        public event Action PlayerDead;

        private int m_Score = 0;
        private int m_Kills = 0;
        private int m_LifePoint;
        private float m_Force = 0;
        private bool m_IsHoldingFoodObj = false;

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
            get=> m_IsHoldingFoodObj;
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

            if (!isInReng(ForceMull+ o_ForceAdding,
                k_MaxForce,
                k_MinForce,
                out float o_NewForce))
            {
                Debug.LogError("How the NewForce is that out of range?");
            }

            m_Force = o_NewForce;
            PlayerForceChange?.Invoke(m_Force);
        }

        public void FruitHitPlayer(float i_HitForce, out bool o_IsKill)
        {
            if (i_HitForce < 0)
            {
                throw new Exception("Why negative mf");
            }

            m_LifePoint -= (int) i_HitForce;
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

        protected static InternalMangerPlayer InitPlayerInternalManger()
        {
            // TODO : fix it 
            Debug.Log("TODO: Fix it");
            Debug.LogError("Player Internal Manger is not heve a Instance");
            throw new NotImplementedException();
        }

        internal override IEnumerable MainCoroutine()
        {
            // TODO : fix it 
            Debug.Log("TODO: Fix it");
            Debug.LogError("Player Internal Manger is not heve a MainCoroutine");
            throw new NotImplementedException();
        }

        internal override void OnGameOver()
        {

        }

        public static bool isInReng(float i_x, float i_Max, float i_Min, out float res)
        {
            bool isMinBigger = i_x < i_Min;
            bool isMaxSmaller = i_x > i_Max;
            res = isMinBigger ? i_Min : isMaxSmaller ? i_Max : i_x;

            return isMinBigger && isMaxSmaller;
        }
        internal struct PlayerData
        {
            public readonly Vector3 m_InitPos;
            public readonly Quaternion m_Quaternion;
            public readonly GameObject m_Prefap;
            public readonly string m_Name;
            private GameObject m_Player;
            private ePlayerModeType m_ModeType;
            private bool m_IsInit;

            public bool IsAi => m_ModeType == ePlayerModeType.OfflineAiPlayer
                || m_ModeType == ePlayerModeType.OnlineAiPlayer;

            public PlayerData(GameObject i_Prefap, string i_Name,
                ePlayerModeType i_ModeType, Vector3 i_InitPos)
            {
                m_Prefap = i_Prefap;
                m_ModeType = i_ModeType;
                m_Name = i_Name;
                m_InitPos = i_InitPos;
                m_Quaternion = Quaternion.identity;
                m_Player = null;
                m_IsInit = false;
            }

            internal void Init(GameObject i_Player)
            {
                if (i_Player == null)
                {
                    Debug.LogError("Init i_Player is unscscful");
                    return;
                }

                if (m_IsInit)
                {
                    Debug.LogError("the Initialization the player Can only happen once");
                    return;
                }

                m_Player = i_Player;
                m_Player.name = m_Name;
                m_Player.tag = TagNames.k_Player;
                InternalMangerPlayer manger = m_Player.AddComponent<InternalMangerPlayer>();

                PlayerMovement.Builder(m_Player, m_ModeType, out PlayerMovement movement,
                    out PlayerMovementImplementor implementor);

                PlayerHand.Builder(m_Player, m_ModeType, out PlayerHand o_PlayerHand,
                    out OfflinePlayerStateMachine o_StateMachineImplemntor);

                m_Player.AddComponent<PlayerUi>();

                m_IsInit = true;
            }

            public bool GetPosition(out Vector3 o_Position)
            {
                bool isPlayerAllive = m_Player != null;
                o_Position = isPlayerAllive ? m_Player.transform.position : Vector3.zero;

                return isPlayerAllive;
            }
        }
    }
}
