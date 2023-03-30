using Assets.Scripts.Player.Offline.Player.States;
using DiningCombat.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
{
    internal abstract class StatePowering : IStatePlayerHand
    {
        public event Action<float> OnPower;

        public StatePowering(PlayerHand i_PickUpItem, AcitonHandStateMachine i_Machine)
            : base(i_PickUpItem, i_Machine)
        {
        }

        protected abstract bool IsPassStage();

        public override void OnStateEnter(params object[] list)
        {
            Debug.Log("init state : StatePowering");
            base.OnStateEnter(list);
        }

        public override void OnStateExit(params object[] list)
        {
        }


        public override void OnStateUpdate(params object[] list)
        {
            if (this.IsPowerKeyPress)
            {
                this.AddToForceMulti();
            }
            else if (this.IsPassStage())
            {
                Debug.Log("StatePowering : IsPassStage ");
                this.m_Machine.StatesIndex++;
            }
            else
            {
                this.StepBack();
            }
        }

        public IEnumerator OnTringPoint()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            OnPower?.Invoke(float.NegativeInfinity);
        }

        private void StepBack()
        {
            if (this.IsBufferTime)
            {
                OnPower?.Invoke(float.NegativeInfinity);
                this.m_Machine.StatesIndex--;
            }
        }
        public override string ToString()
        {
            return "StatePowering : ";
        }

        

        ///// <inheritdoc/>
        //public bool IsPassStage()
        //{
        //    if (this.IsBufferTime)
        //    {
        //        return Input.GetKeyUp(KeyCode.E) && this.m_PlayrHand.ForceMulti > 50;
        //    }

        //    return false;
        //}

        private void AddToForceMulti()
        {
            Debug.Log("AddToForceMulti");
            OnPower?.Invoke(Time.deltaTime * 1400);
        }
    }
}
