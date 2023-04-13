using UnityEngine;
using static DiningCombat.GameGlobal;

namespace DiningCombat.Player.Manger
{
    public struct PlayerData
    {
        private static int s_Cunter =0;
        #region data
        public readonly Vector3 r_InitPos;
        public readonly Quaternion r_Quaternion;
        public readonly GameObject r_Prefap;
        public readonly string r_Name;
        public readonly ePlayerModeType r_ModeType;
        public readonly int r_PlayerNum;
        private GameObject m_Player;
        private bool m_IsInit;
        #endregion
        public bool IsInitialize => m_IsInit;

        private static int getCunter()
        {
            int res = s_Cunter;
            s_Cunter++;
            return res;
        }
        public bool IsAi => r_ModeType == ePlayerModeType.OfflineAiPlayer
            || r_ModeType == ePlayerModeType.OnlineAiPlayer;

        public PlayerData(GameObject i_Prefap, string i_Name,
            ePlayerModeType i_ModeType, Vector3 i_InitPos)
        {
            r_Prefap = i_Prefap;
            r_ModeType = i_ModeType;
            r_Name = i_Name;
            r_InitPos = i_InitPos;
            r_Quaternion = Quaternion.identity;
            m_Player = null;
            m_IsInit = false;
            r_PlayerNum = getCunter();
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
            i_Player.name = r_Name;
            i_Player.tag = TagNames.k_Player;
        }

        public bool GetPosition(out Vector3 o_Position)
        {
            bool isPlayerAllive = m_Player != null;
            o_Position = isPlayerAllive ? m_Player.transform.position : Vector3.zero;

            return isPlayerAllive;
        }
    }
}
