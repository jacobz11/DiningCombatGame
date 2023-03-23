using DiningCombat;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class PlayerHp : MonoBehaviour
    {
        public event EventHandler PlayerDeath;
        private float m_LifePoint;
        private float m_MaxSlderVal;
        private float m_MinSlderVal;
        private GameObject m_Player;
        [SerializeField]
        private FilliStatus m_Slder;
        [SerializeField]
        private GameGlobal.ePlayerModeType m_Type;
        private float HP
        {
            get => m_LifePoint;
            set
            {
                m_LifePoint = Math.Max(Math.Min(value, m_MaxSlderVal), m_MinSlderVal);
                m_Slder.UpdateFilliStatus = m_LifePoint;
            }
        }

        private void Start()
        {
            m_LifePoint = m_Slder.GetSliderCurAndMaxAndMinValue(out m_MaxSlderVal,
                out m_MinSlderVal);
        }

        internal bool HitYou(float i_NumOfLifeLose)
        {
            //Debug.Log("HitYou");
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

            if(HP <= m_MinSlderVal)
            {
                PlayerDeath?.Invoke(this, EventArgs.Empty);
                Destroy(m_Player, 0.1f);
                isKilled = true;
            }

            return isKilled;
        }
    }
}
