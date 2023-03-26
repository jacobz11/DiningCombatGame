using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DiningCombat.Channels.Player;
using static DiningCombat.GameGlobal;
using Abstraction.Player.DiningCombat;
using Abstraction.DiningCombat.Player;

namespace DiningCombat
{
    namespace Managers
    {
        internal class PlayersManager : IManager<PlayersMangerChannel>
        {
            private static PlayersManager s_Singlton;
            private List<PlayerData> m_PlayersDatas = new List<PlayerData>();
            public static PlayersManager Singlton
            {
                get
                {
                    return s_Singlton;
                }
                private set
                {
                    s_Singlton = value;
                }
            }

            public void InitPlayer()
            {
                foreach (PlayerData player in s_GameManager.GetPlayersInitialization())
                {
                    GameObject spawn = Instantiate(player.m_Prefap, player.m_InitPos, player.m_Quaternion);
                    m_PlayersDatas.Add(player);
                    player.Init(spawn);

                }
            }

            internal override IEnumerable MainCoroutine()
            {
                yield return null;
            }

            internal override void OnGameOver()
            {
                throw new NotImplementedException();
            }

            public static PlayersManager InitPlayersManager()
            {
                if (Singlton == null)
                {
                    PlayersManager instance = s_GameManager.AddComponent<PlayersManager>();
                    //instance.SetFoodPrefab();
                    s_GameManager.GameOver += instance.OnGameOver;
                    Singlton = instance;
                }
                else
                {
                    Debug.LogWarning("Singlton is not null");
                }

                return Singlton;
            }

            //protected override IManager<PlayersMangerChannel> Instance()
            //{
            //    if (s_Singlton == null)
            //    {
            //        PlayersManager playerManager = s_GameManager.AddComponent<PlayersManager>();
            //        playerManager.InitPlayer();
            //        s_Singlton = playerManager;
            //    }

            //    return s_Singlton;
            //}
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
                PlayerMovement.Builder(m_Player, m_ModeType);
                PlayerHand.Builder(m_Player, m_ModeType);
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
