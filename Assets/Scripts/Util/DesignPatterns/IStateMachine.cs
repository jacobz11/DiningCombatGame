namespace DiningCombat.Util.DesignPatterns
{
    public interface IStateMachine<TState, TIndex> where TState : IDCState
    {
        public TState CurrentState { get; }

        TIndex Index { get; }
    }
}