using DesignPatterns.Abstraction;
// TODO : Add a namespace
internal interface IStateMachine<T, TIndex> where T : IDCState
{
    public T CurrentState { get; }

    TIndex Index { get; }

}