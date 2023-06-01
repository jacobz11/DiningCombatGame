namespace DiningCombat.Util.DesignPatterns
{
    public interface IDCState
    {
        void OnStateExit();

        void OnStateEnter();
    }
}