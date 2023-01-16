using System;
using UnityEngine;

namespace DiningCombat
{
    internal class GameKeyboardControls
    {
        // ================================================
        // constant Variable 
        public const byte k_Left = 0, k_Right = 1,
            k_Forwar = 2, k_Back = 3, k_Power = 4,
            k_Jump = 5;

        // ----------------Game-Key-Code-------------------
        public const KeyCode k_PowerKey = KeyCode.E;
        public const KeyCode k_JumpKey = KeyCode.Space;

        // Forwar
        public const KeyCode k_ForwardKey = KeyCode.W;
        public const KeyCode k_ForwardKeyArrow = KeyCode.UpArrow;

        // Back 
        public const KeyCode k_BackKey = KeyCode.S;
        public const KeyCode k_BackKeyArrow = KeyCode.DownArrow;

        // Left
        public const KeyCode k_LeftKey = KeyCode.A;
        public const KeyCode k_LeftKeyArrow = KeyCode.LeftArrow;

        // Right
        public const KeyCode k_RightKey = KeyCode.D;
        public const KeyCode k_RightKeyArrow = KeyCode.RightArrow;

        // ================================================
        // Delegate

        // ================================================
        // Fields 
        private KeysHamdler m_Left;
        private KeysHamdler m_Right;
        private KeysHamdler m_Up;
        private KeysHamdler m_Down;
        private KeysHamdler m_Jump;
        private KeysHamdler m_Power;

        // ================================================
        // ----------------Serialize Field-----------------

        // ================================================
        // properties
        internal bool IsVertical
        {
            get => m_Up.Press || m_Down.Press;
        }
        internal bool IsHorizontal
        {
            get => m_Left.Press || m_Right.Press;
        }

        // ----------------Indexer-------------------------
        internal KeysHamdler this[byte i]
        {
            get
            {
                switch (i)
                {
                    case k_Left:
                        return m_Left;
                    case k_Right:
                        return m_Right;
                    case k_Forwar:
                        return m_Up;
                    case k_Back:
                        return m_Down;
                    case k_Power:
                        return m_Jump;
                    case k_Jump:
                        return m_Power;
                }
                throw new Exception();
            }
        }
        // ================================================
        // auxiliary methods programmings

        // ================================================
        // Unity Game Engine

        // ================================================
        //  methods
        public GameKeyboardControls()
        {
            m_Left = new KeysHamdler(k_LeftKey, k_LeftKeyArrow);
            m_Right = new KeysHamdler(k_RightKey, k_RightKeyArrow);
            m_Up = new KeysHamdler(k_ForwardKey, k_ForwardKeyArrow);
            m_Down = new KeysHamdler(k_BackKey, k_BackKeyArrow);
            m_Jump = new KeysHamdler(k_JumpKey);
            m_Power = new KeysHamdler(k_JumpKey);
        }
        // ================================================
        // auxiliary methods

        // ================================================
        // Delegates Invoke 

        // ================================================
        // ----------------Unity--------------------------- 
        // ----------------GameFoodObj---------------------
    }
}

