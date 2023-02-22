using System;
using UnityEngine;

namespace DiningCombat
{
    /// <summary>
    /// this class is to manage buttons input 
    /// <para>this will be an Abstract-Factory
    /// that get from the System the Device Type and
    /// return the a <see cref="GameKeyboardControls"/>by input Device Type
    /// </para>
    /// </summary>
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
        private KeysHamdler[] m_Keys;

        // ================================================
        // ----------------Serialize Field-----------------

        // ================================================
        // properties
        internal bool IsVertical
        {
            get => m_Keys[k_Forwar].Press || m_Keys[k_Back].Press;
        }
        internal bool IsHorizontal
        {
            get => m_Keys[k_Left].Press || m_Keys[k_Right].Press;
        }

        // ----------------Indexer-------------------------
        internal KeysHamdler this[byte i]
        {
            get
            {
                return m_Keys[i];
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
            m_Keys = new KeysHamdler[]
            {
                KeysHamdler.Builder("Left"),
                KeysHamdler.Builder("Right"),
                KeysHamdler.Builder("Forward"),
                KeysHamdler.Builder("Back"),
                KeysHamdler.Builder("Power"),
                KeysHamdler.Builder("Jump"),
            };
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

