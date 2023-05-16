using UnityEngine;
/// <summary>
/// This class is designed to avoid interference with the parent object's animator,
/// which comes from the child's Rigidbody
/// </summary>
public class Ragdoll
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