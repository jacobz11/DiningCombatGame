//using DiningCombat;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DiningCombat.Managers;
//using static DiningCombat.GameGlobal;

//namespace DiningCombat
//{
//    internal class GameManagerTraining : GameManager
//    {
//        [SerializeField] GameObject[] room;
//        private int m_Connter = -1;
//        [SerializeField]
//        [Range(1, 10)]
//        private int m_NumOfPlayerInRoom ;

//        private int Cunnter
//        {
//            get 
//            {
//                m_Connter = (m_Connter+1) % room.Length;
//                return m_Connter; 
//            }

//        }

//        public override Vector3 GetRandomPositionInMap()
//        {
//            return room[Cunnter].transform.position + base.GetRandomPositionInMap();
//        }

//        internal void ResetRoom()
//        {
//            throw new NotImplementedException();
//        }

//        internal override List<Player.Manger.PlayerData> GetPlayersInitialization()
//        {
//            List<Player.Manger.PlayerData> res = new List<Player.Manger.PlayerData>();
//            string name = null;
//            for (int i = 0; i < room.Length * m_NumOfPlayerInRoom ; i++)
//            {
//                name = i.ToString();
//                res.Add(new Player.Manger.PlayerData(m_PlayrPrefab, "player",
//                    ePlayerModeType.MLTrining, GetRandomPositionInMap()));
//            }

//            return res;
//        }
//    }
//}