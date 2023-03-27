using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DiningCombat.Channels.Player;
using DiningCombat.Player.Manger;

namespace DiningCombat.Managers
{
    internal class PlayersManager : IManager<PlayersMangerChannel>
    {
        private static PlayersManager s_Singlton;
        private List<Player.Manger.PlayerData> m_PlayersDatas = new List<Player.Manger.PlayerData>();
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
                InternalMangerPlayer.Builder(player);
                m_PlayersDatas.Add(player);
            }
        }

        internal override IEnumerable MainCoroutine()
        {
            // TDOO : this 
            yield return null;
        }

        internal override void OnGameOver()
        {
            // TDOO : this 
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

