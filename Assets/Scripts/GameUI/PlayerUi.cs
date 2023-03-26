using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    namespace UI
    {
        internal class PlayerUi : Behaviour
        {
            public event EventHandler PlayerDeath;
            private float m_LifePoint;
            private float m_MaxSlderVal;
            private float m_MinSlderVal;
            private int m_ScoreValue = 0;
            private int m_Kills = 0;
            private string m_PlayerFormtToShow;
            private GameObject m_Player;
            [SerializeField]
            private Text m_Score;
            [SerializeField]
            private FilliStatus m_HpSlder;
            [SerializeField]
            private FilliStatus m_ForceMultiUi;

            private float HP
            {
                get => m_LifePoint;
                set
                {
                    m_LifePoint = Math.Max(Math.Min(value, m_MaxSlderVal), m_MinSlderVal);
                    m_HpSlder.UpdateFilliStatus = m_LifePoint;
                }
            }
            public int ScoreValue
            {
                get => m_ScoreValue;
                set
                {
                    m_ScoreValue = value;
                    m_Score.text = string.Format(m_PlayerFormtToShow, m_ScoreValue);
                }
            }

            private void Start()
            {
                if (m_HpSlder == null)
                {
                    Debug.LogError("no find the Hp - Slder ");
                }
                else
                {
                    m_LifePoint = m_HpSlder.GetSliderCurAndMaxAndMinValue(out m_MaxSlderVal,
                        out m_MinSlderVal);
                }


                if (m_Score == null)
                {
                    Debug.LogError("no find the Text elment ");
                }

                if (m_ForceMultiUi == null)
                {
                    Debug.LogError("no find the Text Force-Multi- Ui ");
                }


                m_PlayerFormtToShow = gameObject.name + ": {0}";
                ScoreValue = 0;
            }

            internal bool HitYou(float i_NumOfLifeLose)
            {
                HP -= i_NumOfLifeLose;
                return death();
            }

            internal void HealingYou(float i_NumOfLifeAdd)
            {
                HP += i_NumOfLifeAdd;
            }

            private bool death()
            {
                bool isKilled = false;

                if (HP <= m_MinSlderVal)
                {
                    PlayerDeath?.Invoke(this, EventArgs.Empty);
                    Destroy(m_Player, 0.1f);
                    isKilled = true;
                }

                return isKilled;
            }

            public void OnHitPlayer(object sender, EventArgs args)
            {
                EventHitPlayer hit = args as EventHitPlayer;

                if (hit != null)
                {
                    this.m_Kills += hit.Kill;
                    this.ScoreValue += hit.Score;
                }
            }
        }
    }
}

