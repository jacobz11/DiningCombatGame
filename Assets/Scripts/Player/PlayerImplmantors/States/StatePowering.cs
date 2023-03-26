using DesignPatterns.Abstraction;
using System;

namespace DiningCombat.Player.Offline.State
{
    internal class StatePowering : DCState
    {
        public event Action<float> OnPower;
        public StatePowering(byte stateId, string stateName)
            : base(stateId, stateName)
        {
        }

        public override string ToString()
        {
            return "StatePowering : " + this.name;
        }
    }
}
