using System;
using TMPro;
using UnityEngine;

internal class PlayerScoreVisel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_Score;
    [SerializeField]
    private TextMeshProUGUI m_Kills;
    internal void UpdeteValueKills(int i_Kills)
    {
        m_Kills.text = string.Format("Kills : {0}", m_Kills);
    }

    internal void UpdeteValueScore(float i_ScorePoint)
    {
        m_Score.text = string.Format("Kills : {0}", i_ScorePoint);
    }
}