
using Assets.Scripts.AI.Algo;
using DesignPatterns.Abstraction;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

namespace Assets.Scripts.AI
{
    //internal abstract class AiDCState<IAiAlgoAgent> : IDCState
    //{
    //    protected SimpleAiAlgo m_Agent;
    //    protected Vector3 m_CurrentTarget;

    //    private void Awake()
    //    {
    //        m_Agent = new SimpleAiAlgo();
    //    }

    //    public virtual Vector3 Target
    //    {
    //        get { return m_CurrentTarget; }
    //        protected set { m_CurrentTarget = value; }
    //    }
    //    public void SetTargets(List<Vector3> i_UpdatedTargetList)
    //    {
    //        m_Agent.SetData(i_UpdatedTargetList);
    //    }

    //    public virtual void RunAlgo()
    //    {
    //        Target = m_Agent.RunAlgo(this.transform.position);
    //    }
    //}
}
