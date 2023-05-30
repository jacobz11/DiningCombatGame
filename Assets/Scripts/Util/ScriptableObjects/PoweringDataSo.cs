using System;
using UnityEngine;

namespace DiningCombat.DataObject
{
    [Serializable]
    [CreateAssetMenu(fileName = "PoweringDataSo", menuName = "Custom Objects/Powering Data")]
    public class PoweringDataSo : ScriptableObject
    {
        [Range(100f, 5000f)]
        [Tooltip("the max Power  thet the player can charge")]
        public float m_MaxPower = 3000;
        [Range(10f, 1000f)]
        [Tooltip("The minimum force to make a throw")]
        public float m_MinPower = 50.0f;
        [Range(10f, 2000f)]
        [Tooltip("The increment in each update, it will be multiplied by delta time")]
        public float m_DataTimeAddingPower = 1400;
        [Range(10f, 2000f)]
        [Tooltip("The maximum time the AI can charge")]
        public float m_MaxPoweringTime = 10.0f;
        [Range(0f, 10f)]
        [Tooltip("AI : The Update Rate Find Closesr Player")]
        public float m_UpdateRate = 1.5f;
        [Range(0f, 100f)]
        [Tooltip("AI : Min Distance To Target")]
        public float m_MinDistanceToTarget = 7f;
        public float NormalizingPower(float power)
        {
            return power / m_MaxPower;
        }
    }
}
