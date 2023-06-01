using UnityEngine;

namespace DiningCombat.Environment
{
    [CreateAssetMenu(fileName = "Room", menuName = "Custom Objects/Room")]
    public class Room : ScriptableObject
    {
        public Vector3 m_Center;
        public Vector3 m_Dimensions;
        public float m_YMinOffset;

        public float Width => m_Dimensions.z;
        public float Higet => m_Dimensions.x;
        public Vector3 GetRendonPos()
        {
            Vector3 offset = new Vector3()
            {
                x = Random.Range(-m_Dimensions.x, m_Dimensions.x),
                y = Random.Range(m_YMinOffset, m_Dimensions.y),
                z = Random.Range(-m_Dimensions.z, m_Dimensions.z)
            };
            return m_Center + offset;
        }
    }
}
