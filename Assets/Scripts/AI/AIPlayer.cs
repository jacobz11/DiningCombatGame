using Abstraction.DesignPatterns;
using System.Collections.Generic;

namespace Assets.Scrips_new.AI.Algo
{
    internal class AIPlayer<IAiAlgoAgent> : MonoBehaviourStateMachine
    {
        List<IAiAlgoAgent> agents;
    }
}
