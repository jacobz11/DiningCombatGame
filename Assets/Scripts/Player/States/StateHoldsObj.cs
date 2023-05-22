
using DiningCombat.FoodObject;
using DiningCombat.Util.DesignPatterns;
using System;
using UnityEngine;

namespace DiningCombat.Player.States
{
    public class StateHoldsObj : IStatePlayerHand
    {
        public const int k_Indx = 1;

        private bool isActiv;
        public bool OnPickUpAction() => false;

        bool IStatePlayerHand.OnChargingAction
        {
            get => false;
            set
            {
                OnStartCharging?.Invoke();
            }
        }

        public event Action OnStartCharging;

        public StateHoldsObj()
        {
            isActiv = false;
        }

        public void GameInput_OnChargingAction()
        {
            if (isActiv)
            {
                OnStartCharging?.Invoke();
            }
        }
        #region Not-Implemented
        public void EnterCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public void ExitCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public virtual void OnStateExit()
        {/* Not-Implemented */}
        public virtual void Update()
        {/* Not-Implemented */}
        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {/* Not-Implemented */}
        #endregion

        public virtual void OnStateEnter()
        {
            Debug.Log("init state : StateHoldsObj");
        }
        public bool OnPickUpAction(out GameFoodObj o_Collcted)
        {
            o_Collcted = null;
            return false;
        }

        public bool OnThrowPoint(out float o_Force)
        {
            o_Force = 0;
            return false;
        }
    }
}
