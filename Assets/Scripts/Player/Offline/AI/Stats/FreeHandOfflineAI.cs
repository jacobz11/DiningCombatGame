using DesignPatterns.Abstraction;

namespace Assets.Scrips_new.AI.Stats
{
    internal class FreeHandOfflineAI<IAiAlgoAgent> : DCState
    {
        private readonly IAiAlgoAgent agent;
        public FreeHandOfflineAI(byte stateId, string stateName, IAiAlgoAgent agent) : base(stateId, stateName)
        {
            this.agent = agent;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
