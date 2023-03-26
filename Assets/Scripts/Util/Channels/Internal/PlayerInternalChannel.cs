using System;
using Util.Abstraction;

namespace Assets.Scrips_new.Util.Channels.Internal
{
    internal class PlayerInternalChannel : IInternalChannel
    {
        public event Action<float> PlayerForceChange;

        public event Action<int> PlayerScoreChange;

        public event Action<int> LifePointChange;

        public event Action PlayerDead;

        public event Action AnimatorEvntThrow;

        public event Action<float, float> FruitHitPlayer;
    }
}
