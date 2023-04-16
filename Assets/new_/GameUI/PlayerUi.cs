//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace DiningCombat.Player.UI
//{
//    internal class PlayerUi : MonoBehaviour
//    {
//        private float m_LifePoint;
//        private int m_ScoreValue = 0;
//        private string m_PlayerFormtToShow;
//        private Text m_Score;
//        private FilliStatus m_HpSlder;
//        private FilliStatus m_ForceMultiUi;

//        private float HP
//        {
//            get => m_LifePoint;
//            set
//            {
//                m_HpSlder.UpdateFilliStatus = m_LifePoint;
//            }
//        }
//        public int ScoreValue
//        {
//            get => m_ScoreValue;
//            set
//            {
//                m_ScoreValue = value;
//                m_Score.text = string.Format(m_PlayerFormtToShow, m_ScoreValue);
//            }
//        }

//        private void Awake()
//        {
//            Canvas canvas = Component.FindObjectOfType<Canvas>();
//            if (canvas is null)
//            {
//                Debug.LogError("canvas is null");
//                return;
//            }

//            if (GameManager.Singlton.GetPrefabUiHP(out GameObject o_HPrefab))
//            {
//                m_HpSlder = (Instantiate(o_HPrefab, canvas.transform)).GetComponent<FilliStatus>();
//                if (m_HpSlder is null)
//                {
//                    Debug.LogWarning("m_HpSlder is null");
//                }
//            }

//            if (GameManager.Singlton.GetPrefabUIPower(out GameObject o_PowerPrefab))
//            {
//                m_ForceMultiUi = (Instantiate(o_PowerPrefab, canvas.transform)).GetComponent<FilliStatus>();
//                if (m_ForceMultiUi is null)
//                {
//                    Debug.LogWarning("m_ForceMultiUi is null");
//                }
//            }

//            if (GameManager.Singlton.GetPrefabUIScore(out GameObject o_ScorePrefab))
//            {
//                m_Score = (Instantiate(o_ScorePrefab, canvas.transform)).GetComponent<Text>();
//                if (m_Score is null)
//                {
//                    Debug.LogWarning("m_Score is null");
//                }
//            }
//        }

//        public void OnPlayerForceChange(float i_NewF)
//        {
//            m_ForceMultiUi.UpdateFilliStatus =i_NewF;
//        }
//    }
//}