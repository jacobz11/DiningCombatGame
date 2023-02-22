using System;
using UnityEngine;

namespace DiningCombat
{
    /// <summary>
    /// this struct is 
    /// </summary>
    internal struct KeysHamdler
    {
        // ================================================
        // constant Variable 
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

        internal static KeysHamdler Builder(string i_KeyName)
        {
            if (!configFormStr(i_KeyName, out KeyCode[] o_Arr))
            {
                throw new NotImplementedException();
            }

            return new KeysHamdler(o_Arr);
        }

        private static bool configFormStr(string i_KeyName, out KeyCode[] o_Arr)
        {
            bool isValidStr = true;

            switch (i_KeyName.ToLower())
            {
                case "left":
                    o_Arr = new KeyCode[] { k_LeftKey, k_LeftKeyArrow };
                    break;
                case "right":
                    o_Arr = new KeyCode[] { k_RightKey, k_RightKeyArrow};
                    break;
                case "forward":
                    o_Arr = new KeyCode[] { k_ForwardKey, k_ForwardKeyArrow };
                    break;
                case "back":
                    o_Arr = new KeyCode[] { k_ForwardKey, k_ForwardKeyArrow };
                    break;
                case "power":
                    o_Arr = new KeyCode[] { k_PowerKey };
                    break;                
                case "jump":
                    o_Arr = new KeyCode[] { k_JumpKey };
                    break;
                default:
                    isValidStr = false;
                    o_Arr = new KeyCode[0];
                    break;
            }

            return isValidStr;
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
