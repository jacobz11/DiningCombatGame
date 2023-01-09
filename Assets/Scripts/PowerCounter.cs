using UnityEngine;
using UnityEngine.UI;

namespace DiningCombat
{
    public class PowerCounter : MonoBehaviour
    {
        private const string k_FormtToShow = "Power: {0}";
        private static float s_PowerValue = 0;
        private static Text s_Power;

        public static float PowerValue
        {
            get
            {
                return s_PowerValue;
            }

            set
            {
                s_PowerValue = value;
                s_Power.text = string.Format(k_FormtToShow, s_PowerValue);
            }
        }

        protected void Start()
        {
            s_Power = GetComponent<Text>();
            PowerValue = 0;
        }
    }
}