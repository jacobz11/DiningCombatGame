using Assets.Scripts.Player.Offline.Player.States;
using DiningCombat.Player.Manger;
using DiningCombat.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
{
    internal abstract class StatePowering : IStatePlayerHand
    {
        public event Action<float> OnPower;
        private float initTimeEnteState;

        protected bool IsBufferTime => Time.time - this.initTimeEnteState > 0.2f;

        public StatePowering(PlayerHand i_PickUpItem, AcitonHandStateMachine i_Machine)
            : base(i_PickUpItem, i_Machine)
        {
        }

        protected abstract bool IsPassStage();


        public override void OnStateEnter(params object[] list)
        {
            Debug.Log("init state : StatePowering");
            this.initTimeEnteState = Time.time;
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
                this.m_Machine.StatesIndex++;
            }
            else
            {
                this.StepBack();
            }
        }

        private void StepBack()
        {
            if (this.IsBufferTime)
            {
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
            OnPower?.Invoke(Time.deltaTime * 1400);
        }
    }
}
