using DiningCombat.Player;
using UnityEngine;

namespace DiningCombat.FoodObject
{

    public class CollectState : IFoodState
    {
        public const int k_Indx = 1;

        private readonly Rigidbody r_Rigidbody;
        private readonly Transform r_Transform;
        private readonly GameFoodObj r_GameFoodObj;

        public string TagState => GameGlobal.TagNames.k_Picked;

        public bool IsThrowingAction() => true;
        public bool TryCollect(ActionStateMachine i_Collcter) => false;

        public CollectState()
        {
            // Not implemented
        }

        public CollectState(Rigidbody rigidbody, Transform transform, GameFoodObj gameFoodObj)
        {
            r_Rigidbody = rigidbody;
            r_Transform = transform;
            r_GameFoodObj = gameFoodObj;
        }

        public void OnStateExit()
        {
            // Not implemented
        }
        public void OnStateEnter()
        {
            Ragdoll.DisableRagdoll(r_Rigidbody);
            r_GameFoodObj.gameObject.transform.rotation = Quaternion.identity;
        }

        public void Update()
        {
            r_Transform.position = r_GameFoodObj.GetCollectorPosition();
        }

        public void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
        {
            Debug.LogWarning("trying to set Throw Direction in CollectState");
        }
    }
}