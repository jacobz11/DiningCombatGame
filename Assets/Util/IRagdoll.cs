internal interface IRagdoll
{
    /// Let the rigidbody take control and detect collisions.
    void EnableRagdoll();
    /// Let animation control the rigidbody and ignore collisions.
    void DisableRagdoll();
}

