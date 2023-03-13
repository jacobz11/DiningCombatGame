namespace Assets.Scripts.Player.StatePlayerRightHand
{
    using System;
#pragma warning disable SA1600 // ElementsMustBeDocumented
    using UnityEngine;

    /// <summary>
    /// this is an abstract
    /// </summary>
    public abstract class IStatePlayerHand
    {
        protected PlayerRightHand playrHand;
        private float chargingPower;

        protected IStatePlayerHand(PlayerRightHand i_PickUpItem)
        {
            this.playrHand = i_PickUpItem;
        }

        public float ForceMulti
        {
            get => this.chargingPower;
            set
            {
                this.chargingPower = Math.Max(Math.Min(value, playrHand.maxCargingPower), 0);
            }
        }

        public bool IsPowerKeyPress => Input.GetKey(KeyCode.E);

        public abstract void UpdateByState();

        public abstract void InitState();

        public abstract bool IsPassStage();

        public virtual void EnterCollisionFoodObj(Collider other)
        {
        }

        public virtual void ExitCollisionFoodObj(Collider other)
        {
        }

        internal virtual void OnThrowingAnimator()
        {
        }
    }
#pragma warning restore SA1600 // ElementsMustBeDocumented
}
