using UnityEngine;

internal interface IThrowable
{
    bool IsThrow { get; }
    void SetToThrowing();
    float HitPintCalculator();

    void Throw(Vector3 i_Direction, float i_Force);

}