using System;
using Util.Abstraction;

namespace DiningCombat
{
    namespace Channels
    {
        namespace Player
        {
            internal class PlayersMangerChannel : IChannelGame
            {
                public class AnimationChannel
                {
                    public event Action ThrowingPoint;

                    internal void OnThrowingPoint()
                    {
                        ThrowingPoint?.Invoke();
                    }
                }

                public AnimationChannel Animation;

                public void OnThrowingPointEntet()
                {
                    Animation.OnThrowingPoint();
                }
            }
        }
    }
}