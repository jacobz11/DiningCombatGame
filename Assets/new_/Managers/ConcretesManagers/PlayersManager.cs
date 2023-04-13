using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DiningCombat.Player.Manger;
using DiningCombat.FoodObj.Managers;
using System.Linq;

namespace DiningCombat.Managers
{
    internal class PlayersManager : IElementsViewer
    {
        private static PlayersManager s_Singlton;
        public static PlayersManager Singlton
        {
            get => s_Singlton;
            private set => s_Singlton = value;
        }

        private List<PlayerData> m_PlayersDatas = new List<PlayerData>();
        private List<GameObject> m_Players = new List<GameObject>();
        public void InitPlayer()
        {
            foreach (PlayerData player in GameManager.Singlton.GetPlayersInitialization())
            {
                GameObject spawnPlayer = GameObject.Instantiate(player.r_Prefap, player.r_InitPos, player.r_Quaternion);
                IntiraelPlayerManger channel = spawnPlayer.GetComponentInChildren<IntiraelPlayerManger>();
                channel.Builder(player, spawnPlayer);
                m_PlayersDatas.Add(player);
                m_Players.Add(spawnPlayer);
            }
        }

        internal void OnGameOver()
        {
            // TDOO : this 
            throw new NotImplementedException();
        }

        public static PlayersManager InitPlayersManager()
        {
            if (Singlton == null)
            {
                PlayersManager instance = new PlayersManager();
                GameManager.Singlton.GameOver += instance.OnGameOver;
                Singlton = instance;
            }
            else
            {
                Debug.LogWarning("Singlton is not null");
            }

            return Singlton;
        }

        public List<Vector3> GetElements()
        {
            List<Vector3> res = new List<Vector3>();
            m_Players.ForEach(p => { res.Add(p.gameObject.transform.position); });
            return res;
        }

        public bool FindTheNearestOne(Vector3 i_From, out Vector3 o_NearestElement)
        {
            bool res = false;
            List<Vector3> allPos = GetElements();

            if (allPos.Count < 0)
            {
                allPos.OrderBy(other => Vector3.Distance(i_From, other));
                o_NearestElement = allPos.ElementAt(0);
                res = true;
            }
            else
            {
                o_NearestElement = Vector3.zero;
            }

            return res;
        }
    }
}