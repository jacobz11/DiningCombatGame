using UnityEngine;
using UnityEngine.UI;

namespace DiningCombat
{
    internal class LifeCounter : MonoBehaviour
    {
        private const string k_FormtToShow = "Life: {0}";
        private static float s_LifeValue = 0;
        private static Text s_Power;

        public static float HP
        {
            get => s_LifeValue;
            set
            {
                s_LifeValue = value;
                s_Power.text = string.Format(k_FormtToShow, s_LifeValue);
            }
        }

        protected void Start()
        {
            s_Power = GetComponent<Text>();
            s_LifeValue = 0;
        }
    }
}
