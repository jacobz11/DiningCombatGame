using UnityEngine;

internal interface IEffectable
{
    ParticleSystem Effect { get; }
    void PerformTheEffect();
}