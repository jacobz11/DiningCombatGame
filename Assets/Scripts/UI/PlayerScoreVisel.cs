using TMPro;
using UnityEngine;
namespace DiningCombat.UI
{
    public class PlayerScoreVisel : MonoBehaviour
    {
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
            m_Kills.text = string.Format("Kills : {0}", i_Kills);
        }

        public void UpdeteValueScore(float i_ScorePoint)
        {
            m_Score.text = string.Format("Score : {0}", i_ScorePoint);
        }
    }
}