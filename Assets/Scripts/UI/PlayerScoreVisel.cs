using TMPro;
using UnityEngine;
namespace DiningCombat.UI
{
    public class PlayerScoreVisel : MonoBehaviour
    {
        public const string k_Foormt = "{0} : {1}";
        public static PlayerScoreVisel Instance { get; private set; }
        [SerializeField]
        private TextMeshProUGUI m_Score;
        [SerializeField]
        private TextMeshProUGUI m_Kills;

        private void Awake()
        {
            Instance = this;
        }
        public void UpdeteValueKills(int i_Kills)
        {
            m_Kills.text = string.Format(k_Foormt, GameGlobal.TextVer.k_Kills, i_Kills);
        }

        public void UpdeteValueScore(float i_ScorePoint)
        {
            m_Score.text = string.Format(k_Foormt, GameGlobal.TextVer.k_ScorePoint, i_ScorePoint);
        }
    }
}