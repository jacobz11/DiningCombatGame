namespace DiningCombat.Util.DesignPatterns
{
    public interface IStateMachine<T, TIndex> where T : IDCState
    {
        public T CurrentState { get; }

        TIndex Index { get; }

    }
}