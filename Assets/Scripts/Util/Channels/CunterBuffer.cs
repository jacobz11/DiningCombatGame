using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Util.Channels
{

    internal class CunterBuffer : IBuffer<int>
    {
        private int m_CurrentInx;
        private int m_MaxIdx;
        public CunterBuffer(int i_MaxIdx)
          : this(i_MaxIdx, 0)
        {
        }
        public int DefaultLimt => DefaultMaxLimt;

        public static int DefaultMaxLimt => 100;

        public CunterBuffer(int i_MaxIdx, int i_CurrentInx)
        {
            if (i_MaxIdx < 0)
            {
                Debug.LogError("the Update Rate mast be non nagtive");
                i_MaxIdx = DefaultMaxLimt;
            }
            m_MaxIdx = i_MaxIdx;
            m_CurrentInx = i_CurrentInx;
        }

      
        public void AddToData(int i_Dalta)
        {
            m_CurrentInx += i_Dalta;
        }

        public void Clear()
        {
            m_CurrentInx = 0;
        }

        public bool IsBufferOver()
        {
            return m_CurrentInx >= m_MaxIdx;
        }

        public void SetDataToInit()
        {
            Clear();
        }

        public void UpdeteData(int i_NewLest)
        {
            m_CurrentInx = i_NewLest;
        }
    }
}
