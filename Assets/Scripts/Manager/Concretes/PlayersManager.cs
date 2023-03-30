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
            foreach (PlayerData player in GameManager.Singlton.GetPlayersInitialization())
            {
                GameObject spawnPlayer = Instantiate(player.r_Prefap, player.r_InitPos, player.r_Quaternion);
                IntiraelPlayerManger channel = spawnPlayer.GetComponentInChildren<IntiraelPlayerManger>();
                channel.Builder(player, spawnPlayer);
                m_PlayersDatas.Add(player);
            }
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
                PlayersManager instance = GameManager.Singlton.AddComponent<PlayersManager>();
                //instance.SetFoodPrefab();
                GameManager.Singlton.GameOver += instance.OnGameOver;
                Singlton = instance;
            }
            else
            {
                Debug.LogWarning("Singlton is not null");
            }

            return Singlton;
        }

        internal override IEnumerator MainCoroutine()
        {
            throw new NotImplementedException();
        }
    }
}

