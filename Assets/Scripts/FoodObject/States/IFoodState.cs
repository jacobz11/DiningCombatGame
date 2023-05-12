using DesignPatterns.Abstraction;

internal interface IFoodState : IDCState
{
    string TagState { get; }

    bool IsThrowingAction();

    bool TryCollect(ActionStateMachine i_Collcter);
    void Update();
}