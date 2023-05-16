using DesignPatterns.Abstraction;
// TODO : Move it to the interfaces folder
// TODO : Add a namespace

internal interface IFoodState : IDCState
{
    string TagState { get; }

    bool IsThrowingAction();

    bool TryCollect(ActionStateMachine i_Collcter);
    void Update();
}