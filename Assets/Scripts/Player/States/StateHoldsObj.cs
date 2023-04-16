namespace Assets.scrips.Player.States
{
    using DesignPatterns.Abstraction;
    using System;
    using UnityEngine;

    internal class StateHoldsObj : IStatePlayerHand
    {
        public const int k_Indx = 1;

        private bool isActiv;

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
        public void EnterCollisionFoodObj(Collider other)
        {/* */}

        public void ExitCollisionFoodObj(Collider other)
        {/* */}

        public void OnSteteEnter()
        {
            Debug.Log("init state : StateHoldsObj");
        }

        public void OnSteteExit()
        {
        }

        public void Update()
        {
        }

        public bool OnPickUpAction()
        {
            return false;
        }

        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {
            throw new NotImplementedException();
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
