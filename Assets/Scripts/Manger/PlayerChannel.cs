using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Manger
{
    public class PlayerChannel
    {
        public struct PlayerStatus
        {
            public int Id;
            public string Name;
            public float Score;
            public int Kills;
            public bool IsAlive;
        }
        private Dictionary<string, PlayerStatus> m_Table = new Dictionary<string, PlayerStatus>();
        
        public void UpdatePlayer(string i_Player, float i_Score, int i_Kills)
        {
            if (!m_Table.ContainsKey(i_Player))
            {
                UnityEngine.Debug.Log($"Player-Channel is not Contains Key {i_Player}");
                return;
            }

            PlayerStatus playerStatus = m_Table[i_Player];
            m_Table[i_Player]= new PlayerStatus()
            {
                Id = playerStatus.Id, 
                Name = playerStatus.Name, 
                Score = i_Score,
                Kills = i_Kills,
                IsAlive = playerStatus.IsAlive,
            };
        }

        public List<PlayerStatus> GetTable()
        {
            return m_Table.Values.ToList().OrderBy(p => p.IsAlive).ThenBy(p => p.Score).ToList();
        }
    }
}