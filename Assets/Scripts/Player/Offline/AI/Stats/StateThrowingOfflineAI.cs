using Assets.Scrips_new.AI.Algo;
using Assets.Scripts.AI;
using DesignPatterns.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Offline.AI.Stats
{
    internal class StateThrowingOfflineAI<IAiAlgoAgent> : AiDCState<IAiAlgoAgent<Vector3, Vector3>>
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
