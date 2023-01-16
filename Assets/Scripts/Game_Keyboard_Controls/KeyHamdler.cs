using UnityEngine;

namespace DiningCombat
{
    public struct KeyHamdler
    {
        // ================================================
        // constant Variable 

        // ================================================
        // Delegate

        // ================================================
        // Fields 
        private KeyCode m_Code;

        // ================================================
        // ----------------Serialize Field-----------------

        // ================================================
        // properties
        public bool Press
        {
            get => Input.GetKey(m_Code);
        }
        public bool Up
        {
            get => Input.GetKeyUp(m_Code);
        }
        public bool Down
        {
            get => Input.GetKeyUp(m_Code);
        }
        // ================================================
        // auxiliary methods programmings

        // ================================================
        // Unity Game Engine

        // ================================================
        //  methods
        public KeyHamdler(KeyCode i_Code)
        {
            m_Code = i_Code;
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
