using UnityEngine;

namespace DiningCombat
{
    public static class GameGlobal
    {
        // ==================================================
        // Game-Object-names
        // ==================================================
        public const string k_GameObjectPlayer = "Player";
        public const string k_GameObjectPickUpPoint = "PickUpPoint";

        // ==================================================
        // Tag-names
        // ==================================================
        public const string k_TagGround = "Ground";
        public const string k_TagCapsule = "Capsule";

        // ==================================================
        // Game-Key-Code
        // ==================================================
        public const KeyCode k_PowerKey = KeyCode.E;
        public const KeyCode k_JumpKey = KeyCode.Space;

        // Forwar
        public const KeyCode k_ForwardKey = KeyCode.W;
        public const KeyCode k_ForwardKeyArrow = KeyCode.UpArrow;

        // Back 
        public const KeyCode k_BackKey = KeyCode.S;
        public const KeyCode k_BackKeyArrow = KeyCode.DownArrow;

        // Left
        public const KeyCode k_LeftKey = KeyCode.A;
        public const KeyCode k_LeftKeyArrow = KeyCode.LeftArrow;

        // Right
        public const KeyCode k_RightKey = KeyCode.D;
        public const KeyCode k_RightKeyArrow = KeyCode.RightArrow;

        // ==================================================
        // Animation-name
        // ==================================================
        public const string k_AnimationRunning = "isRunning";
        public const string k_AnimationRunningSide = "isSideRun";

        // ==================================================
        // Default-SerializeField-val
        // k_ Default - class name - var name
        // ==================================================

        // Player-Movement
        public const float k_DefaultPlayerMovementRunSpeed = 20,
            k_DefaultPlayerMovementRunSideSpeed = 5,
            k_DefaultPlayerMovementGroundCheckDistance = 0.2f,
            k_DefaultPlayerMovementGravity = -9.81f,
            k_DefaultPlayerMovementJumpHeight = 2f;
        //public const LayerMask k_DefaultPlayerMovementGroundMask = LayerMask;

        // CameraFollow
        public const float k_DefaultCameraFollowMouseSensetivity = 1000f;
    }
}
