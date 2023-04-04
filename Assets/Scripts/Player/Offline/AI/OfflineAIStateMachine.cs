using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using UnityEngine;

namespace DiningCombat.Player.Manger
{
    internal class OfflineAIStateMachine : BridgeImplementorAcitonStateMachine
    {
        public override void Update()
        {
            base.Update();
        }
        internal override void BuildState()
        {
            SetStates(new FreeHandOfflineAI(Player, this), new StateHoldsObjOfflineAI(Player, this),
                new StatePoweringOfflineAI(Player, this), new StateThrowingOfflineAI(Player, this));
        }

        internal class FreeHandOfflineAI : StateFree
        {
            public FreeHandOfflineAI(BridgeAbstractionAction i_PickUpItem, OfflineAIStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return HaveGameObject;
            }

            
        }

        internal class StateHoldsObjOfflineAI : StateHoldsObj
        {

            public StateHoldsObjOfflineAI(BridgeAbstractionAction i_PickUpItem, OfflineAIStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return false;
            }
        }

        internal class StatePoweringOfflineAI : StatePowering
        {

            public StatePoweringOfflineAI(BridgeAbstractionAction i_PickUpItem, OfflineAIStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return false;
            }
        }

        internal class StateThrowingOfflineAI : StateThrowing
        {
            public StateThrowingOfflineAI(BridgeAbstractionAction i_PickUpItem, OfflineAIStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }
        }
    }
}













//FreeHandOfflineAI freeState = new FreeHandOfflineAI(Player, this);
//StateHoldsObjOfflineAI stateHolding = new StateHoldsObjOfflineAI(Player, this);
//StatePoweringOfflineAI poweringState = new StatePoweringOfflineAI(Player, this);
//StateThrowingOfflineAI stateThrowing = new StateThrowingOfflineAI(Player, this);


//private PlayerHand m_PlayersHand;
//private GameObject m_FoodObj;
//public PlayerHand Player
//{
//    get { return m_PlayersHand; }
//    set
//    {
//        if (m_PlayersHand == null)
//        {
//            m_PlayersHand = value;
//        }
//        else
//        {
//            Debug.LogError("Can't reboot more than once");
//        }
//    }
//}

//public virtual void SetPlayerHand(PlayerHand i_Player)
//{
//    Debug.Log("SetPlayerHand ");
//    Player = i_Player;
//}

//public virtual void Update()
//{
//    //CurrentStates.OnStateUpdate();
//}

//protected virtual void OnPlayerSetFoodObj(GameObject i_ColctedFoodObj)
//{
//    if (i_ColctedFoodObj == null)
//    {

//    }
//    else if (i_ColctedFoodObj.GetComponent<GameFoodObj>() != null)
//    {
//        m_FoodObj = i_ColctedFoodObj;
//    }
//    else
//    {
//        Debug.LogError("Colcted Food Obj is null not FoodObj type");
//    }
//}

//internal void BuildOfflineAIState()
//{
//    if (m_PlayersHand != null)
//    {
//        //Debug.Log(" BuildOfflineAIState");
//        //List<IDCState> states = new List<IDCState>();
//        //FreeHandOfflineAI free = m_PlayersHand.AddComponent<FreeHandOfflineAI>();
//        //states.Add(free);
//        //free.PlayerCollectedFood += OnPlayerSetFoodObj;

//        //Debug.Log(" BuildOfflineAIState");
//        //StateHoldsObjOffline holdsObj = m_PlayersHand.AddComponent<StateHoldsObjOffline>();
//        //holdsObj.StateId = 1;
//        //states.Add(holdsObj);

//        //StatePoweringOffline powering = m_PlayersHand.AddComponent<StatePoweringOffline>();
//        //powering.StateId = 2;
//        //states.Add(powering);

//        //StateThrowingOffline throwing = m_PlayersHand.AddComponent<StateThrowingOffline>();
//        //throwing.StateId = 3;
//        //states.Add(throwing);

//        //SetStates(states);
//    }
//    else
//    {
//        Debug.LogError("Missing component PlayersHand - It is impossible to create StateMachineImplemntor without a PlayersHand");
//    }
//}