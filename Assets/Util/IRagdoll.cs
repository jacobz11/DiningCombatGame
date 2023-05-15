using UnityEngine;

internal class IRagdoll
{
    public static void DisableRagdoll(Rigidbody i_Rigidbody)
    {
        i_Rigidbody.isKinematic = true;
        i_Rigidbody.detectCollisions = false;
    }

    public static void EnableRagdoll(Rigidbody i_Rigidbody)
    {
        i_Rigidbody.isKinematic = false;
        i_Rigidbody.detectCollisions = true;
    }
}

