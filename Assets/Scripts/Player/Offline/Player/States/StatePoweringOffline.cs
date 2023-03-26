using DesignPatterns.Abstraction;
using System;

namespace DiningCombat.Player.Offline.State
{
    internal class StatePoweringOffline : DCState
    {
        public event Action<float> OnPower;
        public StatePoweringOffline(byte stateId, string stateName)
            : base(stateId, stateName)
        {
        }

        public override string ToString()
        {
            return "StatePowering : " + this.name;
        }
    }
}
