using UnityEngine;

namespace DiningCombat
{
    internal struct KeyHamdler
    {
        private KeyCode m_Code;

        public KeyHamdler(KeyCode i_Code)
        {
            m_Code = i_Code;
        }

        public bool Press
        {
            get
            {
                return Input.GetKey(m_Code);
            }
        }
        public bool Up
        {
            get
            {
                return Input.GetKeyUp(m_Code);
            }
        }
        public bool Down
        {
            get
            {
                return Input.GetKeyUp(m_Code);
            }
        }
    }
}
