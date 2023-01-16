using UnityEngine;

namespace DiningCombat
{
    internal struct KeysHamdler
    {
        // ================================================
        // constant Variable 

        // ================================================
        // Delegate

        // ================================================
        // Fields 
        private readonly KeyHamdler[] r_Keys;

        // ================================================
        // ----------------Serialize Field-----------------

        // ================================================
        // properties
        /// <summary>
        /// for each what of the key is Press
        /// </summary>
        public bool Press
        {
            get
            {
                bool isPress = false;

                foreach (KeyHamdler key in r_Keys)
                {
                    if (key.Press)
                    {
                        isPress = true;
                        break;
                    }
                }

                return isPress;
            }
        }
        /// <summary>
        /// for each what of the key is Up
        /// </summary>
        public bool Up
        {
            get
            {
                bool isUp = false;

                foreach (KeyHamdler key in r_Keys)
                {
                    if (key.Up)
                    {
                        isUp = true;
                        break;
                    }
                }

                return isUp;
            }
        }
        /// <summary>
        /// for each what of the key is Down
        /// </summary>
        public bool Down
        {
            get
            {
                bool isDown = false;

                foreach (KeyHamdler key in r_Keys)
                {
                    if (key.Up)
                    {
                        isDown = true;
                        break;
                    }
                }

                return isDown;
            }
        }
        // ================================================
        // auxiliary methods programmings

        // ================================================
        // Unity Game Engine

        // ================================================
        //  methods
        public KeysHamdler(params KeyCode[] keys)
        {
            r_Keys = new KeyHamdler[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                r_Keys[i] = new KeyHamdler(keys[i]);
            }
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
