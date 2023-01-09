using UnityEngine;

namespace DiningCombat
{
    public static class GameGlobal
    {
        public const string k_GameObjectPlayer = "Player";
        public const string k_GameObjectPickUpPoint = "PickUpPoint";

        public const string k_TagGround = "Ground";
        public const string k_TagCapsule = "Capsule";

        public const KeyCode k_PowerKey = KeyCode.E;
        public const KeyCode k_JumpKey = KeyCode.Space;
        public const KeyCode k_ForwardKey = KeyCode.W;
        public const KeyCode k_LeftKey = KeyCode.A;
        public const KeyCode k_BackKey = KeyCode.S;
        public const KeyCode k_RightKey = KeyCode.D;

        public const KeyCode k_ForwardKeyArrow = KeyCode.UpArrow;
        public const KeyCode k_LeftKeyArrow = KeyCode.LeftArrow;
        public const KeyCode k_BackKeyArrow = KeyCode.DownArrow;
        public const KeyCode k_RightKeyArrow = KeyCode.RightArrow;

        public const string k_AnimationRunning = "isRunning";
    }
}
