using Assets.Scrips_new.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static DiningCombat.GameGlobal;

namespace Assets.Scrips_new.Managers
{
    internal class PlayerManager : IManager<PlayerChannel>
    {
        private static PlayerManager s_Singlton;
        private List<PlayerData> m_PlayerDatas = new List<PlayerData>();

        private void InitPlayer()
        {
            foreach(PlayerData player in s_GameManager.GetPlayersInitialization())
            {
                GameObject spawn = Instantiate(player.m_Prefap, player.m_InitPos, player.m_Quaternion);
                m_PlayerDatas.Add(player);
                player.Init(spawn);
            }
        }

        public override IEnumerable MainCoroutine()
        {
            yield return null;
        }

        public override void OnGameOver()
        {
            throw new NotImplementedException();
        }

        protected override IManager<PlayerChannel> Instance()
        {
            if (s_Singlton == null)
            {
                PlayerManager playerManager = s_GameManager.AddComponent<PlayerManager>();
                playerManager.InitPlayer();
                s_Singlton = playerManager;
            }

            return s_Singlton;
        }
    }

    internal struct PlayerData
    {
        public GameObject m_Prefap;
        public Vector3 m_InitPos;
        public Quaternion m_Quaternion;
        private GameObject m_Player;
        private string m_Name;
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
            PlayerMovement.Builder(m_Player, m_ModeType);
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
