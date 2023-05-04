using DesignPatterns.Abstraction;

internal interface IFoodState : IDCState
{
    string TagState { get; }

    bool IsThrowingAction();

    bool TryCollect(AcitonStateMachine i_Collcter);
    void Update();
}