using UnityEngine;

public interface ICollecter
{
    Transform PikUpPonit { get; }

    bool DidIHurtMyself(Collision i_Collision);
}