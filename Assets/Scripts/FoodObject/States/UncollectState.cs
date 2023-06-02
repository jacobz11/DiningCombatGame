using DiningCombat.Player;
using System;
using UnityEngine;
namespace DiningCombat.FoodObject
{
    public class UncollectState : IFoodState
    {
        public const int k_Indx = 0;
        public event Action<ActionStateMachine> Collect;

        public string TagState => GameGlobal.TagNames.k_FoodObj;

        public GameFoodObj GameFood { get; }

        public bool IsThrowingAction() => false;

        public UncollectState(GameFoodObj i_GameFood)
        {
            GameFood = i_GameFood;
        }

        #region Not Implemented
        public virtual void OnStateEnter() {/* Not Implemented */}

        public virtual void OnStateExit() {/* Not Implemented */}

        public virtual void Update() {/* Not Implemented */}
        #endregion

        public virtual bool TryCollect(ActionStateMachine i_Collcter)
        {
            Collect?.Invoke(i_Collcter);

            return true;
        }

        public void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
            => Debug.LogWarning("trying to set Throw Direction in CollectState");
    }
}