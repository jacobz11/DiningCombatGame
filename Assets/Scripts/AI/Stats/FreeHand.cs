using Abstraction.DesignPatterns;

namespace Assets.Scrips_new.AI.Stats
{
    internal class FreeHand<IAiAlgoAgent> : DCState
    {
        private readonly IAiAlgoAgent agent;
        public FreeHand(byte stateId, string stateName, IAiAlgoAgent agent) : base(stateId, stateName)
        {
            this.agent = agent;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
