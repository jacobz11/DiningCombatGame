using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using UnityEngine;

namespace DiningCombat.Player.Manger
{
    internal class OfflinePlayerStateMachine : AcitonHandStateMachine
    {
        internal override void BuildState()
        {
            SetStates(new StateFreeOffline(Player, this), new StateHoldsObjOffline(Player, this),
                new StatePoweringOffline(Player, this), new StateThrowingOffline(Player, this));

        }

        internal class StateFreeOffline : StateFree
        {
            public StateFreeOffline(PlayerHand i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return this.HaveGameObject && this.IsPowerKeyPress;
            }
        }

        internal class StateHoldsObjOffline : StateHoldsObj
        {
            public StateHoldsObjOffline(PlayerHand i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return this.IsPowerKeyPress;
            }
        }

        internal class StatePoweringOffline : StatePowering
        {
            public StatePoweringOffline(PlayerHand i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                if (this.IsBufferTime)
                {
                    Debug.Log("IsBufferTime " + Input.GetKeyUp(KeyCode.E));
                    return Input.GetKeyUp(KeyCode.E);
                }

                return false;
            }
        }
        internal class StateThrowingOffline : StateThrowing
        {
            public StateThrowingOffline(PlayerHand i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }
        }
    }
}
