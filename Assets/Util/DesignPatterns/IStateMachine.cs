using DesignPatterns.Abstraction;

internal interface IStateMachine<T, TIndex> where T : IDCState
{
    public T CurrentStatu { get; }

    TIndex Index { get; }

}