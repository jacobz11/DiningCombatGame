using DesignPatterns.Abstraction;
using UnityEngine;

namespace DiningCombat.Player.Offline.State
{
    internal class StateHoldsObjOffline : DCState
    {
        //private const byte k_AddState = 1;
        private float initTimeEnteState;

        public StateHoldsObjOffline(byte stateId, string stateName)
            : base(stateId, stateName)
        {
        }

        public virtual void OnStateEnter(params object[] list)
        {
            this.initTimeEnteState = Time.time;
            Debug.Log("init state : StateHoldsObj");
        }

        public virtual void OnStateUpdate(params object[] list)
        {
            if (Input.GetKey(KeyCode.E))
            {
                ChangeStateHappened((byte)(r_StateId + 1));
            }
        }

        public override string ToString()
        {
            return "StateHoldsObj : " + this.name;
        }
    }
}
