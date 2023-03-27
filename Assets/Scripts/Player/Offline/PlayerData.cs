using DiningCombat.Player.UI;
using UnityEngine;
using static DiningCombat.GameGlobal;

namespace DiningCombat.Player.Manger
{
        internal struct PlayerData
        {
            public readonly Vector3 m_InitPos;
            public readonly Quaternion m_Quaternion;
            public readonly GameObject m_Prefap;
            public readonly string m_Name;
            public readonly ePlayerModeType m_ModeType;
            private GameObject m_Player;
            private bool m_IsInit;
            public bool IsInitialize => m_IsInit;

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
