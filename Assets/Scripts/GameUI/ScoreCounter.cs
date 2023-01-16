using UnityEngine;
using UnityEngine.UI;

namespace DiningCombat 
{
    public class ScoreCounter : MonoBehaviour
    {
        private const string k_FormtToShow = "Score: {0}";
        private static int s_ScoreValue = 0;
        private static Text s_Score;

        public static int ScoreValue
        {
            get => s_ScoreValue;
            set
            {
                s_ScoreValue = value;
                s_Score.text = string.Format(k_FormtToShow, s_ScoreValue);
            }
        }

        protected void Start()
        {
            s_Score = GetComponent<Text>();
            ScoreValue = 0;
        }
    }
}