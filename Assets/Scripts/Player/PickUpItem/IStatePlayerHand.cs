
using UnityEngine;
using static UnityEditor.ShaderData;

namespace Assets.Scripts.PickUpItem
{
    public interface IStatePlayerHand
    {
        /// <summary>
        /// This method should be implemented
        /// In any State, what will we do?
        /// </summary>
        public void UpdateByState();
        
        /// <summary>
        /// This method should implement the
        /// initialization of the state
        /// </summary>
        public void InitState();
        public bool IsPassStage();
        
        /// <summary>
        /// for now the implementing only in StateFree
        /// to notify StateFree the Collision Food Obj happened
        /// In order to seve the GameObject and 
        /// not automatically pick it up 
        /// </summary>
        /// <param name="i_GameObject"></param>
        void EnterCollisionFoodObj(GameObject i_GameObject);

        /// <summary>
        /// for now the implementing only in StateFree
        /// to notify StateFree the Exit from the Collision Food Obj happened
        /// In order to seve the GameObject and 
        /// not automatically pick it up 
        /// </summary>
        void ExitCollisionFoodObj();
    }
}
