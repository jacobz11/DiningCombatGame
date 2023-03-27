using Assets.Scrips_new.AI.Algo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.AI.Algo
{
    internal class SimpleAiAlgo : IAiAlgoAgent<Vector3, Vector3>
    {
        protected List<Vector3> m_Targets;

        public List<Vector3> GetData()
        {
            return m_Targets;
        }

        public Vector3 RunAlgo(Vector3 pos)
        {
            IEnumerable<Vector3> query = m_Targets.OrderBy(other => DistanceAI(other, pos));

            foreach (Vector3 target in query)
            {
                return target;
            }

            return Vector3.zero;
        }

        protected float DistanceAI(Vector3 v, Vector3 w)
        {
            return Vector3.Distance(v, w);
        }  
        public void SetData(List<Vector3> data)
        {
            m_Targets = data;
        }
    }
}
