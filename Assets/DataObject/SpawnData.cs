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
    internal class SpawnData
    {
        public float m_SpawnTimeBuffer;
        public short m_InitSpawn;
        public Vector3 m_MaxPos;
        public Vector3 m_MinPos;

        public Vector3 GetRendonPos()
        {
            return new Vector3(
                Random.Range(m_MinPos.x, m_MaxPos.x),
                Random.Range(m_MinPos.y, m_MaxPos.y),
                Random.Range(m_MinPos.z, m_MaxPos.z)
                );
        }
    }
}
