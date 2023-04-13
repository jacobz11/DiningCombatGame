using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using UnityEngine;

namespace DiningCombat.Player.Manger
{
    internal class OfflinePlayerStateMachine : BridgeImplementorAcitonStateMachine
    {
        internal override void BuildState()
        {
            SetStates(new StateFreeOffline(Player, this), new StateHoldsObjOffline(Player, this),
                new StatePoweringOffline(Player, this), new StateThrowingOffline(Player, this));

        }

        private new void Update()
        {
            base.Update();
        }

        internal class StateFreeOffline : StateFree
        {
            public StateFreeOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return Input.GetKey(KeyCode.E);
            }
        }

        internal class StateHoldsObjOffline : StateHoldsObj
        {
            public StateHoldsObjOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return Input.GetKey(KeyCode.E);
            }
        }

        internal class StatePoweringOffline : StatePowering
        {
            public StatePoweringOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return Input.GetKeyUp(KeyCode.E);
            }

            protected override bool IsAddPower()
            {
                return Input.GetKey(KeyCode.E);
            }
        }
        internal class StateThrowingOffline : StateThrowing
        {
            public StateThrowingOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }
        }
    }
}
