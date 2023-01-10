using UnityEngine;

namespace DiningCombat
{
    public static class GameGlobal
    {
        public static class GameObjectName
        {
            public const string k_Player = "Player";
            public const string k_PickUpPoint = "PickUpPoint";
        }
        public static class TagNames
        {
            public const string k_Ground = "Ground";
            public const string k_Capsule = "Capsule";
        }

        public static class AnimationName
        {
            public const string k_Running = "isRunning";
            public const string k_RunningSide = "isSideRun";
            public const string k_Throwing = "isThrowing";
        }

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

        // ==================================================
        // PickUpItem
        // ==================================================
        public const float k_MinDistanceToPickUp = 2f;
        public const int k_MaxItemToPick = 1;
    }
}
