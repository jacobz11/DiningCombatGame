namespace DiningCombat
{
    public static class GameGlobal
    {
        public static class GameObjectName
        {
            public const string k_Player = "Player";
            public const string k_PickUpPoint = "PickUpPoint";
            public const string k_Ground = "Ground";
        }

        public static class TagNames
        {
            public const string k_Ground = "Ground";
            public const string k_FoodObj = "FoodObj";
            public const string k_ThrowFoodObj = "ThrownObject";
            public const string k_Player = "Player";
            public const string k_Picked = "Picked";
            public const string k_Ui = "UI";
            public const string k_Environment = "environment";
            public const string k_Water = "water";
            public const string k_DontDestroyOnLoad = "DontDestroyOnLoad";
            public const string k_Hide = "hide";
        }

        public static class AnimationName
        {
            public const string k_Running = "isRunning";
            public const string k_RunningSide = "isSideRun";
            public const string k_Throwing = "isThrowing";
        }

        public static class FoodObjData
        {
            public const byte k_NumOfPrefab = 4;
            // hit 
            public const string k_Apple = "Apple";
            public const byte k_AppleVar = 0;
            public const string k_AppleLocation = @"FoodPrefab\Apple";

            public const string k_Tomato = "Tomato";
            public const byte k_TomatoVar = 1;
            public const string k_TomatoLocation = @"FoodPrefab\Tomato";

            // Dispersing
            public const string k_Cabbage = "Cabbage";
            public const byte k_CabbageVar = 2;
            public const string k_CabbageLocation = @"FoodPrefab\Cabbage";

            // 
            public const string k_Flour = "Flour";
            public const byte k_FlourVar = 3;
            public const string k_FlourLocation = @"FoodPrefab\Flour";
        }

        public enum ePlayerModeType
        {
            OfflinePlayer,
            OfflineAiPlayer,
            OfflineTestPlayer,
            MLTrining,
            OnlinePlayer,
            OnlineAiPlayer,
            OnlineTestPlayer,
        }
    }
}