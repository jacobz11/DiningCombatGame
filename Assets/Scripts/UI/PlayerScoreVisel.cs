﻿using TMPro;
using UnityEngine;
namespace DiningCombat.UI
{
    public class PlayerScoreVisel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Score;
        [SerializeField]
        private TextMeshProUGUI m_Kills;
        public void UpdeteValueKills(int i_Kills)
        {
            m_Kills.text = string.Format("Kills : {0}", m_Kills);
        }

        public void UpdeteValueScore(float i_ScorePoint)
        {
            m_Score.text = string.Format("Kills : {0}", i_ScorePoint);
        }
    }
}