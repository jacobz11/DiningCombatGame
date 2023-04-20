using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.DataObject
{
    [Serializable]
    internal class RoomDimension
    {
        public Vector3 m_Center;
        public Vector3 m_Dimensions;
        public float m_YMinOffset;
        public RoomDimension() { }
        public float Width => m_Dimensions.z;
        public float Higet => m_Dimensions.x;
        public Vector3 GetRendonPos()
        {
            Vector3 offset = new Vector3(
                Random.Range(-m_Dimensions.x, m_Dimensions.x),
                Random.Range(m_YMinOffset, m_Dimensions.y),
                Random.Range(-m_Dimensions.z, m_Dimensions.z)
                );
            return m_Center + offset;
        }


    }
}
