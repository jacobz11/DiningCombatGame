using UnityEngine;

namespace DiningCombat
{
    internal struct KeysHamdler
    {
        private readonly KeyHamdler[] r_Keys;
        public KeysHamdler(params KeyCode[] keys)
        {
            r_Keys = new KeyHamdler[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                r_Keys[i] = new KeyHamdler(keys[i]);
            }
        }
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
        
    }
}
