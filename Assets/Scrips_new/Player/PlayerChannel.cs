using Assets.Scrips_new.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips_new.Player
{
    internal class  PlayerChannel : MonoBehaviour , IChannelGame
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