using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiningCombat.Manger
{
    public class PlayerChannel
    {
        public struct PlayerStatus
        {
            public const string k_Formt = "{0}\t\t{1}\t{2}\t{3}\n";
            public static readonly string sr_TableTop =
                string.Format(k_Formt, "Name", GameGlobal.TextVer.k_ScorePoint, GameGlobal.TextVer.k_Kills, "IsAlive");
            public int Id;
            public string Name;
            public float Score;
            public int Kills;
            public bool IsAlive;
            public override string ToString()
            {
                return string.Format(k_Formt, Name, Score, Kills, IsAlive ? "alive" : "killed");
            }
        }
        private Dictionary<string, PlayerStatus> m_Table = new Dictionary<string, PlayerStatus>();

        public void AddPlayer(string i_Name)
        {
            if (m_Table.ContainsKey(i_Name))
            {
                UnityEngine.Debug.Log($"Player-Channel is Contains Key {i_Name}");
                return;
            }

            m_Table[i_Name] = new PlayerStatus()
            {
                Name = i_Name,
                Score = 0,
                Kills = 0,
                IsAlive = true,
                Id = 0,
            };

        }
        public void UpdatePlayer(string i_Player, int i_Kills)
        {
            if (!m_Table.ContainsKey(i_Player))
            {
                UnityEngine.Debug.Log($"Player-Channel is not Contains Key {i_Player}");
                return;
            }

            PlayerStatus playerStatus = m_Table[i_Player];
            m_Table[i_Player] = new PlayerStatus()
            {
                Id = playerStatus.Id,
                Name = playerStatus.Name,
                Score = playerStatus.Score,
                Kills = i_Kills,
                IsAlive = playerStatus.IsAlive,
            };
        }

        public void UpdatePlayer(string i_Player, float i_Score)
        {
            if (!m_Table.ContainsKey(i_Player))
            {
                UnityEngine.Debug.Log($"Player-Channel is not Contains Key {i_Player}");
                return;
            }

            PlayerStatus playerStatus = m_Table[i_Player];
            m_Table[i_Player] = new PlayerStatus()
            {
                Id = playerStatus.Id,
                Name = playerStatus.Name,
                Score = i_Score,
                Kills = playerStatus.Kills,
                IsAlive = playerStatus.IsAlive,
            };
        }
        public void UpdatePlayer(string i_Player)
        {
            if (!m_Table.ContainsKey(i_Player))
            {
                UnityEngine.Debug.Log($"Player-Channel is not Contains Key {i_Player}");
                return;
            }

            PlayerStatus playerStatus = m_Table[i_Player];
            m_Table[i_Player] = new PlayerStatus()
            {
                Id = playerStatus.Id,
                Name = playerStatus.Name,
                Score = playerStatus.Score,
                Kills = playerStatus.Kills,
                IsAlive = false,
            };
        }

        public List<PlayerStatus> GetTable()
        {
            return m_Table.Values.ToList().OrderBy(p => p.IsAlive).ThenBy(p => p.Score).ToList();
        }

        public string GetPrintabulTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PlayerStatus.sr_TableTop);

            foreach (PlayerStatus status in GetTable())
            {
                sb.Append(status.ToString());
            }

            return sb.ToString();
        }
    }
}