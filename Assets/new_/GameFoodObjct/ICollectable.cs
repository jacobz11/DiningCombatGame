using UnityEngine;

internal interface ICollectable
{
    bool IsCollect{ get; }
    void Collect(ICollecter i_Collecter);
}