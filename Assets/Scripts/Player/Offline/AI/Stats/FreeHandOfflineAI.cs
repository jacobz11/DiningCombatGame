using Assets.Scrips_new.AI.Algo;
using Assets.Scripts.AI;
using Assets.Scripts.AI.Algo;
using DesignPatterns.Abstraction;
using System;
using UnityEngine;

namespace Assets.Scrips_new.AI.Stats
{
    internal class FreeHandOfflineAI : AiDCState <IAiAlgoAgent<Vector3, Vector3>>
    {
        public event Action<GameObject> PlayerCollectedFood;

        public virtual void OnStateEnter(params object[] list)
        {
        }

        public virtual void OnStateUpdate(params object[] list)
        {
            Debug.Log("in OnStateUpdate");
        }

        public virtual void OnStateExit(params object[] list)
        {
        }

        public virtual void OnStateMove(params object[] list)
        {
        }

        public virtual void OnStateIK(params object[] list)
        {
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
