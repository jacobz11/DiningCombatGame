using UnityEngine;
using Assets.Scrips_new.DesignPatterns;
using Assets.Scrips_new.Managers;

public class ChannelGameFoodObj : ScriptableObject , IChannelGame
{
    public static ChannelObserver<Vector3> UickedFruit = new ChannelObserver<Vector3>();
}