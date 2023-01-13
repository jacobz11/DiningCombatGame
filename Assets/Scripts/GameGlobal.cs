using UnityEngine;
using Debug = UnityEngine.Debug;

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
            public const string k_FoodObj = "FoodObj";
            public const string k_ThrowFoodObj = "sFoodObj";
            public const string k_Player = "Player";
        }

        public static class AnimationName
        {
            public const string k_Running = "isRunning";
            public const string k_RunningSide = "isSideRun";
            public const string k_Throwing = "isThrowing";
        }

        public static class FoodObjsNames
        {
            // hit 
            public const string k_Apple = "Apple";
            public const byte k_AppleVar = 0;

            public const string k_Tomato = "Tomato";
            public const byte k_TomatoVar = 1;

            // Dispersing
            public const string k_Cabbage = "Cabbage";
            public const byte k_CabbageVar = 0;

            // 
            public const byte k_FlourVar = 0;
            public const string k_Flour = "Flour";

        }
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

        public static void Dedugger(string i_ClassName, string i_FuncName, string i_Vars)
        {
            Debug.Log(i_ClassName+"->( "+ i_FuncName +"):|"+ i_Vars);
        }

    }
}
