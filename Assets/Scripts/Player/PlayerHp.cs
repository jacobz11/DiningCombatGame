using DiningCombat;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class PlayerHp : MonoBehaviour
    {
        // ================================================
        // constant Variable 
        private const float k_MaxPlayerLife = GameGlobal.PlayerData
            .k_MaxPlayerLife;
        private const float k_Zero = 0;

        // ================================================
        // Delegate
        public event EventHandler PlayerDeath;

        // ================================================
        // Fields 
        private float m_LifePoint;
        private GameObject m_Player;
        // ================================================
        // ----------------Serialize Field-----------------

        // ================================================
        // properties
        private float HP
        {
            get => m_LifePoint;
            set
            {
                m_LifePoint = Math.Min(k_MaxPlayerLife, value);
                LifeCounter.HP = value;
            }
        }
        // ================================================
        // auxiliary methods programmings

        // ================================================
        // Unity Game Engine
        void Start()
        {
            HP = k_MaxPlayerLife;
        }

        // ================================================
        //  methods
        internal void HitYou(float i_NumOfLifeLose)
        {
            HP -= i_NumOfLifeLose;

            if (HP <= k_Zero) 
            {
                death();
            }
        }
        internal void HealingYou(float i_NumOfLifeAdd)
        {
            HP -= i_NumOfLifeAdd;
        }
        // ================================================
        // auxiliary methods

        // ================================================
        // Delegates Invoke 
        private void death()
        {
            PlayerDeath?.Invoke(this, EventArgs.Empty);
            Destroy(m_Player);
        }

        // ================================================
        // ----------------Unity--------------------------- 
        // ----------------GameFoodObj---------------------
    }
}
