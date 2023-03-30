using UnityEngine;
using Util;
using Util.Abstraction;

namespace DiningCombat
{
    namespace Channels
    {
        namespace GameFoodObj
        {
            public class ChannelGameFoodObj : ScriptableObject, IChannelGame
            {
                public static ChannelObserver<Vector3> s_UickedFruit = new ChannelObserver<Vector3>();
            }
        }
    }
}
