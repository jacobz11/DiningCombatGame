using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DiningCombat.Channels.Player;
using static DiningCombat.GameGlobal;
using Abstraction.Player.DiningCombat;
using Abstraction.DiningCombat.Player;
using Player;

namespace DiningCombat
{
    namespace Managers
    {
        internal class PlayersManager : IManager<PlayersMangerChannel>
        {
            private static PlayersManager s_Singlton;
            private List<PlayerInternalManger.PlayerData> m_PlayersDatas = new List<PlayerInternalManger.PlayerData>();
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
                foreach (PlayerInternalManger.PlayerData player in s_GameManager.GetPlayersInitialization())
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
    }
}
