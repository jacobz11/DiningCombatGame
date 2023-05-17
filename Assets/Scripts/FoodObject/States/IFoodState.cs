using DiningCombat.Player;
using DiningCombat.Util.DesignPatterns;
// TODO : Move it to the interfaces folder
namespace DiningCombat.FoodObject
{
    public interface IFoodState : IDCState
    {
        string TagState { get; }

        bool IsThrowingAction();

        bool TryCollect(ActionStateMachine i_Collcter);
        void Update();
    }
}