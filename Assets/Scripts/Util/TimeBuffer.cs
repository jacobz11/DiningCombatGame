using System;
using UnityEngine;

namespace Assets.Scripts.Util.Channels
{
    [Serializable]
    internal struct TimeBuffer : IBuffer<float>
    {
        public static float Default => 5.0f;

        private float m_LestUpdate;
        [SerializeField]
        private float m_UpdateRate;

        public float DefaultLimt => Default;
        public void Clear() => SetDataToInit();
        public void SetDataToInit() => m_LestUpdate = Time.time;
        public bool IsBufferOver() => Time.time - m_LestUpdate > m_UpdateRate;
        public void UpdateByDaltaTime() => AddToData(Time.deltaTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i_UpdateRate">the Buffer time to add</param>
        public TimeBuffer(float i_UpdateRate)
            : this(i_UpdateRate, float.MinValue)
        { /* Not Implemented */}

        public TimeBuffer(float i_UpdateRate, float i_LestUpdate)
        {
            if (i_UpdateRate < 0)
            {
                Debug.LogError("the Update Rate mast be non nagtive");
                i_UpdateRate = Default;
            }
            m_UpdateRate = i_UpdateRate;
            m_LestUpdate = i_LestUpdate;
        }

        public void AddToData(float i_DaltaTime)
        {
            m_LestUpdate += i_DaltaTime;
        }

        public void UpdeteData(float i_NewLest)
        {
            m_LestUpdate = i_NewLest;
        }

        internal void PreventDoubleEntry(float i_ActineTime)
        {
            UpdeteData(Time.time + (m_UpdateRate + i_ActineTime) * 10);
        }
    }
}
