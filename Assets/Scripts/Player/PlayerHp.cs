using DiningCombat;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class PlayerHp : MonoBehaviour
    {
        // ================================================
        // constant Variable 

        // ================================================
        // Delegate
        public event EventHandler PlayerDeath;

        // ================================================
        // Fields 
        private float m_LifePoint;
        private float m_MaxSlderVal;
        private float m_MinSlderVal;
        private GameObject m_Player;
        // ================================================
        // ----------------Serialize Field-----------------
        [SerializeField]
        private FilliStatus m_Slder;

        // ================================================
        // properties
        private float HP
        {
            get => m_LifePoint;
            set
            {
                m_LifePoint = Math.Max(value, m_MinSlderVal);
                m_Slder.UpdateFilliStatus = m_LifePoint;
            }
        }
        // ================================================
        // auxiliary methods programmings

        // ================================================
        // Unity Game Engine

        private void Start()
        {
            m_LifePoint = m_Slder.GetSliderCurAndMaxAndMinValue(out m_MaxSlderVal,
                out m_MinSlderVal);
        }

        // ================================================
        //  methods
        internal void HitYou(float i_NumOfLifeLose)
        {
            //Debug.Log("HitYou");
            HP -= i_NumOfLifeLose;
            death();

        }
        internal void HealingYou(float i_NumOfLifeAdd)
        {
            HP += i_NumOfLifeAdd;
       }
        // ================================================
        // auxiliary methods

        // ================================================
        // Delegates Invoke 
        private void death()
        {
            if(HP <= m_MinSlderVal)
            {
                PlayerDeath?.Invoke(this, EventArgs.Empty);
                Destroy(m_Player);
            }
        }

        // ================================================
        // ----------------Unity--------------------------- 
        // ----------------GameFoodObj---------------------
    }
}
